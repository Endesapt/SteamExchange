using Microsoft.EntityFrameworkCore;

namespace ModelLibrary
{
    [Index(nameof(UserId1))]
    [Index(nameof(UserId2))]
    public class Chat
    {
        public int Id { get; set; }
        public required long UserId1 { get; set; }
        public required long UserId2 { get; set; }
        public bool IsBanned { get; set; }=false;
    }

}
