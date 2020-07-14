using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginDemoApplication.Services
{
    public interface IAPIService
    {
        public Task<SessionDTO[]> GetAsync(DbSet<Session> sessions, string granu, int InLast);
    }
}
