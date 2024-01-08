using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static cww.Resources.common.Common;


namespace cww.Components.Pages.Member
{

    public class MemberEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }


    }

    public class MemberController
    {

        public void Createmember(string Name, int PhoneNumber)
        {
            List<MemberEntity> members = Loadmembers();

            members.Add(new MemberEntity { Id = Guid.NewGuid(), Name = Name, PhoneNumber = PhoneNumber });
            Savemembers(members);
        }

        public static int GetmemberNumber()
        {
            try
            {
                if (File.Exists(memberFilePath))
                {
                    var json = File.ReadAllText(memberFilePath);
                    return JsonSerializer.Deserialize<List<MemberEntity>>(json)?.Count ?? 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading member data: {ex.Message}");
            }
            return 0;
        }

        public static List<MemberEntity> Loadmembers()
        {
            try
            {
                if (File.Exists(memberFilePath))
                {
                    var json = File.ReadAllText(memberFilePath);
                    return JsonSerializer.Deserialize<List<MemberEntity>>(json) ?? new List<MemberEntity>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading member data: {ex.Message}");
            }

            return new List<MemberEntity>();
        }

        private static void Savemembers(List<MemberEntity> members)
        {
            var json = JsonSerializer.Serialize(members, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(memberFilePath, json);
        }
    }
}
