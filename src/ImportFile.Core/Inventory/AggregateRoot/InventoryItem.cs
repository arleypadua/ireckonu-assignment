﻿using ImportFile.SharedKernel.Domain;
using System;

namespace ImportFile.Core.Inventory.AggregateRoot
{
    public class InventoryItem : Entity, IAggregateRoot
    {
        public InventoryItem(
            string inventoryFileId,
            string key,
            string artikelCode,
            SellingDetails sellingDetails,
            string description,
            string deliveredIn,
            string audience,
            string size,
            Color color)
        {
            Id = Guid.NewGuid().ToString();
            InventoryFileId = inventoryFileId ?? throw new ArgumentNullException(nameof(inventoryFileId));
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ArtikelCode = artikelCode ?? throw new ArgumentNullException(nameof(artikelCode));
            SellingDetails = sellingDetails ?? throw new ArgumentNullException(nameof(sellingDetails));
            Description = description;
            DeliveredIn = deliveredIn;
            Audience = audience;
            Size = size;
            Color = color;
        }

        public string InventoryFileId { get; private set; }
        public string Key { get; private set; }
        public string ArtikelCode { get; private set; }

        public SellingDetails SellingDetails { get; private set; }
        public string Description { get; private set; }
        public string DeliveredIn { get; private set; }
        public string Audience { get; private set; }
        public string Size { get; private set; }
        public Color Color { get; private set; }
    }
}