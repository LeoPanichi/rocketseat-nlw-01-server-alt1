using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server
{
    public class DataContext : DbContext{
        public DataContext() : base()
        { }

        public DataContext(DbContextOptions options) : base (options)
        { }

        // dotnet ef database update 0 [ --context dbcontextname ]  -- Reset DB
        // dotnet ef migrations add Initialize                      -- Create initial migration
        // dotnet ef database update                                -- Run migrations on DB

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
            (
                "Password=SqlServer2019!;Persist Security Info=True;User ID=sa;Data Source=localhost;Trusted_Connection=False; TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item(
                    "baterias.svg",
                    "Baterias"
                )
                {
                    Id = 1
                },
                new Item(
                    "eletronicos.svg",
                    "Eletrônicos"
                )
                {
                    Id = 2
                },
                new Item(
                    "lampadas.svg",
                    "Lâmpadas"
                )
                {
                    Id = 3
                },
                new Item(
                    "oleo.svg",
                    "Óleo"
                )
                {
                    Id = 4
                },
                new Item(
                    "organicos.svg",
                    "Orgânicos"
                )
                {
                    Id = 5
                },
                new Item(
                    "papeis-papelao.svg",
                    "Papéis e Papelão"
                )
                {
                    Id = 6
                }
            );

            modelBuilder.Entity<Point>()
            .HasMany(e => e.Item)
            .WithMany(e => e.Point)
            .UsingEntity<Point2Item>(
                l => l.HasOne<Item>().WithMany().HasForeignKey(e => e.ItemId),
                r => r.HasOne<Point>().WithMany().HasForeignKey(e => e.PointId));
        }

        public DbSet<Point> Points { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Point2Item> Point2Items { get; set; }
    }
}