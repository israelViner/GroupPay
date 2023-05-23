using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupPay
{
    internal class Tui
    {
        public static int? UserId = null;

        //public Tui() { }
        
        public static void Run()
        {
            Console.WriteLine("Welcome to GroupPay!");
            UserId = OpeningQuestions.Run();
            if (UserId != null)
            {
                while (true)
                {
                    if (!UserOperations.Run(ref UserId))
                    {
                        break;
                    }
                }

                JsonFileUtils<List<User>>.WriteJson("users_data.txt", UserManagement.GetInstance().UserList);
                JsonFileUtils<List<Group>>.WriteJson("groups_data.txt", GroupManagement.GetInstance().GroupList);
                JsonFileUtils<List<List<short>>>.WriteJson("main_data.txt", UsersGroupsConnections.GetInstance().DataConnections);
                JsonFileUtils<Dictionary<string, int>>.WriteJson("users_names.txt", UserNamesList.GetInstance().NamesList);
            }            
        }        
    }
}
