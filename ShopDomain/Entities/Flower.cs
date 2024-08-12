using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Entities
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public string FlowerDescription { get; set; }
        public string FlowertPrice { get; set; }
        public List<Gift> Gifts { get; set; }


    }
}
