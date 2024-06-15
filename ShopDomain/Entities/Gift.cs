using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDomain.Entities
{
    public class Gift
    {
        public int GiftId { get; set; }
        public int GiftType { get; set; }
        public string GiftName { get; set; }
        public string GiftDescription { get; set; }
        public string GiftPrice { get; set; }
        public List<Flower> Flowers { get; set; }



    }
}
