using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{
	public class MainDBContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Interest> Interests { get; set; }
		public DbSet<Field> Fields { get; set; }
		public MainDBContext() : base()
		{
			
		}
		public MainDBContext(DbContextOptions options)
			: base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer("Server=localhost\\MSSQLLocalDB;Database=MainDb;Uid=root;Pwd=root");
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
