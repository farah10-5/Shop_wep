using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShopDomain.Entities;
using ShopPersistance;
using ShopServicesInterfaces;

namespace ShopServices
{
    public class GiftServices : IGiftServices
    {
        private readonly IDbContextFactory<ShopContext> contextFactory;

        public GiftServices(IDbContextFactory<ShopContext> dbContextFactory)
        {
            contextFactory = dbContextFactory;
        }

        public async Task Delete(Gift gift)
        {
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp != null)
            {
                db.Gifts.Remove(tmp);
              await  db.SaveChangesAsync();
            }
        }

        public async Task<Gift> Get(int GiftId)
        {
            using var db = contextFactory.CreateDbContext();

            var gift = await db.Gifts.FirstOrDefaultAsync(x => x.GiftId == GiftId);
            return gift;
        }

        public async Task<Gift> Get(string GiftName)
        {
            
            using var db = contextFactory.CreateDbContext();

            var gift = await db.Gifts.FirstOrDefaultAsync(x => x.GiftName.ToUpper() == GiftName.ToUpper());
            return gift;
        }

        public async Task<List<Gift>> GetAll()
        {
            
            using var db = contextFactory.CreateDbContext();

            return await db.Gifts.ToListAsync();
        }

        public async Task<List<Gift>> GetList(string GiftName)
        {
            using var db = contextFactory.CreateDbContext();

            var gifts = await db.Gifts.Where(x => x.GiftName.Contains(GiftName)).ToListAsync();
            return [.. gifts];
        }

        public async Task Save(Gift gift)
        {
            
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp == null)
            {
                db.Gifts.Add(gift);
               await db.SaveChangesAsync();
            }
        }

        public async Task Update(Gift gift)
        {
            
            using var db = (contextFactory.CreateDbContext());

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp != null)
            {
                tmp.GiftName = gift.GiftName;
                tmp.GiftPrice = gift.GiftPrice;
                tmp.GiftType = gift.GiftType;
                tmp.GiftDescription = gift.GiftDescription;

              await  db.SaveChangesAsync();
            }
        }

        public async Task AddFlowerToGift(Gift gift, Flower flower)
        {
            using var db = contextFactory.CreateDbContext();
            var tmpGift = db.Gifts.Include(x => x.Flowers).FirstOrDefault(x => x.GiftId == gift.GiftId);
            if (tmpGift != null)
            {
                var tmpFlower = db.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);
                if (tmpFlower != null)
                {
                    tmpGift.Flowers.Add(tmpFlower);
                }
                else
                {
                    db.Flowers.Add(flower);
                    await db.SaveChangesAsync();
                    tmpGift.Flowers.Add(flower);
                }
                await db.SaveChangesAsync();
            }
        }
        public async Task RemoveFlowerFromGift(Gift gift, Flower flower)
        {
            using var db = contextFactory.CreateDbContext();
            var tmpGift = db.Gifts.Include(x => x.Flowers).FirstOrDefault(x => x.GiftId == gift.GiftId);
            if (tmpGift != null)
            {
                var giftFlower = tmpGift.Flowers.FirstOrDefault(x => x.FlowerId == flower.FlowerId);
                if (giftFlower != null)
                {
                    tmpGift.Flowers.Remove(giftFlower);
                    await db.SaveChangesAsync();
                }
            }
        }

    }





}

