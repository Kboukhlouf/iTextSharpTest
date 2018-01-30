using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp
{
    public class AddressInfo
    {
        public int DestinationRegionCode { get; set; }
        public string AdressName { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }
        public int ZipCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Reference { get; set; }
    }
}
