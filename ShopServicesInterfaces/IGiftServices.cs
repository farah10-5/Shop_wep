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
        Task Delete(Gift gift);
        Task<Gift> Get(int GiftI);
        Task<Gift> Get(string GiftName);
        Task<List<Gift>> GetList(string GiftName);
        Task Save(Gift gift);
        Task Update(Gift gift);
        Task<List<Gift>> GetAll();
      
    }
}
