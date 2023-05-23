using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPay
{
    internal class Group
    {
        public record class Purchase(int PayerId, string Product, int Price, List<int> PurchaseMembers);

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string ConnectingLink { get; set; }
        public List<Purchase> GroupPurchases { get; set; }

        public Group(int group_id, string group_name, string connecting_link)
        {
            this.GroupId = group_id;
            this.GroupName = group_name;
            this.ConnectingLink = connecting_link;
            this.GroupPurchases = new List<Purchase>();
        }

        public Group()
        {
            this.GroupId = -1;
            this.GroupName = "";
            this.ConnectingLink = "";
            this.GroupPurchases = new List<Purchase>();
        }

        //public void AddMember(int id)
        //{
        //    this.GroupMembers.Add(id);
        //}

        public void AddPurchase(int payer_id, string product, int price, List<int> members)
        {
            GroupPurchases.Add(new Purchase(payer_id, product, price, members));
        }

        public List<string> GetPurchases()
        {
            return GroupPurchases.Select(x => x.Product).ToList();
        }

    }
}
