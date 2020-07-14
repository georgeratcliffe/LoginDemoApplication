using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using LoginDemoApplication.Services;
using System.Collections.Generic;

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

            var sessions = _context.Sessions;
            return _apiservice.GenerateDTOS(sessions, granu, InLast);
        }

    }
}
