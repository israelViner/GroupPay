using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal class UsersGroupsConnections
    {
        public List<List<short>> DataConnections { get; set; }
        private static UsersGroupsConnections? Instance = null;

        private UsersGroupsConnections()
        {
            DataConnections = new List<List<short>>();
            var list = JsonFileUtils<List<List<short>>>.ReadJson("main_data.txt");
            if (list != null)
            {
                DataConnections = list;
            }
        }

        public static UsersGroupsConnections GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UsersGroupsConnections();
            }          

            return Instance;
        }

        public static void AddGroup(int user_id)
        {
            GetInstance().DataConnections.ForEach(list => list.Add(0));
            GetInstance().DataConnections[user_id][^1] = 1;
        }

        public static void AddUser()
        {
            int group_count = GetInstance().DataConnections.Count == 0 ? 0 : GetInstance().DataConnections[0].Count;
            var list = new List<short>(group_count);

            foreach (int i in Enumerable.Range(0, group_count)) 
            {
                list.Add(0);
            }

            GetInstance().DataConnections.Add(list);
        }

        public static bool Contains(int user_id, int group_id)
        {
            return GetInstance().DataConnections[user_id][group_id] == 1;
        }

        public static bool IsValidUser(int user_id)
        {
            return GetInstance().DataConnections.Count > user_id;
        }

        public static bool IsValidGroup(int Group_id)
        {
            if (IsValidUser(0))
            {
                return GetInstance().DataConnections[0].Count > Group_id;
            }
            return false;
        }
        
        public static List<int> GetUserGroups(int user_id)
        {
            if (IsValidUser(user_id))
            {
                return GetInstance().DataConnections[user_id].Select((x, i) => i).Where(i => Contains(user_id, i)).ToList();
            }
            else
            {
                throw new Exception("This user is not exsist...");
            }
        }

        public static List<int> GetGroupUsers(int group_id)
        {
            if (IsValidGroup(group_id))
            {
                return GetInstance().DataConnections.Select((x, i) => i).Where(i => Contains(i, group_id)).ToList();
            }
            else
            {
                throw new Exception("This group is not exsist...");
            }
        }
    }
}
