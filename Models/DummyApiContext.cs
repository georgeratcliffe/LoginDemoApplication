using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoginDemoApplication.Models
{
    public class DummyApiContext : DbContext
    {
        public DummyApiContext(DbContextOptions<DummyApiContext> options) : base(options)
        {
            LoadSessions();
        }
        public DbSet<Session> Sessions { get; set; }

        public void LoadSessions()
        {
            IList<Session> sessions = new List<Session>()
            {
               new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2019,1,1)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2019,1,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,24)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,2,5)},
                new Session() {Username = "Paul", Email = "paul@abc.com", Sessiondate= new DateTime(2020,1,1)},
                new Session() {Username = "George", Email = "george@abc.com", Sessiondate= new DateTime(2020,3,19)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2017,3,1)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2017,7,10)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,7,9)},
                new Session() {Username = "Bill", Email = "bill@abc.com", Sessiondate= new DateTime(2020,7,9)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2016,2,12)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,7,9)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2020,7,2)},
                new Session() {Username = "Sam", Email = "sam@abc.com", Sessiondate= new DateTime(2020,1,23)},
                new Session() {Username = "John", Email = "jon@abc.com", Sessiondate= new DateTime(2019,12,27)},
                new Session() {Username = "Ringo", Email = "ringo@abc.com", Sessiondate= new DateTime(2020,7,2)}
            };

            Sessions.AddRange(sessions);
            SaveChanges();
        }

    }
}
