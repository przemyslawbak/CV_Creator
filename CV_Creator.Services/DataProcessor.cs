using System.Collections.Generic;
using System.Text;
using CV_Creator.Models;
using TuesPechkin;

namespace CV_Creator.Services
{
    public class DataProcessor : IDataProcessor
    {
        private readonly string _name = "Przemyslaw Bak";
        private readonly IHtmlStringSource _htmlSource;

        public DataProcessor(IHtmlStringSource htmlSource)
        {
            _htmlSource = htmlSource;
        }

        public byte[] ProcessPortfolio(List<Project> loadedProjects, List<Technology> loadedTechStack, string companyName, string positionApplied)
        {
            IConverter converter = new ThreadSafeConverter(new PdfToolset(new Win32EmbeddedDeployment(new TempFolderDeployment())));

            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ProduceOutline = true,
                    DocumentTitle = "Pretty stuff",
                    PaperSize = new PechkinPaperSize("210mm", "297mm"), // Implicit conversion to PechkinPaperSize
                    ImageQuality = 100,
                UseCompression = true,
                ColorMode = GlobalSettings.DocumentColorMode.Color,
                    Margins =
                    {
                        Right = 1,
                        Left = 1,
                        Top = 5
                    }
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlText = GetCvHtml(loadedProjects, companyName, positionApplied, loadedTechStack),
                        WebSettings = GetWebSettings(),
                        CountPages = true,
                        FooterSettings = GetFooterSettings(companyName)
                    }
                }
            };

            return converter.Convert(doc);
        }

        private WebSettings GetWebSettings()
        {
            return new WebSettings() { UserStyleSheet = @"file:///C:\Users\asus\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\styles.css" };
        }

        private FooterSettings GetFooterSettings(string companyName)
        {
            return new FooterSettings() { FontSize = 9, LeftText = "Aplication for " + companyName.ToUpper() + " genereted by: github.com/przemyslawbak/CV_Creator", RightText = "by Przemyslaw Bak - Page [page] of [toPage]", UseLineSeparator = true };
        }

        private string GetCvHtml(List<Project> loadedProjects, string companyName, string positionApplied, List<Technology> loadedTechStack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CreateHtmlDocument());
            sb.Append(CreateHtmlHead());
            sb.Append(CreateOpenHtmlBody());
            sb.Append(CreateCvHeader(_name, companyName, positionApplied));
            sb.Append(CreateCvProjects(loadedProjects));
            sb.Append(CreateTechStack(loadedTechStack));
            sb.Append(CreateCvEmploymentHistory());
            sb.Append(CreateCvEducation());
            sb.Append(CreateCvInterests());
            sb.Append(CreateHtmlFooter(companyName));
            sb.Append(CreateCloseHtmlBody());
            sb.Append(CreateCloseHtmlDocument());

            return sb.ToString();
        }

        private string CreateCloseHtmlDocument()
        {
            return _htmlSource.GetHtmlDocumentClosed();
        }

        private string CreateCloseHtmlBody()
        {
            return _htmlSource.GetHtmlBodyClosed();
        }

        private string CreateHtmlFooter(string companyName)
        {
            return _htmlSource.GetHtmlCvRodoFooter(companyName);
        }

        private string CreateCvInterests()
        {
            return _htmlSource.GetHtmlCvInterests();
        }

        private string CreateCvEducation()
        {
            return _htmlSource.GetHtmlCvEducation();
        }

        private string CreateCvEmploymentHistory()
        {
            return _htmlSource.GetHtmlCvEmployment();
        }

        private string CreateTechStack(List<Technology> loadedTechStack)
        {
            return _htmlSource.GetHtmlCvTechnologies(loadedTechStack);
        }

        private string CreateCvProjects(List<Project> loadedProjects)
        {
            return _htmlSource.GetHtmlCvProjects(loadedProjects);
        }

        private string CreateCvHeader(string name, string company, string position)
        {
            return _htmlSource.GetHtmlCvHeader(name, company, position);
        }

        private string CreateOpenHtmlBody()
        {
            return _htmlSource.GetHtmlBodyOpened();
        }

        private string CreateHtmlDocument()
        {
            return _htmlSource.GetHtmlDocumentOpened();
        }

        private string CreateHtmlHead()
        {
            return _htmlSource.GetHtmlHead();
        }
    }
}
