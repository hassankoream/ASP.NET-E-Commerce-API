using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding
        (
        StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _IdentitydbContext
        )
        : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if ((PendingMigrations).Any())
                {
                    await _dbContext.Database.MigrateAsync();

                }


                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");

                    //Convert String to C# Objects
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);

                        //_dbContext.SaveChanges(); Wait to Add others

                    }

                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");

                    //Convert String to C# Objects
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);

                        //_dbContext.SaveChanges(); Wait to Add others

                    }

                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");

                    //Convert String to C# Objects
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);

                        //_dbContext.SaveChanges(); Wait to Add others

                    }

                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeilveryMethodData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");

                    //Convert String to C# Objects
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeilveryMethodData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);

                        //_dbContext.SaveChanges(); Wait to Add others

                    }

                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO

            }
        }

        //Seeding requirements:
        //1-Add Roles
        //2-Add users
        //3-Add passwords
        //4-Assign Roles to Users
        public async Task IdentityDataSeedAsync()
        {
            try
            {
                //1-Add Roles

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                //2-Add users
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Hassan",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaHassan"
                    };

                    //to hash passwords using create
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    //Assign Roles

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User01, "SuperAdmin");

                }


                await _IdentitydbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


        }
    }
}
