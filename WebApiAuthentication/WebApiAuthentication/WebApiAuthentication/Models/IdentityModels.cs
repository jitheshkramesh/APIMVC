using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiAuthentication.Models
{
    public class ApplicationUser:IdentityUser
    {
    }

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDBContext()
        :base("DefaultConnection",throwIfV1Schema:false)
        { }
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //AspNetUsers=>User
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("User");
        //AspNetRoles=>Role
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("Role");
        //AspNetUserRole=>UserRole
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("UserRole");
        //AspNetUserClaims=>UserClaim
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("UserClaim");
        //AspNetUserLogin=>UserLogin
        modelBuilder.Entity<ApplicationUser>()
            .ToTable("UserLogin");
    }
}