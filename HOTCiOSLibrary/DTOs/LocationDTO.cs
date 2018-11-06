using System;
using System.Collections.Generic;
using System.Text;

namespace HOTCAPILibrary.DTOs
{
    public class LocationDTO
    {
        public int EventID { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string EventName { get; set; }
    }
}
