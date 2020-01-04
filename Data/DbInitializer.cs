using System.Collections.Generic;
using System.Linq;
using Aurora.Data.Models;
 
namespace Aurora.Data
{
    public static class DbInitializer
    {
        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Rings", Description="All rings" },
                        new Category { CategoryName = "Braslets", Description="All braslets" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
        public static void Initialize(AppDbContext context)
        {
          if (!context.Products.Any())
            {
                context.Products.AddRange(
                     new Product {
                        Name = "Кольцо ANNA MASLOVSKAYA",
                        Price = 7.95M, ShortDescription = "Кольцо anna.m.objects Серебро цельное волна",
                        LongDescription = "Еще не придумала",
                        Category = Categories["Rings"],
                        ImageUrl = "https://images.asos-media.com/products/zolotistoe-koltso-s-iskusstvennym-zhemchugom-i-abstraktnoj-otdelkoj-kamnyami-asos-design/11762239-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = true,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/zolotistoe-koltso-s-iskusstvennym-zhemchugom-i-abstraktnoj-otdelkoj-kamnyami-asos-design/11762239-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product {
                        Name = "Кольцо STELLAR JEWELLERY",
                        Price = 12.95M, ShortDescription = "Кольцо STELLAR JEWELLERY ",
                        LongDescription = "Еще не придумала",
                        Category = Categories["Rings"],
                        ImageUrl = "https://images.asos-media.com/products/serebristoe-pletenoe-koltso-asos-design/11890373-1-rhodium?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct= false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/serebristoe-pletenoe-koltso-asos-design/11890373-1-rhodium?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product {
                        Name = "Кольцо Exclusive",
                        Price = 12.95M, 
                        ShortDescription = "Кольцо Exclusive ",
                        LongDescription = "Еще не придумала",
                        Category = Categories["Rings"],
                        ImageUrl = "https://images.asos-media.com/products/zolotistoe-koltso-na-bolshoj-palets-s-dekorativnymi-uzlami-asos-design/11020194-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct= false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/zolotistoe-koltso-na-bolshoj-palets-s-dekorativnymi-uzlami-asos-design/11020194-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product {
                        Name = "Кольцо Vendetta",
                        Price = 9.95M, 
                        ShortDescription = "Кольцо Vendetta",
                        LongDescription = "Еще не придумала",
                        Category = Categories["Rings"],
                        ImageUrl = "https://images.asos-media.com/products/nabor-kolets-pieces/12168654-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct= false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/nabor-kolets-pieces/12168654-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product {
                        Name = "Кольцо Victoria",
                        Price = 15.95M, 
                        ShortDescription = "Кольцо Victoria",
                        LongDescription = "Еще не придумала",
                        Category = Categories["Rings"],
                        ImageUrl = "https://images.asos-media.com/products/fakturnoe-koltso-s-minimalistskim-dizajnom-asos-design/11784745-1-rhodium?$n_320w$&wid=317&fit=constrainn",
                        InStock = true,
                        IsPreferredProduct= false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/fakturnoe-koltso-s-minimalistskim-dizajnom-asos-design/11784745-1-rhodium?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product {
                        Name = "Браслет Pieces",
                        Price = 12.95M, 
                        ShortDescription = "aejgbwehgkg",
                        LongDescription = "smth",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/pletenyj-braslet-s-zolotistymi-gravirovannymi-monetami-asos-design/11752489-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/pletenyj-braslet-s-zolotistymi-gravirovannymi-monetami-asos-design/11752489-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product
                    {
                        Name = "Браслет White",
                        Price = 16.75M,
                        ShortDescription = "A very elegant white braslet",
                        LongDescription = "segnwjwglwbgwgkl",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/zolotistyj-bambukovyj-braslet-liars-lovers/11741680-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/zolotistyj-bambukovyj-braslet-liars-lovers/11741680-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product
                    {
                        Name = "Браслет Asos",
                        Price = 16.75M,
                        ShortDescription = "A very elegant white braslet",
                        LongDescription = "segnwjwglwbgwgkl",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/zolotistyj-braslet-manzheta-s-poludragotsennymi-kamnyami-i-vyrezami-asos-design/11785194-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/zolotistyj-braslet-manzheta-s-poludragotsennymi-kamnyami-i-vyrezami-asos-design/11785194-1-gold?$n_320w$&wid=317&fit=constrain"
                    },

                    new Product
                    {
                        Name = "Браслет Qwerty",
                        Price = 20.75M,
                        ShortDescription = "A very elegant white braslet",
                        LongDescription = "segnwjwglwbgwgkl",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/braslet-manzheta-s-kovannym-effektom-asos-design/11752564-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/braslet-manzheta-s-kovannym-effektom-asos-design/11752564-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product
                    {
                        Name = "Браслет Asdf",
                        Price = 20.45M,
                        ShortDescription = "A very elegant white braslet",
                        LongDescription = "segnwjwglwbgwgkl",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/braslet-manzheta-asos-design/11752525-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/braslet-manzheta-asos-design/11752525-1-gold?$n_320w$&wid=317&fit=constrain"
                    },
                    new Product
                    {
                        Name = "Браслет Aurora",
                        Price = 16.75M,
                        ShortDescription = "A very elegant white braslet",
                        LongDescription = "segnwjwglwbgwgkl",
                        Category = Categories["Braslets"],
                        ImageUrl = "https://images.asos-media.com/products/zolotistyj-braslet-manzhet-s-gravirovkoj-asos-design/11109165-1-gold?$n_320w$&wid=317&fit=constrain",
                        InStock = true,
                        IsPreferredProduct = false,
                        ImageThumbnailUrl = "https://images.asos-media.com/products/zolotistyj-braslet-manzhet-s-gravirovkoj-asos-design/11109165-1-gold?$n_320w$&wid=317&fit=constrain"
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}