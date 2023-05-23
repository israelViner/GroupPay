using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal static class OpeningQuestions
    {
        public static int? Run()
        {
            string answer;
            do
            {
                Console.WriteLine("Do you have a user account? [y / n] ");
                answer = Console.ReadLine()!;
            } while (answer != "y" && answer != "n");

            if (answer == "n")
            {
                return UserManagement.CteateUser(Utils.ChooseUserName());
            }
            else
            {
                string user_name = Utils.GetUserName();
                return UserManagement.Login(UserManagement.GetId(user_name));
            }
        }
    }
}
