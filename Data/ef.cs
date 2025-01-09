// using NETCORE.Models;
// using System;
// using System.Linq;

// namespace NETCORE.Data
// {
//     public static class DbInitializer
//     {
//         public static void Initialize(MvcMovieContext context)
//         {
//             context.Database.EnsureCreated();

//             // Kiểm tra nếu có hoa quả nào trong cơ sở dữ liệu
//             if (context.Hoaqua.Any())
//             {
//                 return;   // Nếu đã có dữ liệu, không cần thêm
//             }

//             // Thêm dữ liệu Hoaqua vào cơ sở dữ liệu
//             var hoaquas = new Hoaqua[]
//             {
//                 new Hoaqua { Title = "Apple", Genre = "Fruit", Price = 5000, ImageUrl = "apple.jpg", CartItem = new List<CartItem>() },
//                 new Hoaqua { Title = "Banana", Genre = "Fruit", Price = 3000, ImageUrl = "banana.jpg", CartItem = new List<CartItem>() },
//                 new Hoaqua { Title = "Orange", Genre = "Fruit", Price = 4000, ImageUrl = "orange.jpg", CartItem = new List<CartItem>() },
//                 new Hoaqua { Title = "Mango", Genre = "Fruit", Price = 6000, ImageUrl = "mango.jpg", CartItem = new List<CartItem>() }
//             };

//             foreach (var hoaqua in hoaquas)
//             {
//                 context.Hoaqua.Add(hoaqua);
//             }
//             context.SaveChanges();

//             // Thêm dữ liệu CartItem vào cơ sở dữ liệu
//             var cartItems = new CartItem[]
//             {
//                 new CartItem { HoaquaId = 1, Hoaqua = context.Hoaqua.FirstOrDefault(h => h.Id == 1), Title = "Apple", Genre = "Fruit", Price = 5000, ImageUrl = "apple.jpg", Quantity = 3 },
//                 new CartItem { HoaquaId = 2, Hoaqua = context.Hoaqua.FirstOrDefault(h => h.Id == 2), Title = "Banana", Genre = "Fruit", Price = 3000, ImageUrl = "banana.jpg", Quantity = 2 },
//                 new CartItem { HoaquaId = 3, Hoaqua = context.Hoaqua.FirstOrDefault(h => h.Id == 3), Title = "Orange", Genre = "Fruit", Price = 4000, ImageUrl = "orange.jpg", Quantity = 1 },
//                 new CartItem { HoaquaId = 4, Hoaqua = context.Hoaqua.FirstOrDefault(h => h.Id == 4), Title = "Mango", Genre = "Fruit", Price = 6000, ImageUrl = "mango.jpg", Quantity = 5 }
//             };

//             foreach (var cartItem in cartItems)
//             {
//                 context.CartItem.Add(cartItem);
//             }
//             context.SaveChanges();
//         }
//     }
// }
