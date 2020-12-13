/* Name - Manjinder Singh
 * Date - December 11, 2020
 * Course - NETD3202
 * Description - This is the model page which shows all the categories a person can choose from
 *               while adding to the discussion.
*/
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscussionForm.ViewModels;

namespace DiscussionForm.Models
{
    public class AppDBContext:IdentityDbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options):
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //initial list of categories 
            modelBuilder.Entity<Category>().HasData(
                new Category {
                    CategoryID = 1,
                    Name="Transport issues"
                },
                new Category {
                    CategoryID = 2,
                    Name="Senior Management"
                },
                new Category
                {
                    CategoryID = 3,
                    Name = "Employee Benifit"
                },
                 new Category
                 {
                     CategoryID = 4,
                     Name = "Others"
                 }
                );
        }

        public DbSet<DiscussionForm.ViewModels.RegisterViewModel> RegisterViewModel { get; set; }

        public DbSet<DiscussionForm.ViewModels.LoginViewModel> LoginViewModel { get; set; }

        public DbSet<DiscussionForm.ViewModels.ResetPassword> ResetPassword { get; set; }

        public DbSet<DiscussionForm.ViewModels.ResetPasswordViewModel> ResetPasswordViewModel { get; set; }


    }
}
