﻿
namespace Server.Helpers
{
    public static class ExtractIdHelper
    {
        public static bool ExtractedIdFromClaim(string? userIdString,out long userId)
        {

            if (userIdString == null) { userId = 0; return false; }
            var splicedUrl = userIdString.Split('/');
            if (long.TryParse(splicedUrl.Last(),out userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
