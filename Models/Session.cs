using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.Models
{
    public class Session
    {
        [Key]
        public int SessionKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Sessiondate { get; set; }
    }
}
