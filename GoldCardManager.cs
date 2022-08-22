using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview5
{
    public class GoldCardManager : CardManager
    {
        public override void Discount(Product product)
        {
            Console.WriteLine("Ürün fiyatı: " + product.Price);
            double cardDiscound = (product.Price - (product.Price * 0.03));
            double generalDiscound = cardDiscound - (Math.Floor(cardDiscound / 200) * 5);
            Console.WriteLine("Gold Kart indirimi: " + cardDiscound);
            Console.Write("200$'a 5$ indirim: " + generalDiscound);
        }
    }
}
