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
    public class FlowerServiceTests
    {
        private DbContextOptions<ShopContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
        private IDbContextFactory<ShopContext> GetDbContextFactoryAsync(DbContextOptions<ShopContext> options)
        {
            var mockDbFactory = new Mock<IDbContextFactory<ShopContext>>();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(() => new ShopContext(options));
            return mockDbFactory.Object;

        }
        [Fact]
        public async Task Save_ShouldAddFlower()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            var flower = new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر ", FlowertPrice = "35" };

            // Act
            await service.Save(flower);

            // Assert
            using var context = new ShopContext(options);
            var savedFlower = await context.Flowers.FirstOrDefaultAsync(a => a.FlowerName == "Flower1");
            Assert.NotNull(savedFlower);
        }
        [Fact]
        public async Task Get_ShouldReturnFlowerByFlowerId()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            var flower = new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" };
            await service.Save(flower);

            // Act
            var fetchedFlower = await service.Get(flower.FlowerId);

            // Assert
            Assert.NotNull(fetchedFlower);
            Assert.Equal(flower.FlowerName, fetchedFlower.FlowerName);
        }
        [Fact]
        public async Task GetList_ShouldReturnFlowersByFlowerName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            await service.Save(new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" });
            await service.Save(new Flower { FlowerName = "Flower2", FlowerDescription = "لون احمر", FlowertPrice = "35" });

            // Act
            var flowers = await service.GetList("Flower");

            // Assert
            Assert.Equal(2, flowers.Count);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllFlowers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            await service.Save(new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" });
            await service.Save(new Flower { FlowerName = "Flower2", FlowerDescription = "لون احمر", FlowertPrice = "35" });

            // Act
            var flowers = await service.GetAll();
            // Assert
            Assert.Equal(2, flowers.Count);
        }
        [Fact]
        public async Task Delete_ShouldRemoveFlower()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            var flower = new Flower { FlowerName = "Flower1", FlowerDescription = "لون احمر", FlowertPrice = "35" };
            await service.Save(flower);

            // Act
            await service.Delete(flower);

            // Assert
            using var context = new ShopContext(options);
            var deletedFlower = await context.Flowers.FindAsync(flower.FlowerId);
            Assert.Null(deletedFlower);
        }
        [Fact]
        public async Task Update_ShouldModifyFlower()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new FlowerServices(factory);
            var flower = new Flower { FlowerName = "Flower1", FlowertPrice = "35", FlowerDescription = " لون احمر" };
            await service.Save(flower);

            // Act
            flower.FlowerName = "Updated Flower";
            flower.FlowertPrice = "36";
            flower.FlowerDescription = "زهرة عطرية";
            await service.Update(flower);

            // Assert
            using var context = new ShopContext(options);
            var updatedFlower = await context.Flowers.FindAsync(flower.FlowerId);
            Assert.Equal("Updated Flower", updatedFlower.FlowerName);
            Assert.Equal("36", updatedFlower.FlowertPrice);
            Assert.Equal("زهرة عطرية", updatedFlower.FlowerDescription);
        }


    }
}
