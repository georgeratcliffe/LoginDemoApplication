using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.Repositories
{
    public class DummySessionRepository : ISessionRepository
    {
        private ApiContext _context;
        public DummySessionRepository()
        {
            //_context = context;
        }

        //public IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters)
        //{

        //    var result = new List<SessionDTO>()
        //    {
        //        new SessionDTO() {from = "01/01/2020", to = "01/02/200", count="5"},
        //        new SessionDTO() {from = "01/02/2020", to = "01/03/200", count="12"},
        //        new SessionDTO() {from = "01/03/2020", to = "01/04/200", count="38"}
        //    };

        //    return result;
        //}

        public IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters)
        {

            var sessions = new List<Session>()
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
                new Session() {Username = "Ringo", Email = "ringo@abc.com", Sessiondate= new DateTime(2020,7,2)}
            };

            //apicontext.Sessions.AddRange(testdata);
            //apicontext.SaveChanges();
            //var result = _context.Sessions.ToList();
            //var result = _context.GetSessions();

            string granu = queryParameters.Granu.ToLower();
            int InLast = int.Parse(queryParameters.InLast);
            DateTime today = DateTime.Now;

            DataTable sessiondatatable = new DataTable();

            sessiondatatable.Columns.Add("IntialDate", typeof(DateTime));
            sessiondatatable.Columns.Add("FromDate", typeof(DateTime));
            sessiondatatable.Columns.Add("ToDate", typeof(DateTime));
            sessiondatatable.Columns.Add("Count", typeof(int));

            for (int x = 0; x < InLast; x++)
            {
                if (granu == "year")
                {
                    sessiondatatable.Rows.Add(today.AddYears(x * -1), null, null, 0);
                }
                else if (granu == "month")
                {
                    sessiondatatable.Rows.Add(today.AddMonths(x * -1), null, null, 0);
                }
                else if (granu == "day")
                {
                    sessiondatatable.Rows.Add(today.AddDays(x * -1), today.AddDays(x * -1), today.AddDays(x * -1), 0);
                }
            }

            foreach (DataRow row in sessiondatatable.Rows)
            {
                if (granu == "year")
                {
                    var yr = row.Field<DateTime>(0).Year;
                    row["FromDate"] = new DateTime(yr, 1, 1, 0, 0, 0);
                    row["ToDate"] = new DateTime(yr, 12, 31, 23, 59, 59);
                }
                else if (granu == "month")
                {
                    var yr = row.Field<DateTime>(0).Year;
                    var mon = row.Field<DateTime>(0).Month;
                    row["FromDate"] = new DateTime(yr, mon, 1, 0, 0, 0);
                    row["ToDate"] = new DateTime(yr, mon, DateTime.DaysInMonth(yr, mon), 23, 59, 59);
                }
                else if (granu == "day")
                {
                    var yr = row.Field<DateTime>(0).Year;
                    var mon = row.Field<DateTime>(0).Month;
                    var day = row.Field<DateTime>(0).Day;
                    row["FromDate"] = new DateTime(yr, mon, day, 0, 0, 0);
                    row["ToDate"] = new DateTime(yr, mon, day, 23, 59, 59);
                }
            }

            foreach (Session session in sessions)
            {
                foreach (DataRow row in sessiondatatable.Rows)
                {
                    if (session.Sessiondate >= (DateTime)row["FromDate"] && session.Sessiondate <= (DateTime)row["ToDate"])
                    {
                        int count = (int)row.Field<int>(3);
                        row["Count"] = ++count;
                    }
                }
            }

            var sessionDTOs = new List<SessionDTO>();

            foreach (DataRow row in sessiondatatable.Rows)
            {
                sessionDTOs.Add(
                    new SessionDTO()
                    {
                        From = row[1].ToString(),
                        To = row[2].ToString(),
                        Count = row[3].ToString()
                    });
            }

            return sessionDTOs;
        }

    }
}
