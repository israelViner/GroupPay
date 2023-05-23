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
    internal class GroupManagement
    {
        private static GroupManagement? Instance;
        public List<Group> GroupList { get; set; }
        private static int IdAllocate;

        private GroupManagement()
        {
            GroupList = new List<Group>();
            List<Group>? groups_file = JsonFileUtils<List<Group>>.ReadJson("groups_data.txt");
            GroupList = groups_file ?? GroupList;
            IdAllocate = GroupList.Count == 0 ? -1 : GroupList.Count - 1;
        }

        public static GroupManagement GetInstance()
        {            
            if (Instance == null)
            {
                Instance = new GroupManagement();
            }      
            
            return Instance;
        }

        public static Group GetGroup(string name)
        {
            try
            {
                return GetInstance().GroupList.Find(x => x.GroupName == name)!;
            }
            catch
            {
                throw new Exception("This name does not match any group...");
            }
        }

        public static Group GetGroup(int group_id)
        {
            try
            {
                return GetInstance().GroupList[group_id];
            }
            catch
            {
                throw new Exception("This ID does not match any group...");
            }
        }

        public static int GetId(string name) 
        {
            try
            {
                return GetInstance().GroupList.Find(x => x.GroupName == name)!.GroupId;
            }
            catch
            {
                throw new Exception("This name does not match any group...");
            }
        }

        public static int GetId(int group_id)
        {
            try
            {
                return GetInstance().GroupList[group_id].GroupId;
            }
            catch
            {
                throw new Exception("This ID does not match any group...");
            }
        }

        public static string GetName(int group_id)
        {
            try
            {
                return GetInstance().GroupList[group_id].GroupName;
            }
            catch
            {
                throw new Exception("This name does not match any group...");
            }           
        }

        public static List<string> GetGroups() 
        {
            try
            {
                return GetInstance().GroupList.Select(x => x.GroupName).ToList();
            }
            catch
            {
                throw new Exception("There is not group exsisting...");
            }
        }

        public static void CreateGroup(int user_id)
        {
            string group_name = Utils.ChooseGroupName();

            Group new_group = CreateGroup(group_name);
            GetInstance().GroupList.Add(new_group);

            UsersGroupsConnections.AddGroup(user_id);
        }

        private static Group CreateGroup(string group_name)
        {
            int group_id = AllocateId();
            string connecting_link = AllocateConnectingLink(group_name);
            Group new_group = new(group_id, group_name, connecting_link);
            GetInstance().GroupList.Add(new_group);
            return new_group;
        }

        private static int AllocateId() 
        {
            ++IdAllocate;
            return IdAllocate;
        }

        private static string AllocateConnectingLink(string group_name)
        {
            return $"groups/{group_name}/{IdAllocate}";
        }

        public static bool IsValidGroup(string group_name)
        {
            return GetInstance().GroupList.Exists(x => x.GroupName == group_name)!;
        }

        public static bool IsValidGroup(int group_id)
        {
            return GetInstance().GroupList.Exists(x => x.GroupId == group_id);
        }

        public static bool IsInGroupMembers(int group_id, int user_id)
        {
            try
            {
                return UsersGroupsConnections.GetInstance().DataConnections[user_id][group_id] == 1;
            }
            catch
            {
                throw new Exception($"{group_id} not found in the group list");
            }
        }

        public static void AddPurchase(int group_id)
        {            
            Console.WriteLine("Enter the name of the product: ");
            string purchase_name = Console.ReadLine()!;
            Console.WriteLine("Enter the price of the product: ");
            int purchase_price = Convert.ToInt32(Console.ReadLine()!);

            string payer_name;
            int payer_id;
            do
            {
                Console.WriteLine("Who is the payer for this purchase?");
                payer_name = Console.ReadLine()!;
                payer_id = UserManagement.GetId(payer_name);
            } while (UsersGroupsConnections.GetInstance().DataConnections[payer_id][group_id] == 0);

            Console.WriteLine("Which members of the group participate in the purchase? \nthose are the members of the group: ");
            //GetGroup(group_id).GroupMembers.ForEach(x => Console.Write(UserManagement.GetName(x) + ", "));
            Console.WriteLine("\n[Enter the names with white space between them]");
            List<string> members = Console.ReadLine()!.Split(' ').ToList();


            var members_id = members.Select(x => UserManagement.GetId(x)).ToList();
            members_id = members_id.Where(user_id => IsInGroupMembers(group_id, user_id)).ToList();

            GetGroup(group_id).AddPurchase(payer_id, purchase_name, purchase_price, members_id);
        }

        public static string GetGroupLink(int group_id)
        {
            return GetGroup(group_id).ConnectingLink;            
        }

        public static List<string> GetPurchases(int group_id)
        {
            return GetGroup(group_id).GetPurchases();            
        }

        public static void GroupAnalysis(int group_id)
        {            
            Hashtable group_analyze = Balance.GroupAnalysis(group_id);
            IDictionaryEnumerator e = group_analyze.GetEnumerator();
            Console.WriteLine($"This group contain { UsersGroupsConnections.GetGroupUsers(group_id).Count} members, and this is the analyze of the group: ");
            while (e.MoveNext())
            {
                Console.WriteLine($"{UserManagement.GetName(Convert.ToInt32(e.Key))}: {e.Value}");
            }
        }

        //private static bool IsInYourGroups(string group_name)
        //{
        //    return GetInstance().GroupList.Exists(x => x.GroupId == GetId(group_name));
        //}       

        public static void JoinGroup(int user_id)
        {
            Console.WriteLine("Enter the connecting-link of the group that you want to join into: ");
            string? group_link = null;
            while (group_link == null)
            {
                group_link = Console.ReadLine();
            }
            Group? group = FindGroupByLink(group_link);
            if (group != null)
            {
                UsersGroupsConnections.GetInstance().DataConnections[user_id][group.GroupId] = 1;
                Console.WriteLine("You successfully joined the group!");
            }
            else
            {
                Console.WriteLine("This link is wrong!");
            }
        }

        private static Group? FindGroupByLink(string connecting_link)
        {
            return GetInstance().GroupList.Find(group => group.ConnectingLink == connecting_link);
        }
    }
}
