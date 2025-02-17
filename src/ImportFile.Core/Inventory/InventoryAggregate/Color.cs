﻿namespace ImportFile.Core.Inventory.InventoryAggregate
{
    public class Color
    {
        public Color(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; private set; }
        public string Description { get; private set; }
    }
}