using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal static class Authentication
    {
        public static bool CheckPassword(int user_id)
        {
            int password;
            int count = 0;
            Console.WriteLine("Enter the password of the user: ");
            do
            {
                password = Convert.ToInt32(Console.ReadLine())!;
                ++count;
                if (password != UserManagement.GetInstance().UserList[user_id].Password && count < 3)
                {
                    Console.WriteLine("The password is not correct, please try again... ");
                }
            } while (password != UserManagement.GetInstance().UserList[user_id].Password && count < 3);

            if (password != UserManagement.GetInstance().UserList[user_id].Password)
            {
                Console.WriteLine("Your attempts is finished, you failed to login into this account!");
                return false;
            }

            return true;
        }
    
        public static int ChoosePassword()
        {
            int password, password_2;

            do
            {
                Console.WriteLine("Enter a password for this user: ");
                password = Convert.ToInt32(Console.ReadLine())!;
                Console.WriteLine("Enter the password again: ");
                password_2 = Convert.ToInt32(Console.ReadLine())!;
            } while (password != password_2);

            return password;
        }
    }
}
