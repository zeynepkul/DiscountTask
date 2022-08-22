using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview5
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsEmployee { get; set; }
        public bool LoyalCustomer => DateTime.UtcNow.Year - CreateDate.Year >= 2 ? true : false;
        public CardManager Card { get; set; }
        public string Password { get; set; }
    }
}
