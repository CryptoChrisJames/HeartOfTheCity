using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOTCLibrary.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public string City { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string PictureFile { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        //public ApplicationUser { get; set; }

}
}