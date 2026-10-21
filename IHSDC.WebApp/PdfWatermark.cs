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
