using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace uMAD.Data
{
    [Parse.ParseClassName("Events")]
    public class ScheduleSession : Parse.ParseObject
    {
        [Parse.ParseFieldName("speaker")]
        public string SpeakerName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("sessionName")]
        public string SubjectName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("startTime")]
        public DateTime Time
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }


        [Parse.ParseFieldName("company")]
        public string CompanyName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [Parse.ParseFieldName("room")]
        public string Room
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("companyImage")]
        public Parse.ParseFile CompanyImageFile
        {
            get { return GetProperty<Parse.ParseFile>(); }
            set { SetProperty<Parse.ParseFile>(value); }
        }


        [Parse.ParseFieldName("companyWebsite")]
        public string CompanyWebsite
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("email")]
        public string Email
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("endTime")]
        public DateTime EndTime
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }


        [Parse.ParseFieldName("twitterHandle")]
        public string TwitterHandle
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        public Uri CompanyImageUri
        {
            get
            {
                return CompanyImageFile?.Url;
            }
        }

        public static ScheduleSession CurrentSession { get; set; }

        public override int GetHashCode()
        {
            return ObjectId.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return ObjectId.Equals(((Parse.ParseObject)obj).ObjectId);
        }
    }
}
