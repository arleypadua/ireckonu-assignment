using System.ComponentModel;
using System.Globalization;
using ImportFile.Core.Inventory.AggregateRoot;

namespace ImportFile.Core.Inventory.UseCases
{
    internal static class InventoryItemFactory
    {
        public static InventoryItem FromCsvLine(string[] lineData, string fileId)
        {
            if(lineData.Length < 10)
                throw new InvalidEnumArgumentException("Invalid amount of columns");

            return new InventoryItem(
                fileId,
                lineData[DataPositions.Key],
                lineData[DataPositions.ArtikelCode],
                new SellingDetails(
                    decimal.Parse(lineData[DataPositions.Price], CultureInfo.InvariantCulture),
                    decimal.Parse(lineData[DataPositions.DiscountPrice], CultureInfo.InvariantCulture)), 
                lineData[DataPositions.Description],
                lineData[DataPositions.DeliveredIn],
                lineData[DataPositions.Q1],
                lineData[DataPositions.Size],
                new Color(
                    lineData[DataPositions.ColorCode],
                    lineData[DataPositions.Color]));
        }

        private static class DataPositions
        {
            public const int Key = 0;
            public const int ArtikelCode = 1;
            public const int ColorCode = 2;
            public const int Description = 3;
            public const int Price = 4;
            public const int DiscountPrice = 5;
            public const int DeliveredIn = 6;
            public const int Q1 = 7;
            public const int Size = 8;
            public const int Color = 9;
        }
    }
}