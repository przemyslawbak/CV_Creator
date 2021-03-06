﻿using System.Collections.Generic;
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
        <div class='workTitle'>Gdynia Maritime University</div>
        <div class='workPeriod'>
            10.2003 - 06.2007
        </div>
        <div class='workDescription'>
            Bachelor of Engineering in Navigation, specialization: Sea Transport.
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
        <div class='workTitle'>C# WPF, ASP.NET developer</div>
        <div class='workPeriod'>
            04.2017 - present
        </div>
        <div class='workDescription'>
            I am self-employed, working on projects for my private and commercial use. I am responsible for front-end and back-end. Creating from scratch ASP.NET Core web applications, WPF desktop applications, writing unit tests for my projects, using good practices of programming, based on SOLID principles, while learning from the best in the internet. Working with async programming, identity, desingn and achitectural patterns, unit testing, various APIs, graphic editors, planning all from scratch.
        </div>
    </div>
    <div class='workHistory'>
        <div class='workTitle'>Offshore industry freelancer</div>
        <div class='workPeriod'>
            06.2011 - present
        </div>
        <div class='workDescription'>
            Work in the offshore oil-gas and renewable energy industries in operational and management positions, as a team member participating in projects of various levels of complexity.
        </div>
    </div>
    <div class='workHistory'>
        <div class='workTitle'>Maritime transport freelancer</div>
        <div class='workPeriod'>
            08.2005 - 05.2011
        </div>
        <div class='workDescription'>
            Work as a staff member and operational positions at maritime cargo transportation industry.
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
	<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\pic.jpg'>
	<div class='personalData'>
		<div class='name'>" + name + @"</div>
		<div>" + position + @"</div>
		<div>in " + company + @"</div>
	</div>
	<div class='sideInfo'>
		<div class='sideText'>
			<div>+48 725 875 135</div>
			<div>przemyslaw.bak@simple-mail.net</div>
			<div>przemyslaw-bak.pl</div>
			<div>github.com/przemyslawbak</div>
			<div>Wroclaw</div>
		</div>
		<div class='sideImages'>
			<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\phone.png' />
			<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\email.png' />
			<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\world.png' />
			<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\github.png' />
			<img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\house.png' />
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
        <img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\financial.png' />
        <img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\sport.png' />
        <img src='file:///C:\Users\asus\Desktop\IT\!!Trials!!\02_APIs\HtmlToPdf_08_TuesPechkin_CV_Spike\HtmlToPdf\bin\Debug\book.png' />
    </div>
    <div class='interestsText'>
        <div>Interested in global financial markets, observing them for many years.</div>
        <div>Training calisthenics, as time allows matrial arts, and running at a medium distances.</div>
        <div>Interested in social psychology, listening to SWPS lectures, if time permits reading publications.</div>
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
            if (project.Comments.Length > 175)
            {
                int spaceIndex = project.Comments.IndexOf(" ", 175);
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
	<div class='projectComments'><b>About:</b> " + project.Comments + @"</div>
	<div class='projectTech'><b>Tech:</b> VS2017, C#, JS, HTML, WPF, MVVM, LINQ, Git, Prism, Autofac, xUnit, CefSharp, Html Agility Pack, NLog</div>
	<div class='projImages'>
		<img src='file:///C:\Users\asus\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\info_brown.png' />");
            if (project.GitHubUrl != "#")
            {
                sb.Append(@"<img src='file:///C:\Users\asus\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\github_brown.png' />");
            };
            if (project.WebUrl != "#")
            {
                sb.Append(@"<img src='file:///C:\Users\asus\Desktop\IT\Projekty\CV_Creator\CV_Creator.Desktop\bin\Debug\world_brown.png' />");
            };
            sb.Append(@"
	</div>
	<div class='projText'>
		<div><a class='projectLink' href='http://przemyslaw-bak.pl/myprojects/details?projectid=" + project.ProjectID + @"'>more about on: przemyslaw-bak.pl</a></div>");
            if (project.GitHubUrl != "#")
            {
                sb.Append(@"<div><a class='projectLink' href=" + project.GitHubUrl + @" '>" + project.GitHubUrl.Replace("https://", "") + @"</a></div>");
            };
            if (project.WebUrl != "#")
            {
                sb.Append(@"<div><a class='projectLink' href='" + project.WebUrl + @"'>" + project.WebUrl.Replace("https://", "") + @"</a></div>");
            };
            sb.Append(@"
	</div>
</div>
");
            return sb.ToString();
        }

        public string GetHtmlCvRodoFooter(string company)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<div class='footerContainer'>
    <div class='footerText'>
        I agree to the processing, storing and sharing of personal data provided in this document for realising the recruitment process pursuant to the Personal Data Protection Act of 10 May 2018 (Journal of Laws 2018, item 1000) and in agreement with Regulation (EU) 2016/679 of the European Parliament and of the Council of 27 April 2016 on the protection of natural persons with regard to the processing of personal data and on the free movement of such data, and repealing Directive 95/46/EC (General Data Protection Regulation) I hereby consent " + company + @" to administrate, process and store my personal data for the purpose of recruitment processes including sharing my details with potential future employer for whom " + company + @" performs work to establish conditions of engagement before concluding a contract of employment. The agreement covers the processing of personal data by " + company + @" even after recruitment process in order to present further proposals of employment and may be revoked at any time. I declare that all the data that are included in the CV and job applications have been delivered to the company " + company + @" voluntarily and they are true.
    </div>
</div>
");

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
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
<div class='singleTech'>
    <img class='techPic' src='" + technology.PictureLink + @"'>
    <div class='techName'>" + technology.Name + @"</div>
</div>
");

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
    height: 135px;
}

.projectTech {
    width: 95%;
    font-size: 27px;
    margin-top: 10px;
    margin-left: 10px;
    height: 105px;
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
    font-size: 27px;
    margin-top: 30px;
    margin-left: 10px;
    display: inline-block;
}

    .projText div {
        margin-bottom: 10px;
        display: block;
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
    width: 270px;
    margin: 5px;
    margin-top: 20px;
    float: left;
}

.techPic {
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
    margin-left: 15px;
    overflow-x: auto;
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
