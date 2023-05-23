using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal static class GroupOperations
    {
        public static bool Run(int group_id)
        {
            Console.WriteLine("What do you want to do in this group? \nEnter the number of the operation: \n" +
                               "  1. get group link \n" +
                               "  2. add purchase \n" +
                               "  3. group analysis \n" +
                               "  4. print purchases \n" +
                               "  5. logout \n" +
                               "  6. group settings");
            int op = Convert.ToInt32(Console.ReadLine())!;
            switch (op)
            {
                case 1:
                    Console.WriteLine(GroupManagement.GetGroupLink(group_id));
                    return true;
                case 2:
                    GroupManagement.AddPurchase(group_id);
                    return true;
                case 3:
                    GroupManagement.GroupAnalysis(group_id);
                    return true;
                case 4:
                    GroupManagement.GetPurchases(group_id).ForEach(x => Console.Write(x + ", "));
                    Console.WriteLine();
                    return true;
                case 5:
                    Console.WriteLine("You have successfully logged out!");
                    return true;
                default:
                    return true;
            }
        }
    }
}
