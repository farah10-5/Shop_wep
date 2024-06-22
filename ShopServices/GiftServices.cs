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

        public void Delete(Gift gift)
        {
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp != null)
            {
                db.Gifts.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Gift Get(int GiftId)
        {
            using var db = contextFactory.CreateDbContext();

            var gift = db.Gifts.FirstOrDefault(x => x.GiftId == GiftId);
            return gift;
        }

        public Gift Get(string GiftName)
        {
            
            using var db = contextFactory.CreateDbContext();

            var gift = db.Gifts.FirstOrDefault(x => x.GiftName.ToUpper() == GiftName.ToUpper());
            return gift;
        }

        public List<Gift> GetAll()
        {
            
            using var db = contextFactory.CreateDbContext();

            return db.Gifts.ToList();
        }

        public List<Gift> GetList(string GiftName)
        {
            using var db = contextFactory.CreateDbContext();

            var gifts = db.Gifts.Where(x => x.GiftName.Contains(GiftName));
            return [.. gifts];
        }

        public void Save(Gift gift)
        {
            
            using var db = contextFactory.CreateDbContext();

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp == null)
            {
                db.Gifts.Add(gift);
                db.SaveChanges();
            }
        }

        public void Update(Gift gift)
        {
            
            using var db = (contextFactory.CreateDbContext());

            var tmp = db.Gifts.FirstOrDefault(x => x.GiftId == gift.GiftId);

            if (tmp != null)
            {
                tmp.GiftName = gift.GiftName;
                tmp.GiftPrice = gift.GiftPrice;
                tmp.GiftType = gift.GiftType;
                tmp.GiftDescription = gift.GiftDescription;

                db.SaveChanges();
            }
        }
    }





}

