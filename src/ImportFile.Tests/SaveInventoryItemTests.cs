using FluentAssertions;
using ImportFile.Core.Inventory.UseCases.SaveInventoryItem;
using ImportFile.Tests.Stubs;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ImportFile.Core.Inventory.InventoryAggregate;

namespace ImportFile.Tests
{
    public class SaveInventoryItemTests
    {
        [Test]
        public async Task GivenExistingItem_WhenSaving_ItemShouldBeUpdated()
        {
            InventoryItem existingItem = ExistingInventoryItem();
            InventoryItem newItem = NewInventoryItem();

            InventoryItemUnitOfWorkStub uowStub = new InventoryItemUnitOfWorkStub();
            uowStub.SetUpExisting(existingItem);

            var handler = new SaveInventoryItemCommandHandler(uowStub);
            await handler.Handle(new SaveInventoryItemCommand
            {
                Item = newItem
            }, CancellationToken.None);

            uowStub.UnderlyingEntity.Description.Should().Be(newItem.Description);
        }

        [Test]
        public async Task GivenNonExistingItem_WhenSaving_ItemShouldBeAdded()
        {
            InventoryItem newItem = NewInventoryItem();

            InventoryItemUnitOfWorkStub uowStub = new InventoryItemUnitOfWorkStub();

            var handler = new SaveInventoryItemCommandHandler(uowStub);
            await handler.Handle(new SaveInventoryItemCommand
            {
                Item = newItem
            }, CancellationToken.None);

            uowStub.UnderlyingEntity.Should().BeEquivalentTo(newItem);
        }

        private InventoryItem ExistingInventoryItem()
        {
            return new InventoryItem(
                "fileId",
                "key",
                "artikelCode",
                new SellingDetails(50, 10),
                "description",
                "1-2 working days",
                "babies",
                "large",
                new Color("code", "desc"));
        }

        private InventoryItem NewInventoryItem()
        {
            return new InventoryItem(
                "fileId",
                "key",
                "artikelCode",
                new SellingDetails(50, 10),
                "description-II",
                "1-2 working days",
                "babies",
                "large",
                new Color("code", "desc"));
        }
    }
}