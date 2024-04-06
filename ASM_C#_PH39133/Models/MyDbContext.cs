using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ASM_C__PH39133.Models
{
	public class MyDbContext : DbContext
	{
        public MyDbContext()
        {
            
        }
        public MyDbContext(DbContextOptions options) : base(options) { }

		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<Customers> Customers { get; set; }
		public virtual DbSet<Categories> Categories { get; set; }
		public virtual DbSet<Cart_detail> CartDetails { get; set; }
		public virtual DbSet<Carts> Carts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Products>(entity =>
			{
				entity.HasIndex(e => e.IMEI).IsUnique();
			});
			modelBuilder.Entity<Carts>(entity =>
			{
				entity.HasIndex(e => e.IDKH).IsUnique();
			});
			modelBuilder.Entity<Customers>(entity =>
			{
				entity.HasIndex(e => e.DienThoai).IsUnique();
			});
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=LAPTOP-ATAN102\\TRONGTAN;Initial Catalog=Db_C#4;Integrated Security=True;TrustServerCertificate=true");
			}
		}

	}
}
