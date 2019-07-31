using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TuringBackend.Models.Data
{
    public partial class TuringBackendContext : DbContext
    {
        public TuringBackendContext()
        {
        }

        public TuringBackendContext(DbContextOptions<TuringBackendContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<AttributeValue> AttributeValue { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<ShippingRegion> ShippingRegion { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Configuration.GetConnectionString("TShopDB");
                optionsBuilder.UseMySql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.ToTable("attribute");

                entity.Property(e => e.AttributeId)
                    .HasColumnName("attribute_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<AttributeValue>(entity =>
            {
                entity.ToTable("attribute_value");

                entity.HasIndex(e => e.AttributeId)
                    .HasName("idx_attribute_value_attribute_id");

                entity.Property(e => e.AttributeValueId)
                    .HasColumnName("attribute_value_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AttributeId)
                    .HasColumnName("attribute_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.HasIndex(e => e.OrderId)
                    .HasName("idx_audit_order_id");

                entity.Property(e => e.AuditId)
                    .HasColumnName("audit_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("idx_category_department_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Email)
                    .HasName("idx_customer_email")
                    .IsUnique();

                entity.HasIndex(e => e.ShippingRegionId)
                    .HasName("idx_customer_shipping_region_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address1)
                    .HasColumnName("address_1")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Address2)
                    .HasColumnName("address_2")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CreditCard)
                    .HasColumnName("credit_card")
                    .HasColumnType("text");

                entity.Property(e => e.DayPhone)
                    .HasColumnName("day_phone")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EvePhone)
                    .HasColumnName("eve_phone")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MobPhone)
                    .HasColumnName("mob_phone")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ShippingRegionId)
                    .HasColumnName("shipping_region_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PRIMARY");

                entity.ToTable("order_detail");

                entity.HasIndex(e => e.OrderId)
                    .HasName("idx_order_detail_order_id");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UnitCost)
                    .HasColumnName("unit_cost")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_orders_customer_id");

                entity.HasIndex(e => e.ShippingId)
                    .HasName("idx_orders_shipping_id");

                entity.HasIndex(e => e.TaxId)
                    .HasName("idx_orders_tax_id");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AuthCode)
                    .HasColumnName("auth_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ShippedOn)
                    .HasColumnName("shipped_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShippingId)
                    .HasColumnName("shipping_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TaxId)
                    .HasColumnName("tax_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => new {e.Name, e.Description})
                    .HasName("idx_ft_product_name_description");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.DiscountedPrice)
                    .HasColumnName("discounted_price")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Image2)
                    .HasColumnName("image_2")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Thumbnail)
                    .HasColumnName("thumbnail")
                    .HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => new {e.ProductId, e.AttributeValueId})
                    .HasName("PRIMARY");

                entity.ToTable("product_attribute");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AttributeValueId)
                    .HasColumnName("attribute_value_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new {e.ProductId, e.CategoryId})
                    .HasName("PRIMARY");

                entity.ToTable("product_category");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_review_customer_id");

                entity.HasIndex(e => e.ProductId)
                    .HasName("idx_review_product_id");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Review1)
                    .IsRequired()
                    .HasColumnName("review")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("shipping");

                entity.HasIndex(e => e.ShippingRegionId)
                    .HasName("idx_shipping_shipping_region_id");

                entity.Property(e => e.ShippingId)
                    .HasColumnName("shipping_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShippingCost)
                    .HasColumnName("shipping_cost")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ShippingRegionId)
                    .HasColumnName("shipping_region_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShippingType)
                    .IsRequired()
                    .HasColumnName("shipping_type")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ShippingRegion>(entity =>
            {
                entity.ToTable("shipping_region");

                entity.Property(e => e.ShippingRegionId)
                    .HasColumnName("shipping_region_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShippingRegion1)
                    .IsRequired()
                    .HasColumnName("shipping_region")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PRIMARY");

                entity.ToTable("shopping_cart");

                entity.HasIndex(e => e.CartId)
                    .HasName("idx_shopping_cart_cart_id");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddedOn)
                    .HasColumnName("added_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.BuyNow)
                    .HasColumnName("buy_now")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CartId)
                    .IsRequired()
                    .HasColumnName("cart_id")
                    .HasColumnType("char(32)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.ToTable("tax");

                entity.Property(e => e.TaxId)
                    .HasColumnName("tax_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxPercentage)
                    .HasColumnName("tax_percentage")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.TaxType)
                    .IsRequired()
                    .HasColumnName("tax_type")
                    .HasColumnType("varchar(100)");
            });
        }


    }
}