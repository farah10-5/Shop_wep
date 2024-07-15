using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopDomain.Entities;
using ShopPersistance;
using ShopServicesInterfaces;

namespace ShopServices
{
    public class FlowerServices : IFlowerServices
    {
        private readonly IDbContextFactory<ShopContext> contextFactory;

        public FlowerServices(IDbContextFactory<ShopContext> dbContextFactory)
        {
            contextFactory = dbContextFactory;
        }

        public async Task Delete(Flower flower)
        {
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp != null)
            {
                db.Flowers.Remove(tmp);
               await db.SaveChangesAsync();
            }
        }

        public async Task< Flower> Get(int FlowerId)
        {
            using var db = contextFactory.CreateDbContext();

            var flower = await db.Flowers.FirstOrDefaultAsync(x => x.FlowerId == FlowerId);
            return flower;
        }

        public async Task<List<Flower>> GetAll()
        {

            using var db = contextFactory.CreateDbContext();

            return await db.Flowers.ToListAsync();
        }

        public async Task<List<Flower>> GetList(string FlowerName)
        {

            using var db = contextFactory.CreateDbContext();

            var flowers = db.Flowers.Where(x => x.FlowerName.Contains(FlowerName));
            return [..await flowers.ToListAsync()];

        }

        public async Task Save(Flower flower)
        {

            using var db = contextFactory.CreateDbContext();

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp == null)
            {
                db.Flowers.Add(flower);
               await db.SaveChangesAsync();
            }
        }

        public async Task Update(Flower flower)
        {
            
            using var db = (contextFactory.CreateDbContext());

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp != null)
            {
                tmp.FlowerName = flower.FlowerName;
                tmp.FlowertPrice = flower.FlowertPrice;
                tmp.FlowerDescription = flower.FlowerDescription;
               

              await  db.SaveChangesAsync();
            }
        }
      }
    } 
