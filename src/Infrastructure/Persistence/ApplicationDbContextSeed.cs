using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed Categories
            if (!context.Categories.Any())
            {
                var categoryList = new List<Category>
                    {
                        new Category { Name = "Fruits & Vegetables" },
                        new Category { Name = "Dairy & Eggs" },
                        new Category { Name = "Meat & Seafood" },
                        new Category { Name = "Bakery" },
                        new Category { Name = "Beverages" }
                    };

                await context.Categories.AddRangeAsync(categoryList);
                await context.SaveChangesAsync();
            }

            // Map Category Name → Id
            var categoryMap = await context.Categories
                .ToDictionaryAsync(c => c.Name, c => c.Id);

            // Seed Products
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        SKU = "FV-APL-001",
                        Name = "Red Apples",
                        Description = "Fresh and juicy red apples.",
                        Price = 3.99m,
                        Stock = 50,
                        Rating = 4.5,
                        ImageUrl = "https://example.com/images/apple.jpg",
                        CategoryId = categoryMap["Fruits & Vegetables"]
                    },
                    new Product
                    {
                        SKU = "FV-TMT-002",
                        Name = "Tomatoes",
                        Description = "Organic ripe tomatoes.",
                        Price = 2.49m,
                        Stock = 100,
                        Rating = 4.2,
                        ImageUrl = "https://example.com/images/tomato.jpg",
                        CategoryId = categoryMap["Fruits & Vegetables"]
                    },
                    new Product
                    {
                        SKU = "DE-MIL-003",
                        Name = "Whole Milk",
                        Description = "1L bottle of whole milk.",
                        Price = 1.49m,
                        Stock = 80,
                        Rating = 4.0,
                        ImageUrl = "https://example.com/images/milk.jpg",
                        CategoryId = categoryMap["Dairy & Eggs"]
                    },
                    new Product
                    {
                        SKU = "DE-CHS-004",
                        Name = "Cheddar Cheese",
                        Description = "200g block of aged cheddar.",
                        Price = 4.99m,
                        Stock = 60,
                        Rating = 4.6,
                        ImageUrl = "https://example.com/images/cheese.jpg",
                        CategoryId = categoryMap["Dairy & Eggs"]
                    },
                    new Product
                    {
                        SKU = "MS-CHN-005",
                        Name = "Chicken Breast",
                        Description = "Boneless skinless chicken breasts.",
                        Price = 6.75m,
                        Stock = 40,
                        Rating = 4.3,
                        ImageUrl = "https://example.com/images/chicken.jpg",
                        CategoryId = categoryMap["Meat & Seafood"]
                    },
                    new Product
                    {
                        SKU = "MS-SLM-006",
                        Name = "Fresh Salmon",
                        Description = "Premium Atlantic salmon fillets.",
                        Price = 12.90m,
                        Stock = 30,
                        Rating = 4.8,
                        ImageUrl = "https://example.com/images/salmon.jpg",
                        CategoryId = categoryMap["Meat & Seafood"]
                    },
                    new Product
                    {
                        SKU = "BK-BRD-007",
                        Name = "Sourdough Bread",
                        Description = "Crusty artisan sourdough loaf.",
                        Price = 3.50m,
                        Stock = 25,
                        Rating = 4.7,
                        ImageUrl = "https://example.com/images/bread.jpg",
                        CategoryId = categoryMap["Bakery"]
                    },
                    new Product
                    {
                        SKU = "BK-CRS-008",
                        Name = "Butter Croissant",
                        Description = "Freshly baked French croissant.",
                        Price = 1.25m,
                        Stock = 100,
                        Rating = 4.9,
                        ImageUrl = "https://example.com/images/croissant.jpg",
                        CategoryId = categoryMap["Bakery"]
                    },
                    new Product
                    {
                        SKU = "BV-ORG-009",
                        Name = "Orange Juice",
                        Description = "1L 100% pure squeezed orange juice.",
                        Price = 3.10m,
                        Stock = 70,
                        Rating = 4.4,
                        ImageUrl = "https://example.com/images/orangejuice.jpg",
                        CategoryId = categoryMap["Beverages"]
                    },
                    new Product
                    {
                        SKU = "BV-COF-010",
                        Name = "Ground Coffee",
                        Description = "500g bag of medium roast coffee.",
                        Price = 5.80m,
                        Stock = 45,
                        Rating = 4.6,
                        ImageUrl = "https://example.com/images/coffee.jpg",
                        CategoryId = categoryMap["Beverages"]
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

        }


        /*
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        */

    }
}
