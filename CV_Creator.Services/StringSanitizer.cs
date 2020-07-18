using System;

namespace CV_Creator.Services
{
    public class StringSanitizer : IStringSanitizer
    {
        //TODO: unit test
        public string CleanUpComment(string comment)
        {
            if (comment.Contains(@"<div align='justify'>"))
            {
                comment = comment.Split(new string[] { @"<div align='justify'>" }, StringSplitOptions.None)[1].Split('<')[0];
            }

            if (comment.Contains(Environment.NewLine))
            {
                comment = comment.Replace(Environment.NewLine, " ");
            }

            return comment;
        }
    }
}
