using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using LoginDemoApplication.Services;
using System.Collections.Generic;
using System.Linq;

namespace LoginDemoApplication.Repositories
{
    public class DummySessionRepository : ISessionRepository
    {
        private readonly DummyApiContext _context;
        private readonly IAPIService _apiservice;

        public DummySessionRepository(DummyApiContext context, IAPIService apiservice)
        {
            _context = context;
            _apiservice = apiservice;
        }

        public IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters)
        {
            string granu = queryParameters.Granu.ToLower();
            int InLast = int.Parse(queryParameters.InLast);

            var sessions = _apiservice.GetAsync(_context.Sessions, granu, InLast).Result;

            return sessions.ToList();
        }

    }
}
