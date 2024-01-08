
using System.Text.Json;
using static cww.Resources.common.Common;

namespace cww.Components.Pages.Admin
{

    public class AdminEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }

    public class AdminController
    {
        public void SeedAdminData()
        {
            AdminEntity admins = LoadAdmin();

            if (admins != null)
            {
                SaveAdmins(new AdminEntity { Email = "admin@email.com", Password = HashPassword("password") });
            }
        }

        public static AdminEntity LoadAdmin()
        {
            if (File.Exists(AdminsFilePath))
            {
                var json = File.ReadAllText(AdminsFilePath);
                return JsonSerializer.Deserialize<AdminEntity>(json) ?? new AdminEntity();
            }

            return new AdminEntity();
        }

        private static void SaveAdmins(AdminEntity admins)
        {
            var json = JsonSerializer.Serialize(admins, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(AdminsFilePath, json);
        }
    }
}
