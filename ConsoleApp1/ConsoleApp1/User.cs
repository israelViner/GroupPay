using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal class User
    {
        public int Password { get; set; }
        public int UserId { get; set; }

        public User(int user_id, int password)
        {
            this.UserId = user_id;
            this.Password = password;
        }

        public User()
        {
            this.UserId = -1;
            this.Password = 0;
        }      
     
    }
}
