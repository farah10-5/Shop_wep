using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDomain.Entities;

namespace ShopServicesInterfaces
{
    public interface IFlowerServices
    {
        
          Task Delete(Flower flower);
             Task <Flower> Get(int FlowerId);
             Task< List<Flower>> GetList(string FlowerName);
             Task<List<Flower>> GetAll();
             Task Save(Flower flower);
             Task Update(Flower flower);
        
    }
    }


