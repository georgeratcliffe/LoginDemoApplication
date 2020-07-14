using LoginDemoApplication.DTOS;
using System.Collections.Generic;


namespace LoginDemoApplication.Repositories
{
    public interface ISessionRepository
    {
        IList<SessionDTO> GetAll(SessionParamsCreateDTO queryParameters);
    }
}
