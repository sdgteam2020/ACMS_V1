using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using IHSDC.WebApp.Models;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;

namespace IHSDC.WebApp.Services
{
    public class SearchService
    {
        private readonly List<SearchableItem> searchableItems; 

        public SearchService(List<SearchableItem> items)
        {
            searchableItems = items;
        }

        public static List<SearchableItem> InitializeSearchData(string uploadsDirectoryPath)
        {
            string[] filesPath = Directory.GetFiles(uploadsDirectoryPath, "*.*", SearchOption.AllDirectories);
            List<SearchableItem> sampleData = new List<SearchableItem>();

            int i = 1;
            foreach (string path in filesPath)
            {
                sampleData.Add(new SearchableItem
                {
                    Id = i,
                    FileName = Path.GetFileName(path),
                    Content = ProcessFile(path),
                    FilePath= path
                });
                i = i + 1;
            }
            return sampleData;
        }


        private static string ProcessFile(string filePath)
        {
            if (System.IO.File.Exists(filePath) && Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    using (var pdfReader = new PdfReader(filePath))
                    {
                        using (var pdfDocument = new PdfDocument(pdfReader))
                        {
                            string text = "";
                            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                            {
                                var strategy = new SimpleTextExtractionStrategy();
                                text += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);
                            }
                            return text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing PDF: {ex.Message}");
                    return "Error processing PDF";
                }
            }
            else
            {
                return "Invalid or non-existent PDF file";
            }
        }

        public List<SearchableItem> Search(string query)
        {
            var results = new List<SearchableItem>();

            foreach (var item in searchableItems)
            {
                var content = item.Content;

                int index = content.IndexOf(query.ToLower(), StringComparison.OrdinalIgnoreCase);

                if (index != -1)
                {
                    int startParagraphBefore = Math.Max(0, content.LastIndexOf('\n', index - 1));
                    string paragraphBefore = content.Substring(startParagraphBefore, index - startParagraphBefore).Trim();

                    int endParagraphAfter = content.IndexOf('\n', index + query.Length);
                    if (endParagraphAfter == -1)
                        endParagraphAfter = content.Length;

                    string paragraphAfter = content.Substring(index + query.Length, endParagraphAfter - (index + query.Length)).Trim();

                    item.MatchedParagraph = content.Substring(index, query.Length);
                    item.ParagraphBefore = paragraphBefore;
                    item.ParagraphAfter = paragraphAfter;

                    results.Add(item);
                }
            }
            return results;
        }
    }
}
