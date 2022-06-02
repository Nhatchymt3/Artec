using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Outsourcing.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data
{
    public class OutsourcingEntities : IdentityDbContext<User>
    {

        public OutsourcingEntities()
            : base("OutsourcingEntities")
        {
        }
        public DbSet<ProductRelationship> ProductRelationships { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductTagMapping> ProductTagMappings { get; set; }
        public DbSet<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPictureMapping> ProductPictureMappings { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<WebsiteAttribute> WebsiteAttributes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<PetProfile> PetProfiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ZaloToken> ZaloToken { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTagMapping> BlogTagMappings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Recruit> Recuits { get; set; }
        public DbSet<MomoModel> MomoModels { get; set; }

        public virtual void Commit()
        {
            try
            {
                
                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

        }


        





 
    }
}