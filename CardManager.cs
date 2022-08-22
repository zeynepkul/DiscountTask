using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview5
{
    public abstract class CardManager
    {        
        public abstract void Discount(Product product);

        public void ToPay()
        {
            Console.WriteLine("Ödeme Alındı");
        }
    }
}
