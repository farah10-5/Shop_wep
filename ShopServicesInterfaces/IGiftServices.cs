using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Entities;

namespace ShopServicesInterfaces
{
    public interface IGiftServices
    {
        void Delete(Gift gift);
        Gift Get(int GiftI);
        Gift Get(string GiftName);
        List<Gift> GetList(string GiftName);
        void Save(Gift gift);
        void Update(Gift gift);
        List<Gift> GetAll();
      
    }
}
