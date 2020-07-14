using System;
using System.Collections.Generic;
using CV_Creator.Models;
using DinkToPdf;

namespace CV_Creator.Services
{
    public class DataProcessor : IDataProcessor
    {
        public byte[] ProcessPortfolio(List<Project> loadedProjects, List<Technology> loadedTechStack, string companyName, string positionApplied)
        {
            var converter = new BasicConverter(new PdfTools());

            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    UseCompression = true
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = GetCvHtml(loadedProjects, companyName, positionApplied),
                        WebSettings = GetWebGettings(),
                        FooterSettings = GetFooterSettings()
                    }
                }
            };

            return converter.Convert(doc);
        }

        private WebSettings GetWebGettings()
        {
            return new WebSettings() { DefaultEncoding = "utf-8" };
        }

        private FooterSettings GetFooterSettings()
        {
            return new FooterSettings() { FontSize = 9, Right = "Przemyslaw Bak - Page [page] of [toPage]", Line = true };
        }

        private string GetCvHtml(object loadedProjects, object companyName, object positionApplied)
        {
            throw new NotImplementedException();
        }
    }
}
