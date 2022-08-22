using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interview5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region productInit
            Product telefon = new Product()
            {
                Id = 0,
                Name = "Samsung Telefon",
                Price = 15000,
                Category = new Category
                {
                    Id = 1,
                    Name = "Telefon",
                    Description = "İphone",
                }
            };
            Product tablet = new Product()
            {
                Id = 1,
                Name = "Tablet",
                Price = 10000,
                Category = new Category
                {
                    Id = 2,
                    Name = "Tablet",
                    Description = "Samsung",
                }
            };
            Product laptop = new Product()
            {
                Id = 2,
                Name = "Laptop",
                Price = 25999,
                Category = new Category
                {
                    Id = 3,
                    Name = "Laptop",
                    Description = "HP Pavilion",
                }
            };
            Product Supurge = new Product()
            {
                Id = 3,
                Name = "Supurge",
                Price = 6000,
                Category = new Category
                {
                    Id = 4,
                    Name = "Supurge",
                    Description = "Philips",
                }
            };
            Product Kulaklik = new Product()
            {
                Id = 4,
                Name = "Kulaklik",
                Price = 10000,
                Category = new Category
                {
                    Id = 5,
                    Name = "Kulaklik",
                    Description = "Kablosuz",
                }
            };
            Product Iphone = new Product()
            {
                Id = 5,
                Name = "Iphone Telefon",
                Price = 35000,
                Category = new Category
                {
                    Id = 1,
                    Name = "Telefon",
                    Description = "Iphone",
                }
            };
            #endregion
            #region userInit
            User user = new User()
            {
                Id = 1,
                FirstName = "Zeynep",
                LastName = "Ece",
                CreateDate = DateTime.Now,
                IsEmployee = false,
                Password = "81dc9bdb52d04dc20036dbd8313ed055", //1234
                Mail = "zeynep@gmail.com"
            };
            User loyalUser = new User()
            {
                Id = 1,
                FirstName = "Derin",
                LastName = "Yılmaz",
                CreateDate = DateTime.Now.AddYears(-3),
                IsEmployee = false,
                Password = "b06f4fc112cf9b0ec5a3308649c106c5", //denemesifre
                Mail = "derin@gmail.com"
            };
            User employeeUser = new User()
            {
                Id = 1,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                CreateDate = DateTime.Now.AddYears(-3),
                IsEmployee = true,
                Password = "8cdee5526476b101869401a37c03e379", // sifre
                Mail = "ahmet@gmail.com"
            };
            #endregion
            #region ListInit
            var userList = new List<User>();
            var productList = new List<Product>();
            #endregion
            #region addList
            productList.Add(telefon);
            productList.Add(tablet);
            productList.Add(laptop);
            productList.Add(Supurge);
            productList.Add(Kulaklik);
            productList.Add(Iphone);
            userList.Add(user);
            userList.Add(loyalUser);
            userList.Add(employeeUser);
            #endregion
            //deneme
            bool mailFound = false;
            User loginUser = null;
            while(mailFound == false)
            {
                Console.Write("Lütfen mailinizi giriniz: ");
                var selectedMail = Console.ReadLine();
                loginUser = userList.FirstOrDefault(p => p.Mail == selectedMail);
                if (loginUser == null) { Console.Write("Üye Değilsiniz\n"); }
                else { mailFound = true; }
            }
            
            bool isLoggedIn = false;
            while(isLoggedIn == false)
            {
                Console.Write("Lütfen parolanızı giriniz: ");
                var password = Console.ReadLine();
                using (MD5 md5Hash = MD5.Create())
                {
                    string hashedPassword = GetMd5Hash(md5Hash, password);
                    if (hashedPassword == loginUser.Password) { isLoggedIn = true; }
                    else { Console.Write("Parolanız yanlış\n"); }
                }
            }

            

            foreach (var item in productList)
            {
                Console.WriteLine($"Id: {item.Id}- Ürün Adı: {item.Name}\nFiyatı: {item.Price}");
            }

            Console.Write("Alacağınız ürünü seçiniz: ");
            var selectedIndex = Console.ReadLine();
            Product urun = productList.FirstOrDefault(p => p.Id.ToString() == selectedIndex);

            if (urun.Category.Id == 1)
            {
                Console.WriteLine("Telefon ürünlerinde indirim uygulanamaz.");
                Console.WriteLine("Ödeme yapacağınız tutar: " + telefon.Price);
            }
            else
            {

                Console.WriteLine("1- Gold Card");
                Console.WriteLine("2- Silver Card");
                if (loginUser.IsEmployee == true) { Console.WriteLine("3- Çalışanımız olduğunuz için %10 indirim hakkınızı kullabilirsiniz."); }
                if (loginUser.LoyalCustomer == true) { Console.WriteLine("4- 2 yıllık müşterimiz olduğunuz için %5 indirim hakkınızı kullabilirsiniz."); }
                Console.Write("Lütfen kullanacağınız indirimi seçin: ");

                string selectedDiscount = Console.ReadLine();
                if(selectedDiscount == "1" || selectedDiscount == "2")
                {
                    switch (selectedDiscount)
                    {
                        case "1":
                            GoldCardManager goldCard = new GoldCardManager();
                            goldCard.Discount(urun);

                            break;
                        case "2":
                            SilverCardManager silverCard = new SilverCardManager();
                            silverCard.Discount(urun);

                            break;
                    }
                }

                if(selectedDiscount == "3" && loginUser.IsEmployee == true)
                {
                    urun.Price = urun.Price - (urun.Price * 0.10);
                    Console.WriteLine($"Ödenecek Fiyat: ${urun.Price}");
                }
                if (selectedDiscount == "4" && loginUser.LoyalCustomer == true)
                {
                    urun.Price = urun.Price - (urun.Price * 0.05);
                    Console.WriteLine($"Ödenecek Fiyat: ${urun.Price}");
                }
                
            }             
            Console.ReadLine();



            string GetMd5Hash(MD5 md5Hash, string input)
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
