using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnFile.Models;

namespace OnFile.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // 1 File -> * OptionalPicture
            builder.Entity<OptionalPicture>()
                .HasOne(op => op.File)
                .WithMany(f => f.OptionalPictures);

            // 1 BuyedFile -> * Files
            builder.Entity<File>()
                .HasOne(f => f.BuyedFile)
                .WithMany(bf => bf.Files);

            // 1 WishList -> * Files
            builder.Entity<File>()
                .HasOne(f => f.WishList)
                .WithMany(wl => wl.Files);

            // 1 ApplicationUser -> * Files
            builder.Entity<File>()
                .HasOne(f => f.ApplicationUser)
                .WithMany(au => au.Files);

            //
        }
    }
}
