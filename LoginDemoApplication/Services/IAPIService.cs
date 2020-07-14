using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoginDemoApplication.Services
{
    public interface IAPIService
    {
        public IList<SessionDTO> GenerateDTOS(DbSet<Session> sessions, string granu, int InLast);
    }
}
