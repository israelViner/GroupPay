using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GroupPay
{
    internal class UserManagement
    {
        private static UserManagement? Instance;
        public List<User> UserList { get; set; }
        private int IdAllocate { get; set; }

        private UserManagement()
        {
            UserList = new List<User>();
            List<User>? list_file = JsonFileUtils<List<User>>.ReadJson("users_data.txt");
            UserList = list_file ?? UserList;
            IdAllocate = UserList.Count == 0 ? -1 : UserList.Count - 1;
        }

        public static UserManagement GetInstance()
        {            
            if (Instance == null)
            {
                Instance = new UserManagement();
            }            

            return Instance;
        }

        public static User GetUser(int user_id)
        {
            return GetInstance().UserList[user_id];
        }

        public static int GetId(string name)
        {
            try
            {
                return UserNamesList.GetInstance().NamesList.FirstOrDefault(x => x.Key == name).Value; 
            }
            catch 
            {
                throw new Exception("This name does not match any user...");
            }
        }

        public static string GetName(int user_id)
        {
            return UserNamesList.GetInstance().NamesList.FirstOrDefault(x => x.Value == user_id).Key;
        }

        public static List<string> GetUsers()
        {
            return UserNamesList.GetInstance().NamesList.Keys.ToList();
        }

        public static int CteateUser(string user_name)
        {            
            int user_id = AllocateId();
            int password = Authentication.ChoosePassword();
            User new_user = new(user_id, password);
            GetInstance().UserList.Add(new_user);
            UsersGroupsConnections.AddUser();
            UserNamesList.AddName(user_name, user_id);
            return user_id;
        }

        public static int? Login(int user_id)
        {
            if (Authentication.CheckPassword(user_id))
            {
                return user_id;
            }
            else
            {
                return null;
            }
        }

        private static int AllocateId()
        {
            ++GetInstance().IdAllocate;
            return GetInstance().IdAllocate;
        }

        public static bool IsValidUser(string user_name)
        {
            return UserNamesList.Contains(user_name); 
        }

        public static int BalanceAnalysis(int user_id)
        {
            return Balance.UserAnalysis(user_id);
        }

        public static void ChangeUserName(int user_id, string new_user_name)
        {
            UserNamesList.DeleteName(UserNamesList.GetInstance().NamesList.FirstOrDefault(x => x.Value == user_id).Key);
            UserNamesList.AddName(new_user_name, user_id);
        }

        public static void ChangePassword(int user_id, int new_password)
        {
            GetInstance().UserList.Find(user => user.UserId == user_id)!.Password = new_password;
        }

    }
}
