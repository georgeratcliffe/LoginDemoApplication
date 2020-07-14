using System.Collections.Generic;
using LoginDemoApplication.DTOS;
using LoginDemoApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginDemoApplication.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        // GET: api/<SessionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/<SessionController>
        [HttpPost]
        public IActionResult Post([FromBody] SessionParamsCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest();
            }

            string result = JsonConvert.SerializeObject(_sessionRepository.GetAll(createDTO));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
