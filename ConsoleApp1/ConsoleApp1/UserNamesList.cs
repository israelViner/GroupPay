using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal class UserNamesList
    {
        private static UserNamesList? Instance;
        public Dictionary<string, int> NamesList {get; set;}

        private UserNamesList()
        {
            NamesList = new Dictionary<string, int>();
            Dictionary<string, int>? list_file = JsonFileUtils<Dictionary<string, int>>.ReadJson("users_names.txt");
            NamesList = list_file ?? NamesList;
        }

        public static UserNamesList GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserNamesList();
            }            

            return Instance;
        }

        public static void AddName(string name, int id)
        {            
            GetInstance().NamesList[name] = id;
        }

        public static bool Contains(string name)
        {
            return GetInstance().NamesList.ContainsKey(name);
        }

        public static void DeleteName(string name)
        {
            GetInstance().NamesList.Remove(name);
        }

    }
}
