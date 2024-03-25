
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ModelLibrary
{
    [Microsoft.EntityFrameworkCore.Index(nameof(ChatId))]
    public class Message
    {
        [Key]
        public long Id { get; set; }
        public DateTime SendTime { get; set; }
        public int ChatId { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }

        [NotNull]
        public string MessageText { get; set; }
        public string? ImageKey { get; set; }

        public Trade? Trade { get; set; }
        public Guid? TradeId { get; set; }


    }
}
