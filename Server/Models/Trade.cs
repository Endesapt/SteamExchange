﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(UserToId))]
    [Microsoft.EntityFrameworkCore.Index(nameof(UserFromId))]
    public class Trade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required long UserFromId {  get; set; }
        public required long UserToId {  get; set; }
        public User UserTo { get; set; } = null!;
        public User UserFrom { get; set; } = null!;
        public List<TradeWeapon> TradeWeapons { get; set; }=null!;
    }
}
