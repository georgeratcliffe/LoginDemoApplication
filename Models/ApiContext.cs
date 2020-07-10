using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoginDemoApplication.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            LoadSessions();
        }
        public DbSet<Session> Sessions { get; set; }




        //public DbSet<Category> Categories { get; set; }

        //public ApiContext(DbContextOptions options) : base(options)
        //{
        //    LoadCategories();
        //}

        //public void LoadCategories()
        //{
        //    Category category = new Category() { CategoryName = "Category1" };
        //    Categories.Add(category);
        //    category = new Category() { CategoryName = "Category2" };
        //    Categories.Add(category);
        //}


        public void LoadSessions()
        {
            var testdata = new List<Session>()
            {
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,1,1)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,1,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,24)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,2,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,1)},
                new Session() {Username = "George", Email = "george@abc.com", Sessiondate= new DateTime(2020,3,19)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,3,1)},
            };

            Sessions.AddRange(testdata);

        }



        //public List<Category> GetCategories()
        //{
        //    return Categories.Local.ToList<Category>();
        //}

        //public List<Session> GetSessions()
        //{
        //    return Sessions.ToList();
        //}









        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
