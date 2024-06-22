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
        
         public void Delete(Flower flower);
            public Flower Get(int FlowerId);
            public List<Flower> GetList(string FlowerName);
            public List<Flower> GetAll();
            public void Save(Flower flower);
            public void Update(Flower flower);
        
    }
    }


