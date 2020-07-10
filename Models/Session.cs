using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.Models
{
    public class Session
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Sessiondate { get; set; }
    }
}
