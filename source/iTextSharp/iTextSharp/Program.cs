using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            PdfDocument doc = new PdfDocument("DENYYYY.pdf");

            doc.AddHeader();

            doc.AddOrderLine(124453544);

            doc.AddInfoTable(new AddressInfo
            {
                AdressName = "KHALID BOUKHLOUF",
                DestinationRegionCode = 2018,
                Country = "Belgium",
                Number = 4,
                Province = "Antwerpen",
                Reference = "ASD231DA1WC4",
                StreetName = "Van Leriusstraat",
                ZipCode = 2018
            });

            doc.GenerateDocument();

        }


    
    }
}
