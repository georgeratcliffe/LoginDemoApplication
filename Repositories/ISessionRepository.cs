using LoginDemoApplication.DTOS;
using LoginDemoApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.Repositories
{
    public interface ISessionRepository
    {
        IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters);
    }
}
