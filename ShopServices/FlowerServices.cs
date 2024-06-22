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

        public void Delete(Flower flower)
        {
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp != null)
            {
                db.Flowers.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Flower Get(int FlowerId)
        {
            using var db = contextFactory.CreateDbContext();

            var flower = db.Flowers.FirstOrDefault(x => x.FlowerId == FlowerId);
            return flower;
        }

        public List<Flower> GetAll()
        {

            using var db = contextFactory.CreateDbContext();

            return db.Flowers.ToList();
        }

        public List<Flower> GetList(string FlowerName)
        {

            using var db = contextFactory.CreateDbContext();

            var flowers = db.Flowers.Where(x => x.FlowerName.Contains(FlowerName));
            return [.. flowers];

        }

        public void Save(Flower flower)
        {

            using var db = contextFactory.CreateDbContext();

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp == null)
            {
                db.Flowers.Add(flower);
                db.SaveChanges();
            }
        }

        public void Update(Flower flower)
        {
            
            using var db = (contextFactory.CreateDbContext());

            var tmp = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);

            if (tmp != null)
            {
                tmp.FlowerName = flower.FlowerName;
                tmp.FlowertPrice = flower.FlowertPrice;
                tmp.FlowerDescription = flower.FlowerDescription;
               

                db.SaveChanges();
            }
        }
      }
    } 
