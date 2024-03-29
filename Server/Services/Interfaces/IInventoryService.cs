﻿using ModelLibrary;
using Server.Models;
using Server.ResponseModels;

namespace Server.Services.Interfaces
{
    public interface IInventoryService
    {
        public IEnumerable<User> GetInventories(DateTime fromTime,int count=20);
        public IEnumerable<Offer> GetOffers(DateTime fromTime, int count = 20);
    }
}
