using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.ResponseModels
{
    internal class ProfileResponseModel
    {
        public User User { get; set; }
        public List<Weapon> Weapons { get; set; }
    }
}