using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {
        public required long Id { get; set; }
        public required string UserName { get; set; }
        public required string AvatarHash { get; set; }
    }
}
