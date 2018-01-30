using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp
{
    public class PdfDocument : Document
    {
        public string FileName { get; set; }
        public PdfWriter Writer { get; set; }
        public MemoryStream Ms { get; set; }
        public FileStream Fs { get; set; }
        public Font NormalFont { get; set; }
        public Font BoldFont { get; set; }
        
        public PdfDocument(string filename) : base()
        {
            NormalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            BoldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Ms = new MemoryStream();
            Fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            Writer = PdfWriter.GetInstance(this, Fs);
            this.Open();
        }

        public void GenerateDocument()
        {
            //Ms.CopyTo(Fs);
            this.Close();
        }
        

        public void AddHeader()
        {
            //Filenames for image headers (Deny logo and barcode)
            string logoFileName = "App_Data/logo.jpg";
            string barCodeFileName = "App_Data/barcode.png";

            //Creating the two image instances
            Image img = CreateImage(logoFileName, Element.ALIGN_LEFT, 120f, 90f);
            Image img2 = CreateImage(barCodeFileName, Element.ALIGN_RIGHT, 120f, 90f);

            //Creating a two cell table that will contain the header
            PdfPTable table = new PdfPTable(2);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            //Creating the two cells
            PdfPCell cell = new PdfPCell(img);
            cell.PaddingLeft = -50f;

            PdfPCell cell2 = new PdfPCell(img2);
            cell2.PaddingLeft = 100f;

            //Setting no border line for the two cells
            cell.BorderWidth = 0;
            cell2.BorderWidth = 0;

            //Adding the two cells to the table
            table.AddCell(cell);
            table.AddCell(cell2);

            table.SpacingAfter = 50f;

            //Final step : Adding the table to the document
            this.Add(table);
        }

        public void AddOrderLine(int orderNo)
        {

            Paragraph p = new Paragraph(String.Concat("ORDER: ", orderNo));
            p.PaddingTop = 100f;

            this.Add(p);
        }


        public void AddInfoTable(AddressInfo adressInfo)
        {

            PdfPTable infoTable = new PdfPTable(1);

            var phrase = new Phrase();

            //Destination Region Code
            phrase.Add(new Chunk(adressInfo.DestinationRegionCode.ToString() + "\n", BoldFont));

            //Adress Name
            phrase.Add(new Chunk(adressInfo.AdressName + "\n", BoldFont));

            //Street + Number
            phrase.Add(new Chunk(adressInfo.StreetName, BoldFont));
            phrase.Add(new Chunk("          "));
            phrase.Add(new Chunk(adressInfo.Number.ToString() + "\n", BoldFont));

            //ZipCode + Province + Country
            phrase.Add(new Chunk(adressInfo.ZipCode.ToString(), BoldFont));
            phrase.Add(new Chunk("          "));
            phrase.Add(new Chunk(adressInfo.Province, BoldFont));
            phrase.Add(new Chunk("          "));
            phrase.Add(new Chunk(adressInfo.Country + "\n", BoldFont));

            //Reference Number
            phrase.Add(new Chunk("LOSREFERENTIE:", NormalFont));
            phrase.Add(new Chunk("          "));
            phrase.Add(new Chunk(adressInfo.Reference, BoldFont));

            PdfPCell infoTableCell = new PdfPCell(phrase);

            this.Add(new Chunk("\n"));

            infoTable.AddCell(infoTableCell);

            this.Add(infoTable);

            this.Add(new Chunk("\n"));
        }

        private static Image CreateImage(string imgUrl, int Alignment, float width, float height)
        {
            Image jpg = Image.GetInstance(imgUrl);
            jpg.ScaleToFit(width, height);
            jpg.Alignment = Alignment;
            return jpg;
        }

    }
}
