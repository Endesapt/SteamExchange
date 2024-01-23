﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Server.Models
{
    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(ChatId))]
    public class Message
    {
        [JsonIgnore]
        public long Id { get; set; }
        public required DateTime SendTime { get; set; }
        public required int ChatId { get; set; }
        public required long FromUserId { get; set; }
        public required long ToUserId { get; set; }

        [NotNull]
        public required string MessageText { get; set; }
        public string? ImageURl { get; set; }


    }
}