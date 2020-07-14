using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.Services
{
    public class APIService : IAPIService
    {
        public static class ColumnNames
        {
            public const string FromDate = "FromDate";
            public const string ToDate = "ToDate";
            public const string Count = "Count";
        }

        public async Task<SessionDTO[]> GetAsync(DbSet<Session> sessions, string granu, int InLast)
        {
            DateTime today = DateTime.Now;

            DataTable sessiondatatable = new DataTable();
            sessiondatatable.Columns.Add("FromDate", typeof(DateTime));
            sessiondatatable.Columns.Add("ToDate", typeof(DateTime));
            sessiondatatable.Columns.Add("Count", typeof(int));

            PopulateDates(sessiondatatable, granu, InLast);
            await PopulateCounts(sessiondatatable, sessions);

            return sessiondatatable.Rows
                .Cast<DataRow>()
                .Select(row => new SessionDTO()
                {
                    From = row[ColumnNames.FromDate].ToString(),
                    To = row[ColumnNames.ToDate].ToString(),
                    Count = row[ColumnNames.Count].ToString()
                })
                .ToArray();
        }

        private async void PopulateDates(DataTable sessionData, string granularity, int count)
        {
            Enumerable.Range(0, count)
                .Select(r =>
                {
                    var (fromDate, toDate) = GetDates(granularity, r);

                    var row = sessionData.NewRow();
                    row[ColumnNames.FromDate] = fromDate;
                    row[ColumnNames.ToDate] = toDate;

                    return row;
                })
                .ToList()
                .ForEach(row => sessionData.Rows.Add(row));
        }

        private async Task PopulateCounts(DataTable sessionData, DbSet<Session> sessions)
        {
            foreach(var sessionRow in sessionData.Rows.Cast<DataRow>())
            {
                var fromDate = (DateTime)sessionRow[ColumnNames.FromDate];
                var toDate = (DateTime)sessionRow[ColumnNames.ToDate];

                var count = await sessions
                    .CountAsync(session =>
                        (session.Sessiondate >= fromDate)
                        && (session.Sessiondate <= toDate)
                    );

                sessionRow[ColumnNames.Count] = count;
            }
        }

        private (DateTime, DateTime)  GetDates(string granularity, int rowNo)
        {
            (DateTime, DateTime) GetYearDates()
            {
                var initialDate = DateTime.UtcNow.AddYears(rowNo * -1);
                var fromDate = new DateTime(initialDate.Year, 1, 1, 0, 0, 0);
                var toDate = new DateTime(initialDate.Year, 12, 31, 23, 59, 59);

                return (fromDate, toDate);
            }

            (DateTime, DateTime) GetMonthDates()
            {
                var initialDate = DateTime.UtcNow.AddMonths(rowNo * -1);
                var fromDate = new DateTime(initialDate.Year, initialDate.Month, 1, 0, 0, 0);
                var toDate = new DateTime(initialDate.Year, initialDate.Month, DateTime.DaysInMonth(initialDate.Year, initialDate.Month), 23, 59, 59);

                return (fromDate, toDate);
            }

            (DateTime, DateTime) GetDayDates()
            {
                var initialDate = DateTime.Now.AddDays(rowNo * -1);
                var fromDate = new DateTime(initialDate.Year, initialDate.Month, initialDate.Day, 0, 0, 0);
                var toDate = new DateTime(initialDate.Year, initialDate.Month, initialDate.Day, 23, 59, 59);

                return (fromDate, toDate);
            }

            return granularity switch
            {
                "year" => GetYearDates(),
                "month" => GetMonthDates(),
                "day" => GetDayDates(),
                _ => throw new NotSupportedException($"The {granularity} granularity is not supported.")
            };
        }
    }
}
