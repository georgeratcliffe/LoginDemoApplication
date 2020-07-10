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
    public class SessionRepository : ISessionRepository
    {
        private readonly DummyApiContext _context;
        public SessionRepository(DummyApiContext context)
        {
            _context = context;
        }

        public IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters)
        {
            var sessions = _context.Sessions;


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
