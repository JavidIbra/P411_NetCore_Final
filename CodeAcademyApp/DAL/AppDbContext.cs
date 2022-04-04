using CodeAcademyApp.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAcademyApp.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<EducationCategory> EducationCategories { get; set; }
    }
}
