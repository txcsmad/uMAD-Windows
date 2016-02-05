using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace uMAD.Data
{
    [Parse.ParseClassName("User")]
    public class User : ParseObject
    {
        [ParseFieldName("gender")]
        public string Gender
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("emailVerified")]
        public bool EmailVerified
        {
            get { return GetProperty<bool>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("graduationYear")]
        public int GraduationYear
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("resume")]
        public ParseFile Resume
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("username")]
        public string Username
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("major")]
        public string Major
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("shirtSize")]
        public string ShirtSize
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("githubUsername")]
        public string GithubUsername
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("favorites")]
        public IList<ScheduleSession> Favorites
        {
            get { return GetProperty<IList<ScheduleSession>>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("email")]
        public string Email
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
    }
}
