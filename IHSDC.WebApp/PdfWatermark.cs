using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Hosting;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Parser;
//using Microsoft.AspNetCore.Http;

using Color = iText.Kernel.Colors.Color;
using Document = iText.Layout.Document;
using Paragraph = iText.Layout.Element.Paragraph;
using Rectangle = iText.Kernel.Geom.Rectangle;



namespace IHSDC.WebApp
{
    public class PdfWatermark
    {
       
        public static string AddWatermark(string inputFilePath, string watermarkText)
        {

            try
            {
                const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new char[15];

                for (int i = 0; i < 15; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }

                string OFileName = new string(result);
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                watermarkText = addresses[0].ToString();
                string rootPath = HostingEnvironment.MapPath("~");
                

                Random rnd = new Random();
                string Dfilename = rnd.Next(1, 1000).ToString();
                var filePath1 = System.IO.Path.Combine(rootPath, "PolicyPdf" + OFileName + ".pdf");
                //var filePath1 = "Test.pdf";
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputFilePath), new PdfWriter(filePath1));
                Document doc = new Document(pdfDoc);
                PdfFont font = PdfFontFactory.CreateFont(FontProgramFactory.CreateFont(StandardFonts.HELVETICA));
                Paragraph paragraph = new Paragraph(watermarkText + DateTime.Now).SetFont(font).SetFontSize(30);

                PdfExtGState gs1 = new PdfExtGState().SetFillOpacity(0.2f);
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfPage pdfPage = pdfDoc.GetPage(i);
                    Rectangle pageSize = pdfPage.GetPageSize();
                    float x = (pageSize.GetLeft() + pageSize.GetRight()) / 2;
                    float y = (pageSize.GetTop() + pageSize.GetBottom()) / 2;
                    PdfCanvas over = new PdfCanvas(pdfPage);
                    over.SaveState();
                    over.SetExtGState(gs1);

                    doc.ShowTextAligned(paragraph, 297, 450, i, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);

                    over.RestoreState();
                }

                doc.Close();
                return filePath1;
                // return modifiedFilePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        //public string getipAddress()
        //{

        //    string ip="";
        //    //System.Text.StringBuilder ip = new System.Text.StringBuilder(); // empty

        //    string hostName = Dns.GetHostName();
        //    IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
        //    IPAddress[] addresses = hostEntry.AddressList;

        //    ip = addresses.ToString();
        //    //foreach (IPAddress address in addresses)
        //    //{
        //    //    ip = address;
        //    //}
        //    return ip.ToString();
        //}




    }

}











                //string modifiedFilePath = "Test.pdf";
                //using (PdfReader reader = new PdfReader(inputFilePath))
                //using (PdfWriter writer = new PdfWriter("Test.Pdf"))
                //using (PdfDocument pdfDoc = new PdfDocument(reader))
                //{
                //    using (Document doc = new Document(pdfDoc))
                //    {
                //        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                //        float fontSize = 60;
                //        Color color = ColorConstants.LIGHT_GRAY;


                //        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                //        {
                //            PdfPage page = pdfDoc.GetPage(i);
                //            Rectangle pageSize = page.GetPageSize();

                //            PdfCanvas pdfCanvas = new PdfCanvas(page);
                //           // Canvas canvas = new Canvas(pdfCanvas, pageSize);
                //            Canvas canvas = new Canvas(page, pdfDoc.GetDefaultPageSize());


                //            Paragraph watermark = new Paragraph(watermarkText)
                //                .SetFont(font)
                //                .SetFontSize(fontSize)
                //                .SetFontColor(color)
                //                .SetOpacity(0.3f)
                //                .SetTextAlignment(TextAlignment.CENTER);

                //            canvas.Add(watermark);
                //        }

                //        //pdfDoc.WriteSelectedTextTo(writer);
                //        return modifiedFilePath;
                //    }
                //}
           



    //public class PdfWatermark
    //{
    //    public static string GenerateModifiedPDF(string inputFile, string watermarkText)
    //    {
    //        try {
    //            // Create a temporary file path for the modified PDF
    //            string modifiedFilePath = Path.Combine(Path.GetTempPath(), "modified.pdf");

    //            // Read the input PDF file
    //            PdfReader reader = new PdfReader(inputFile);

    //            // Create a PDF stamper to write modifications to the modified PDF file
    //            using (FileStream fs = new FileStream(modifiedFilePath, FileMode.Create, FileAccess.Write))
    //            {
    //                PdfStamper stamper = new PdfStamper(reader, fs);

    //                // Get the total number of pages in the PDF
    //                int pageCount = reader.NumberOfPages;

    //                // Loop through each page and add the watermark
    //                for (int i = 1; i <= pageCount; i++)
    //                {
    //                    // Get the content byte of the current page
    //                    PdfContentByte content = stamper.GetUnderContent(i);

    //                    // Define the font and size of the watermark
    //                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
    //                    content.SetFontAndSize(baseFont, 48);

    //                    // Set the color of the watermark text
    //                    content.SetColorFill(BaseColor.LIGHT_GRAY);

    //                    // Set the position of the watermark
    //                    float x = 200; // X coordinate
    //                    float y = 400; // Y coordinate

    //                    // Rotate the watermark
    //                    content.BeginText();
    //                    content.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, 45);
    //                    content.EndText();
    //                }

    //                // Close the stamper and input PDF reader
    //                stamper.Close();
    //                reader.Close();

    //    }
    //            return modifiedFilePath;
    //        }
    //        catch (Exception ex)
    //        {
    //            return ex.Message;
    //        }
    //    }


    //}


    //string hostName = Dns.GetHostName();
    //IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
    //IPAddress[] addresses = hostEntry.AddressList;

    //    foreach (IPAddress address in addresses)
    //    {
    //        Console.WriteLine("IP Address: " + address);
    //    }

