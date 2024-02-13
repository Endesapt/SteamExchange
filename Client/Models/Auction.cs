using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    class Auction
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string TradeObject { get; set; }
        public bool IsActive { get; set; }  
        public int UserId {  get; set; }
        public string AuctionDescription {  get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public DateTime AuctionTime { get; set; }
        public DateTime AuctionUpTime { get; set; }


    }
}
