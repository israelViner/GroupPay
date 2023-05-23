using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal static class Utils
    {
        public static int ChooseGroup(int user_id)
        {
            string group_name;

            while (true)
            {
                Console.WriteLine("Enter the name of the group that you want to insert into, those are the groups");
                PrintUserGroupsList(user_id);
                group_name = Console.ReadLine()!;
                if (GroupManagement.IsValidGroup(group_name) && UsersGroupsConnections.Contains(user_id, GroupManagement.GetId(group_name)))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This name is wrong, please try again...");
                }
            }

            return GroupManagement.GetId(group_name);
        }

        public static string GetUserName()
        {
            string user_name;

            while (true)
            {
                Console.WriteLine("Enter your user-name:");
                user_name = Console.ReadLine()!;
                if (UserManagement.IsValidUser(user_name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This name is wrong, please try again...");
                }
            }

            return user_name;
        }

        public static string GetGroupName()
        {
            string group_name;

            while (true)
            {
                Console.WriteLine("Enter the name of the group:");
                group_name = Console.ReadLine()!;
                if (GroupManagement.IsValidGroup(group_name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This name is wrong, please try again...");
                }
            }

            return group_name;
        }

        public static string ChooseGroupName()
        {
            Console.WriteLine("Enter the name of the group that you want to create: ");
            string group_name = Console.ReadLine()!;
            if (GroupManagement.IsValidGroup(group_name))
            {
                Console.WriteLine("That name is already taken!");
                ChooseGroupName();
            }

            return group_name;
        }

        public static string ChooseUserName()
        {
            Console.WriteLine("Enter the name of the user that you interest in: ");
            string user_name = Console.ReadLine()!;
            if (UserManagement.IsValidUser(user_name))
            {
                Console.WriteLine("That name is already taken!");
                ChooseUserName();
            }

            return user_name;
        }

        public static void PrintGroupsList()
        {
            GroupManagement.GetInstance().GroupList.Select(x => x.GroupName).ToList().ForEach(x => Console.Write(x + ", "));
            Console.WriteLine();
        }

        public static void PrintUserGroupsList(int user_id)
        {
            UsersGroupsConnections.GetUserGroups(user_id).ForEach(x => Console.Write(GroupManagement.GetName(x) + ", "));
            Console.WriteLine();
        }

    }
}
