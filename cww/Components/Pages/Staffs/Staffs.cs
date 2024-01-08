 
using System.Text.Json;
 
using static cww.Resources.common.Common;

namespace cww.Components.Pages.Staffs
{
    public class StaffEntity
    {
        public Guid id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }

    public class StaffController
    {

        public void CreateStaff(string userName, string password)
        {
            List<StaffEntity> staffs = Loadstaffs();

            staffs.Add(new StaffEntity { id = Guid.NewGuid(), UserName = userName, Password = HashPassword(password) });
            Savestaffs(staffs);
        }

        public static int GetStaffNumber()
        {
            try
            {
                if (File.Exists(StaffsFilePath))
                {
                    var json = File.ReadAllText(StaffsFilePath);
                    return JsonSerializer.Deserialize<List<StaffEntity>>(json)?.Count ?? 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading staff data: {ex.Message}");


            }
            return 0;
        }

        public List<StaffEntity> Loadstaffs()
        {
            try
            {
                if (File.Exists(StaffsFilePath))
                {
                    var json = File.ReadAllText(StaffsFilePath);
                    return JsonSerializer.Deserialize<List<StaffEntity>>(json) ?? new List<StaffEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading staff data: {ex.Message}");
            }

            return new List<StaffEntity>();
        }

        private void Savestaffs(List<StaffEntity> staffs)
        {
            var json = JsonSerializer.Serialize(staffs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StaffsFilePath, json);
        }
    }
}
