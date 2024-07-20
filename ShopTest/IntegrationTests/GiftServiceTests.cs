using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopDomain.Entities;
using ShopPersistance;
using ShopServices;

namespace Shop.Test.IntegrationTests
{
    public class GiftServiceTests
    {
        private DbContextOptions<ShopContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        private IDbContextFactory<ShopContext> GetDbContextFactory(DbContextOptions<ShopContext> options)
        {
            var mockFactory = new Mock<IDbContextFactory<ShopContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new ShopContext(options));
            return mockFactory.Object;
        }
        [Fact]
        public async Task Save_ShouldAddGift()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };

            // Act
            await service.Save(gift);

            // Assert
            using var context = new ShopContext(options);
            var savedGift = await context.Gifts.FirstOrDefaultAsync(b => b.GiftName == "Gift1");
            Assert.NotNull(savedGift);
        }
        [Fact]
        public async Task GetById_ShouldReturnGiftByGiftId()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };
            await service.Save(gift);

            // Act
            var fetchedGift = await service.Get(gift.GiftId);

            // Assert
            Assert.NotNull(fetchedGift);
            Assert.Equal(gift.GiftName, fetchedGift.GiftName);
        }
       
        [Fact]
        public async Task GetList_ShouldReturnGiftsByGiftName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            await service.Save(new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" });
            await service.Save(new Gift { GiftName = "Gift2", GiftType = "بوكس مفتوح", GiftDescription = "لون ابيض", GiftPrice = "100" });

            // Act
            var gifts = await service.GetList("Gift");

            // Assert
            Assert.Equal(2, gifts.Count);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllGifts()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            await service.Save(new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" });
            await service.Save(new Gift { GiftName = "Gift2", GiftType = "بوكس مفتوح", GiftDescription = "لون ابيض", GiftPrice = "100" });

            // Act
            var gifts = await service.GetAll();

            // Assert
            Assert.Equal(2, gifts.Count);
        }
        [Fact]
        public async Task Delete_ShouldRemoveGift()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices (factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };
            await service.Save(gift);

            // Act
            await service.Delete(gift);

            // Assert
            using var context = new ShopContext(options);
            var deletedGift = await context.Gifts.FindAsync(gift.GiftId);
            Assert.Null(deletedGift);
        }
        [Fact]
        public async Task Update_ShouldModifyGift()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };
            await service.Save(gift);

            // Act
            gift.GiftName = "Updated Gift";
            gift.GiftType = "بوكس صغير";
            await service.Update(gift);

            // Assert
            using var context = new ShopContext(options);
            var updatedGift = await context.Gifts.FindAsync(gift.GiftId);
            Assert.Equal("Updated Gift", updatedGift.GiftName);
            Assert.Equal("بوكس صغير", updatedGift.GiftType);
        }
        [Fact]
        public async Task AddFlowerToGift_ShouldAddFlower()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };
            var flower = new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" };
            await service.Save(gift);

            // Act
            await service.AddFlowerToGift(gift, flower);

            // Assert
            using var context = new ShopContext(options);
            var savedGift = await context.Gifts.Include(b => b.Flowers).FirstOrDefaultAsync(b => b.GiftId == gift.GiftId);
            Assert.NotNull(savedGift);
            Assert.Contains(savedGift.Flowers, a => a.FlowerName == "Flower1");
        }
        [Fact]
        public async Task RemoveFlowerFromGift_ShouldRemoveFlower()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new GiftServices(factory);
            var gift = new Gift { GiftName = "Gift1", GiftType = "بوكس مغلق", GiftDescription = "لون وردي", GiftPrice = "80" };
            var flower = new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" };
            await service.Save(gift);
            await service.AddFlowerToGift(gift, flower);

            // Act
            await service.RemoveFlowerFromGift(gift, flower);

            // Assert
            using var context = new ShopContext(options);
            var savedGift = await context.Gifts.Include(b => b.Flowers).FirstOrDefaultAsync(b => b.GiftId == gift.GiftId);
            Assert.NotNull(savedGift);
            Assert.DoesNotContain(savedGift.Flowers, a => a.FlowerName == "Flower1");
        }
    }

}
