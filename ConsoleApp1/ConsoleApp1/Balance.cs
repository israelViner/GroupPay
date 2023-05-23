using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace GroupPay
{
    internal class Balance
    {
        public static Hashtable GroupAnalysis(int group_id) 
        {
            Group group = GroupManagement.GetGroup(group_id);
            Hashtable result = new();

            foreach (int user in UsersGroupsConnections.GetGroupUsers(group_id))
            {
                result.Add(user, 0);
            }            

            foreach (Group.Purchase purchase in group.GroupPurchases) 
            {
                int members_count = purchase.PurchaseMembers.Exists(x => x == purchase.PayerId) ? purchase.PurchaseMembers.Count() : purchase.PurchaseMembers.Count() + 1;
                int cost = purchase.Price / members_count;
             
                result[purchase.PayerId] = Convert.ToInt32(result[purchase.PayerId]) + purchase.Price - cost;
               
                foreach (int member in purchase.PurchaseMembers)
                {
                    if (member != purchase.PayerId)
                    {
                        result[member] = Convert.ToInt32(result[member]) - cost;
                    }
                }
            }

            return result;
        }

        public static int UserAnalysis(int user_id) 
        {
            int balance = 0;
            foreach (int group_id in UsersGroupsConnections.GetInstance().DataConnections[user_id])
            {              
                if (UsersGroupsConnections.Contains(user_id, group_id))
                {
                    balance += Convert.ToInt32(GroupAnalysis(group_id)[user_id]);
                }
            }

            return balance;
        }
    }
}
