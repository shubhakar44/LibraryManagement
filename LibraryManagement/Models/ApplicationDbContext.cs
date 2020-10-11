using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BookStore> BookStores { get; set; }

        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserBook>()
                .HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<UserBook>()
                .HasOne(bk => bk.BookStores)
                .WithMany(ub => ub.UserBooks)
                .HasForeignKey(id => id.BookId);

            builder.Entity<UserBook>()
                .HasOne(bk => bk.Users)
                .WithMany(ub => ub.UserBooks)
                .HasForeignKey(id => id.UserId);
        }
    }
}
