﻿namespace TrucksApi.Responses
{
    public class GetAllTrucksResponseItem
    {
        public int Id { get; set; }

        // must have a unique alphanumeric code given by the user
        public string AlphanumericCode { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string TruckStatusFriendlyString { get; set; }
    }
}