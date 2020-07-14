using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using LoginDemoApplication.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Xunit;

namespace LoginDemoApplication.Tests.Services
{
    public class APIServiceTests
    {
        #region Helpers

        private IAPIService CreateService()
        {
            return new APIService();
        }

        private DummyApiContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DummyApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DummyApiContext(options);
        }

        private async Task AssertSessions(string granularity, int count, SessionDTO[] expected)
        {
            var service = CreateService();

            using(var context = CreateContext())
            {
                var actual = await service.GetAsync(context.Sessions, granularity, count);
                Assert.Equal(expected, actual);
            }
        }

        #endregion

        [Fact]
        public async Task Throws_Exception_For_Incorrect_Granularity()
        {
            var error = true;

            try
            {
                var context = CreateContext();
                var service = CreateService();

                var _ = await service.GetAsync(context.Sessions, "quarter", 4);

                error = false;
            }
            catch(NotSupportedException)
            {
                error = true;
            }

            Assert.True(error);
        }

        [Fact]
        public Task Returns_Correct_Values_For_Year()
        {
            return AssertSessions("year", 1, new SessionDTO[]
            {
                new SessionDTO()
                {
                    From = "01/01/2020 00:00:00",
                    To = "31/12/2020 23:59:59",
                    Count = "10"
                }
            });
        }

        [Fact]
        public Task Returns_Correct_Values_For_Month()
        {
            return AssertSessions("month", 3, new SessionDTO[]
            {
                new SessionDTO()
                {
                    From = "01/07/2020 00:00:00",
                    To = "31/07/2020 23:59:59",
                    Count = "5"
                },
                new SessionDTO()
                {
                    From = "01/06/2020 00:00:00",
                    To = "30/06/2020 23:59:59",
                    Count = "0"
                },
                new SessionDTO()
                {
                    From = "01/05/2020 00:00:00",
                    To = "31/05/2020 23:59:59",
                    Count = "0"
                }
            });
        }
    }
}
