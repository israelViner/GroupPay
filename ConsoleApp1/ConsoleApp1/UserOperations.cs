using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal static class UserOperations
    {
        public static bool Run(ref int? user_id)
        {
            Console.WriteLine("What do you want to do? \n" +
                              "Enter the number of the operation: \n" +
                              "  1. create group \n" +
                              "  2. join group \n" +
                              "  3. balance analysis \n" +
                              "  4. insert into group \n" +
                              "  5. logout \n" +
                              "  6. exit \n" +
                              "  7. settings");
            int op = Convert.ToInt32(Console.ReadLine())!;
            switch (op)
            {
                case 1:
                    GroupManagement.CreateGroup(user_id.GetValueOrDefault());
                    return true;
                case 2:
                    GroupManagement.JoinGroup(user_id.GetValueOrDefault());
                    return true;
                case 3:
                    Console.WriteLine($"Your balance is: {UserManagement.BalanceAnalysis(user_id.GetValueOrDefault())}");
                    return true;
                case 4:
                    int group_id = Utils.ChooseGroup(user_id.GetValueOrDefault());
                    bool result = GroupOperations.Run(group_id);
                    return result;
                case 5:
                    Console.WriteLine("You have successfully logged out!");
                    user_id = OpeningQuestions.Run()!;
                    return true;
                case 6:
                    return false;
                case 7:
                    return UserSettings(user_id.GetValueOrDefault());
                default:
                    return true;
            }            
        }

        private static bool UserSettings(int user_id)
        {
            Console.WriteLine("What do you want to do? \nEnter the number of the operation: \n" +
                               "  1. Change username \n" +
                               "  2. Change password \n");
            int op = Convert.ToInt32(Console.ReadLine())!;
            switch (op)
            {
                case 1:
                    ChangeUserName(user_id);
                    Console.WriteLine("Your username changed successfully!");
                    return true;
                case 2:
                    ChangePassword(user_id);
                    Console.WriteLine("Your password changed successfully!");
                    return true;
                default:
                    return true;
            }
        }

        private static void ChangeUserName(int user_id)
        {
            string new_user_name = Utils.ChooseUserName();
            UserManagement.ChangeUserName(user_id, new_user_name);
        }

        private static void ChangePassword(int user_id)
        {
            int new_password = Authentication.ChoosePassword();
            UserManagement.ChangePassword(user_id, new_password);
        }
    }
}
