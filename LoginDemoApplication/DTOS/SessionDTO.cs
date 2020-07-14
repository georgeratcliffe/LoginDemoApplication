
using System;

namespace LoginDemoApplication.DTOS
{
    public class SessionDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Count { get; set; }

        public override bool Equals(object obj)
        {
            try
            {
                var other = (SessionDTO)obj;

                return (From == other.From)
                    && (To == other.To)
                    && (Count == other.Count);
            }
            catch(InvalidCastException)
            {
                return false;
            }
        }
    }
}
