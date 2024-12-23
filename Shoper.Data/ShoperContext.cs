﻿using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Shoper.Entities;
using System.Data;
using System.Net.Mail;

namespace Shoper.Data
{
    
    public class ShoperContext : IdentityDbContext<AppUser>,IDataProtectionKeyContext
    {
        public ShoperContext()
        {

        }
        public DbSet<Manifacture> Manifactures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductItemValue> ProductItemValues { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //home-windows
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-LRL95PE\SQLEXPRESS;Database=ShoperDb;Trusted_Connection=True;TrustServerCertificate=True");
            //docker home Mac
            //optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=ShoperDb;User Id=SA;Password=Mhmsoft123;MultipleActiveResultSets=true;TrustServerCertificate=true");

             optionsBuilder.UseSqlServer(@"Server=sqldata,1433;Database=ShoperDb;User=sa;Password=Mhmsoft123;MultipleActiveResultSets=true;TrustServerCertificate=True",options=>options.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null));
            
            // course
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-A6PJE9T\SQLEXPRESS;Database=ShoperDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tablo ismi ve tabloya ait primary key verilir.
            modelBuilder.Entity<Manifacture>().ToTable("Manifacture").HasKey(c => c.ManifactureId);
            modelBuilder.Entity<Category>().ToTable("Category").HasKey(c=>c.CategoryId);
            modelBuilder.Entity<Product>().ToTable("Product").HasKey(p=>p.ProductId);
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrice").HasKey(pp=>pp.PriceId);
            modelBuilder.Entity<ProductImage>().ToTable("ProductImage").HasKey(pi => pi.ImageId);
            modelBuilder.Entity<ProductDiscount>().ToTable("ProductDiscount").HasKey(d=>d.ProductDiscountId);
            modelBuilder.Entity<ProductComment>().ToTable("ProductComment").HasKey(d => d.CommentId);
            modelBuilder.Entity<ProductItem>().ToTable("ProductItem").HasKey(d => d.ItemId);
            modelBuilder.Entity<ProductItemValue>().ToTable("ProductItemValue").HasKey(d => new { d.ItemValueId, d.ItemId });
            modelBuilder.Entity<Address>().ToTable("Address").HasKey(a=>a.AddressId);
            modelBuilder.Entity<Customer>().ToTable("Customer").HasKey(a => a.CustomerId);
            modelBuilder.Entity<Order>().ToTable("Order").HasKey(a => a.OrderId);
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail").HasKey(a => a.OrderDetailId);
            modelBuilder.Entity<Slider>().ToTable("Slider").HasKey(a => a.sliderId);
            modelBuilder.Entity<WishList>().ToTable("WishList").HasKey(a => a.wishListId);
            modelBuilder.Entity<Coupon>().ToTable("Coupon").HasKey(a => a.couponId);

            modelBuilder.Entity<ProductItemValue>().Property(d => d.ItemValueId).UseIdentityColumn(1,1);
            modelBuilder.Entity<Order>().Property(d => d.OrderId).UseIdentityColumn(100, 1);
            modelBuilder.Entity<ProductPrice>().Property(d => d.Price).HasPrecision(8,2);
            // Relations
            //category-product
            modelBuilder.Entity<Manifacture>()
                .HasMany<Product>(c => c.Products)
                .WithOne(p => p.Manifacture)
                .HasForeignKey(p => p.ManifactureId)
                .HasConstraintName("Fk_ProductToManifacture")
                .OnDelete(DeleteBehavior.Cascade);
            //category-product
            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.CategoryId)
                .HasConstraintName("Fk_ProductToCategory")
                .OnDelete(DeleteBehavior.Cascade);
            // productPrice-product
            modelBuilder.Entity<ProductPrice>()
                .HasOne<Product>(pp => pp.Product)
                .WithMany(p => p.ProductPrice)
                .HasForeignKey(pp => pp.ProductId)
                .HasConstraintName("Fk_ProductPriceToProduct");
            //product-productImage           
            modelBuilder.Entity<ProductImage>()
                .HasOne<Product>(pp => pp.Product)
                .WithMany(p => p.ProductImage)
                .HasForeignKey(pp => pp.ProductId)
                .HasConstraintName("Fk_ProductImageToProduct");
            //product-productDiscount
            modelBuilder.Entity<ProductDiscount>()
               .HasOne<Product>(pp => pp.Product)
               .WithMany(d => d.ProductDiscount)
               .HasForeignKey(pp => pp.ProductId)
               .HasConstraintName("Fk_ProductDiscountToProduct");
            //product-productComment
            modelBuilder.Entity<ProductComment>()
               .HasOne<Product>(pp => pp.Product)
               .WithMany(d => d.ProductComment)
               .HasForeignKey(pp => pp.ProductId)
               .HasConstraintName("Fk_ProductCommentToProduct");
            //category-productItem
            modelBuilder.Entity<ProductItem>()
               .HasOne<Category>(pp => pp.Category)
               .WithMany(d => d.ProductItem)
               .HasForeignKey(pp => pp.CategoryId)
               .HasConstraintName("Fk_ProductItemToCategory");
            //product-productItemValue            
            modelBuilder.Entity<ProductItemValue>()
               .HasOne<Product>(pp => pp.Product)
               .WithMany(d => d.ProductItemValue)
               .HasForeignKey(pp => pp.ProductId)
               .HasConstraintName("Fk_ProductItemValueToProduct");
            //productItemValue-productItem
            modelBuilder.Entity<ProductItemValue>()
               .HasOne(pp => pp.ProductItem)
               .WithOne(d => d.ProductItemValue)
               .HasForeignKey<ProductItemValue>(pp => pp.ItemId)
               .HasConstraintName("Fk_ProductItemValueToProductItem").OnDelete(DeleteBehavior.NoAction);
            //Customer-Address
            modelBuilder.Entity<Address>()
              .HasOne<Customer>(pp => pp.Customer)
              .WithMany(d => d.Addresses)
              .HasForeignKey(pp => pp.CustomerId)
              .HasConstraintName("Fk_CustomerToAddress");
            //Customer-Order
            modelBuilder.Entity<Order>()
              .HasOne<Customer>(pp => pp.Customer)
              .WithMany(d => d.Orders)
              .HasForeignKey(pp => pp.CustomerId)
              .HasConstraintName("Fk_CustomerToOrder");
            //OrderDetail-Order
            modelBuilder.Entity<OrderDetail>()
              .HasOne<Order>(pp => pp.Order)
              .WithMany(d => d.OrderDetail)
              .HasForeignKey(pp => pp.OrderId)
              .HasConstraintName("Fk_OrderDetailToOrder");

            //OrderDetail-Product
            modelBuilder.Entity<OrderDetail>()
              .HasOne<Product>(pp => pp.Product)
              .WithMany(d => d.OrderDetail)
              .HasForeignKey(pp => pp.ProductId)
              .HasConstraintName("Fk_OrderDetailToProduct");

            modelBuilder.Entity<Customer>()
                .HasOne<AppUser>(x => x.User)
                .WithOne(y => y.Customer)
                .HasForeignKey<Customer>(t => t.UserId);

            modelBuilder.Entity<Slider>()
               .HasOne<Category>(x => x.Category)
               .WithMany(y => y.Slider)
               .HasForeignKey(t => t.categoryId)
               .HasConstraintName("Fk_SliderToCategory"); 

            modelBuilder.Entity<Coupon>()
                .HasOne<Customer>(x => x.Customer)
                .WithMany(x => x.Coupons)
                .HasForeignKey(x => x.customerId)
                .HasConstraintName("Fk_CouponsToCustomer");

            modelBuilder.Entity<WishList>()
                .HasOne<Customer>(x=>x.Customer)
                .WithMany(x=>x.WishLists)
                .HasForeignKey(x => x.customerId)
                .HasConstraintName("Fk_WishListsToCustomer");

            modelBuilder.Entity<WishList>()
                .HasOne<Product>(x => x.product)
                .WithMany(x => x.WishLists)
                .HasForeignKey(x => x.productId)
                .HasConstraintName("Fk_WishListsToProduct");

            // SEED DATA
             modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Manager", NormalizedName = "MANAGER".ToUpper() });
             modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "4dfsdfsd-3b0e-446f-86af-483d56fd7211", Name = "Customer", NormalizedName = "CUSTOMER".ToUpper() });
             
             var hasher = new PasswordHasher<IdentityUser>();
            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "dibutra",
                    fullName=   "Dibutra",
                    Email = "dibutra@gmail.com",
                    NormalizedUserName = "DIBUTRA",
                    EmailConfirmed=true,
                    PasswordHash = hasher.HashPassword(null, "Test.123")
                }
            );
            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

            base.OnModelCreating(modelBuilder);// identity tablolarını oluşturmak için
        }
    }
}