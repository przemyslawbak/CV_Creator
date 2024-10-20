﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CV_Creator.Models;

namespace CV_Creator.Services
{
    public class HtmlStringSource : IHtmlStringSource
    {//TODO: czleaning addresses of http and slashes, TODO: tech images
        private readonly IStringSanitizer _stringSanitizer;

        public HtmlStringSource(IStringSanitizer stringSanitizer)
        {
            _stringSanitizer = stringSanitizer;
        }

        public string GetHtmlBodyClosed()
        {
            return @"
</body>";
        }

        public string GetHtmlBodyOpened()
        {
            return @"
<body>";
        }

        public string GetHtmlCvEducation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='sectionTitle'>Education:</div>");
            sb.Append(@"
<div class='historyContainer'>
    <div class='workHistory'>
        <div class='workTitle'>Poznan University of Technology</div>
        <div class='workPeriod'>
            2014
        </div>
        <div class='workDescription'>
            Master of Science: majoring in Management and production engineering, specializing in quality management.
        </div>
    </div>
    <div class='workHistory'>
        <div class='workTitle'>University of Economics in Wroclaw</div>
        <div class='workPeriod'>
            2013
        </div>
        <div class='workDescription'>
            Bachelor of Engineering: Engineering and Economics.
        </div>
    </div>
</div>
");

            return sb.ToString();
        }

        public string GetHtmlCvEmployment()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='sectionTitle'>Employment history:</div>");
            sb.Append(@"
<div class='historyContainer'>
    <div class='workHistory'>
        <div class='workTitle'>Junior Developer</div>
        <div class='workPeriod'>
            05.2023 - present
        </div>
        <div class='workDescription'>
            Responsibilities: Rewriting application from Python to C#, Creating ASP.NET microservices, Refactoring monolithic applications to microservices, Data engineering, Unit testing, Work on services for algorithmic data processing.
        </div>
    </div>
    <div class='workHistory'>
        <div class='workTitle'>Manual Tester</div>
        <div class='workPeriod'>
            05.2022 - 05.2023
        </div>
        <div class='workDescription'>
            Self-educated junior software tester. My first testing experience I gained at uTest crowd-testing platform. I have completed there a uTest Academy training learning fundaments of testing along with usage of testing tools as SDK Platform Tools, Postman. I also learned how to automate web application tests using Selenium.
        </div>
    </div>
    <div class='workHistory'>
        <div class='workTitle'>Office Assistant</div>
        <div class='workPeriod'>
            08.2014 - 02.2023
        </div>
        <div class='workDescription'>
            Responsibilities: Documentation management and archiving, Cooperation with the accounting office, Cooperation with Customers, Order management and invoicing, Managing receivables and maintaining financial liquidity.
        </div>
    </div>
</div>
");
            return sb.ToString();
        }

        public string GetHtmlCvHeader(string name, string company, string position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<div class='aboutMainContainer'>
	<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\pic.jpg'>
	<div class='personalData'>
		<div class='name'>" + name + @"</div>
		<div>" + position + @"</div>
		<div>in " + company.ToUpper() + @"</div>
	</div>
	<div class='sideInfo'>
		<div class='sideText'>
			<div>+48 694 352 899</div>
			<div>alicja.zalupska@gmail.com</div>
			<div>https://linkedin.com/in/zalupskaalicja</div>
			<div>https://github.com/Aliuser11</div>
			<div>Sobotka, dolnoslaskie</div>
		</div>
		<div class='sideImages'>
			<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\phone.png' />
			<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\email.png' />
			<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\world.png' />
			<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\github.png' />
			<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\house.png' />
		</div>
	</div>
</div>
");

            return sb.ToString();
        }

        public string GetHtmlCvInterests()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='sectionTitle'>Hobbies and interests:</div>");
            sb.Append(@"
<div class='interestsContainer'>
    <div class='interestsImages'>
        <img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\sport.png' />
        <img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\world_brown.png' />
        <img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\book.png' />
    </div>
    <div class='interestsText'>
        <div>Swimming, hiking and jogging.</div>
        <div>English C1.</div>
        <div>Crime stories, board games.</div>
    </div>
</div>
");
            return sb.ToString();
        }

        public string GetHtmlCvProjects(List<Project> loadedProjects)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='sectionTitle'>Recent projects:</div>");
            foreach (var project in loadedProjects)
            {
                project.Comments = _stringSanitizer.CleanUpComment(project.Comments);
                sb.Append(CreateSingleProjectDisplay(project));
            }

            return sb.ToString();
        }

        private string CreateSingleProjectDisplay(Project project)
        {
            if (project.Comments.Length > 220)
            {
                int spaceIndex = project.Comments.IndexOf(" ", 220);
                try
                {
                    project.Comments = project.Comments.Substring(0, spaceIndex) + " (...)";
                }
                catch
                {
                    project.Comments = project.Comments;
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<div class='projectContainer'>
	<div class='projectName'>" + project.Name + @"</div>
	<div class='projectComments'><b>About:</b> " + project.Comments + @"</div>");
            sb.Append(GenerateProjectTestAndDesignSection(project.Tests, project.DesignPattern));
            sb.Append(GenerateProjectTechStackSection(project.TechnologiesProjects));
            sb.Append(GenerateProjectCompletedSection(project.CompletionDate));
            sb.Append(GenerateProjectGithubAndWebsiteInfoSection(project.GitHubUrl, project.WebUrl));
            sb.Append(@"
	</div>
</div>
");
            return sb.ToString();
        }

        private string GenerateProjectCompletedSection(DateTime completionDate)
        {
            Dictionary<int, string> months = new Dictionary<int, string>()
            {
                { 1, "Jan" },
                { 2, "Feb" },
                { 3, "Mar" },
                { 4, "Apr" },
                { 5, "May" },
                { 6, "Jun" },
                { 7, "Jul" },
                { 8, "Aug" },
                { 9, "Sep" },
                { 10, "Oct" },
                { 11, "Nov" },
                { 12, "Dec" },
            };

            StringBuilder sb = new StringBuilder();

            sb.Append(@" <b>Production:</b> " + months[completionDate.Month] + "." + completionDate.Year);

            sb.Append(@"</div>
	<div class='projImages'>");

            return sb.ToString();
        }

        private string GenerateProjectTestAndDesignSection(bool tests, string designPattern)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"
	<div class='projectTech'><b>Tests:</b> " + (tests ? "yes" : "no") + " <b>Design pattern:</b> " + designPattern + " ");

            return sb.ToString();
        }

        private string GenerateProjectTechStackSection(ICollection<TechnologyProject> technologiesProjects)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"
    <b>Stack:</b> ");

            TechnologyProject last = technologiesProjects.Last();
            foreach (TechnologyProject tech in technologiesProjects)
            {
                sb.Append(tech.Technology.Name);
                if (!tech.Equals(last))
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        private string GenerateProjectGithubAndWebsiteInfoSection(string gitHubUrl, string webUrl)
        {
            var maxLength = 33;
            StringBuilder sb = new StringBuilder();

            if (gitHubUrl != "#")
            {
                sb.Append(@"<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\github_brown.png' />");
            };
            if (webUrl != "#")
            {
                sb.Append(@"<img src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\world_brown.png' />");
            };
            sb.Append(@"
	</div>
	<div class='projText'>");
            if (gitHubUrl != "#")
            {
                sb.Append(@"<div><a class='projectLink' href=" + gitHubUrl + @" '>" + TruncateLongString(gitHubUrl.Replace("https://", "").Replace("http://", ""), maxLength) + @"</a></div>");
            };
            if (webUrl != "#")
            {
                sb.Append(@"<div><a class='projectLink' href='" + webUrl + @"'>" + TruncateLongString(webUrl.Replace("https://", "").Replace("http://", ""), maxLength) + @"</a></div>");
            };

            return sb.ToString();
        }

        private string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;

            if (str.Length > maxLength) return str.Substring(0, Math.Min(str.Length, maxLength)) + "(...)";

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }


        public string GetHtmlCvRodoFooter(string company, string optionalRodo)
        {
            StringBuilder sb = new StringBuilder();
            if (optionalRodo == string.Empty)
            {
                sb.Append(@"
<div class='footerContainer'>
    <div class='footerText'>
        I agree to the processing, storing and sharing with potential future employer of personal data provided in this document by " + company.ToUpper() + @" for realising the recruitment process pursuant to the Personal Data Protection Act of 10 May 2018 (Journal of Laws 2018, item 1000) and in agreement with Regulation (EU) 2016/679 of the European Parliament and of the Council of 27 April 2016 on the protection of natural persons with regard to the processing of personal data and on the free movement of such data, and repealing Directive 95/46/EC (General Data Protection Regulation).
    </div>
</div>
");
            }
            else
            {

                sb.Append(@"
<div class='footerContainer'>
    <div class='footerText'>
        " + optionalRodo + @"
    </div>
</div>
");
            }

            return sb.ToString();
        }

        public string GetHtmlCvTechnologies(List<Technology> loadedTechStack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div class='sectionTitle'>Technology stack:</div>");
            sb.Append(@"<div class='techContainer'>");
            foreach (var technology in loadedTechStack)
            {
                sb.Append(CreateSingleTechDisplay(technology));
            }
            sb.Append(@"</div>");

            return sb.ToString();
        }

        private string CreateSingleTechDisplay(Technology technology)
        {
            int emptyStarCounter = 3 - technology.Level;
            int fullStarCounter = technology.Level;
            string filePath = @"file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\stack\" + technology.PictureFile;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<div class='singleTech'>
    <img class='techPic' src='" + filePath + @"' />");
            sb.Append(AddTechLevelStars(emptyStarCounter, fullStarCounter));
            sb.Append(@"
    <div class='techName'>" + technology.Name + @"</div>
</div>
");

            return sb.ToString();
        }

        private string AddTechLevelStars(int emptyStarCounter, int fullStarCounter)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fullStarCounter; i++)
            {
                sb.Append(@"<img class='techStar' src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\star_full.png' />");
            }
            for (int i = 0; i < emptyStarCounter; i++)
            {
                sb.Append(@"<img class='techStar' src='file:///C:\Users\przem\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\images\art\star_empty.png' />");
            }

            return sb.ToString();
        }

        public string GetHtmlDocumentClosed()
        {
            return @"
</html>";
        }

        public string GetHtmlDocumentOpened()
        {
            return @"
<!DOCTYPE html><html>";
        }

        public string GetHtmlHead()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<head>");
            sb.Append(GetCssStyles());
            sb.Append(@"</head>");

            return sb.ToString();
        }

        private string GetCssStyles()
        {
            return @"
<style>
.body {
}

.sectionTitle {
    font-family: Helvetica;
    font-size: 50px;
    padding-top: 40px;
    margin-bottom: 10px;
    margin-left: 80px;
}

/*HEADER*/

.aboutMainContainer {
    font-family: Helvetica;
    background-color: #557da8;
    color: white;
    height: 300px;
    width: 100%;
}

    .aboutMainContainer img {
        border-radius: 50%;
        height: 260px;
        width: 260px;
        margin-top: 20px;
        margin-left: 50px;
        float: left;
    }

.personalData {
    height: 200px;
    font-size: 45px;
    float: left;
    margin-top: 60px;
    margin-left: 50px;
    width: 40%;
}

.name {
    font-size: 60px;
}

.sideInfo {
    float: right;
}

.sideText {
    font-size: 23px;
    margin-top: 40px;
    margin-bottom: 25px;
    margin-right: -10px;
    display: inline-block;
    width: 350px;
}

    .sideText div {
        margin-bottom: 20px;
        display: block;
        float: right;
    }

.sideImages {
    margin-top: -290px;
    margin-right: 140px;
    width: 18px;
    display: inline-block;
    float: right;
}

    .sideImages img {
        margin-bottom: -12px;
        display: block;
        height: 40px;
        width: 40px;
    }

/*PROJECT*/

.projectContainer {
    border-right: solid 1px #557da8;
    border-top: solid 1px #557da8;
    font-family: Helvetica;
    height: 500px;
    width: 48%;
    color: black;
    margin: 1%;
    margin-top: 0.5%;
    float: right;
}

.projectName {
    color: #5C451C;
    font-size: 45px;
    margin-top: 10px;
    margin-left: 10px;
    overflow: hidden;
}

.projectComments {
    width: 95%;
    background-color: #f0f0f0;
    font-size: 27px;
    margin-top: 10px;
    margin-left: 10px;
    padding-right: 5px;
    text-align: justify;
    height: 170px;
}

.projectTech {
    width: 95%;
    font-size: 27px;
    margin-top: 10px;
    margin-left: 10px;
    margin-bottom: 10px;
}

.projImages {
    margin-top: 30px;
    width: 42px;
    display: inline-block;
    float: left;
    margin-left: 40px;
}

    .projImages img {
        margin-bottom: 5px;
        display: block;
        height: 40px;
        width: 40px;
    }

.projText {
    width: 80%;
    font-size: 27px;
    margin-top: 30px;
    margin-left: 10px;
    display: inline-block;
}

    .projText div {
        margin-bottom: 10px;
        display: inline-block;
    }

        .projText div a {
            height: 50px;
            overflow: scroll;
            word-break: break-all;
        }

.projectLink {
    text-decoration: none;
    color: #557da8;
}

/*TECH STACK*/

.techContainer {
    border-top: solid 1px #557da8;
    width: 95%;
    padding-left: 100px;
    height: 260px;
}

.techGroup {
    margin-bottom: 100px;
}

.singleTech {
    font-family: Helvetica;
    height: 60px;
    width: 315px;
    margin: 5px;
    margin-top: 20px;
    float: left;
}

.techPic {
    margin-right: 10px;
    vertical-align: middle;
    display: inline-block;
    height: 50px;
    width: 50px;
    filter: sepia(100%);
}

.techName {
    font-size: 27px;
    vertical-align: middle;
    display: inline-block;
    margin: 5px;
    margin-top: auto;
    margin-bottom: auto;
    margin-left: 10px;
    overflow-x: auto;
}

.techStar {
    margin-right: 1px;
    vertical-align: middle;
    display: inline-block;
    height: 25px;
    width: 25px;
}

/*WORK HISTORY*/

.historyContainer {
    font-family: Helvetica;
    border-top: solid 1px #557da8;
    width: 100%;
}

.workHistory {
    width: 100%;
    margin-bottom: 10px;
    font-size: 27px;
}

.workTitle {
    color: #5C451C;
    font-size: 45px;
    margin-top: 10px;
    margin-left: 50px;
    overflow: hidden;
    width: 60%;
    display: inline-block;
}

.workDescription {
    background-color: #f0f0f0;
    font-size: 27px;
    text-align: justify;
    margin-top: 10px;
    margin-bottom: 30px;
    margin-left: 50px;
    width: 95%;
    padding-right: 10px;
}

.workPeriod {
    margin-top: 10px;
    margin-right: 20px;
    font-size: 45px;
    width: 29%;
    text-align: center;
    float: right;
    display: inline-block;
}

/*INTERESTS*/

.interestsContainer {
    font-family: Helvetica;
    font-size: 27px;
    border-top: solid 1px #557da8;
    width: 100%;
}

.interestsImages {
    margin-top: 30px;
    width: 41px;
    display: inline-block;
    float: left;
    margin-left: 40px;
}

    .interestsImages img {
        margin-bottom: 10px;
        display: block;
        height: 40px;
        width: 40px;
    }

.interestsText {
    font-size: 27px;
    margin-top: 30px;
    margin-bottom: 20px;
    margin-left: 10px;
    display: inline-block;
}

    .interestsText div {
        margin-bottom: 20px;
        display: block;
    }

/*FOTER*/

.footerContainer {
    margin-top: 130px;
    width: 100%;
    font-family: Helvetica;
    font-size: 20px;
    background-color: #557da8;
    color: white;
    text-align: justify;
}

.footerText {
    margin: 20px;
    padding-top: 30px;
    padding-bottom: 30px;
}
</style>";
        }
    }
}
