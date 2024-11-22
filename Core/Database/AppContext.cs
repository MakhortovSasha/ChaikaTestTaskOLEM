using Chaika_TestTask.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Chaika_TestTask.Core.Database
{
    public class AppDBContext:DbContext
    {
        public AppDBContext()
        : base("Server=DESKTOP-AKO2KR8\\SQLEXPRESS;Database=Chaika;Trusted_Connection=True;User=UserName;Password=b")
        {

        }
        public AppDBContext( string connectionString)
        : base(connectionString)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }
        public DbSet<TransactionIcon>? TransactionIcons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Transaction>()
                .HasRequired(c => c.RelatedUser)
                .WithMany(o => o.PrimaryTransactionList);

            modelBuilder.Entity<Transaction>()
                .HasOptional(c => c.AuthorizedUser)
                .WithMany(o => o.DependentTransactionList);

            //modelBuilder.Entity<Transaction>().HasRequired(t => t.RelatedUser);
            //modelBuilder.Entity<Transaction>().HasOptional(t => t.AuthorizedUser);

            //modelBuilder.Entity<User>()
            //    .HasMany(c => c.TransactionList)
            //    .WithOptional(o => o.RelatedUser);

            modelBuilder.Entity<Transaction>()
                .HasOptional(t=>t.Icon)
                .WithMany();
        }
    }
}
