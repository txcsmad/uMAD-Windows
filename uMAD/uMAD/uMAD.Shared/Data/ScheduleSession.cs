using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace uMAD.Data
{
    [Parse.ParseClassName("UMAD_Session")]
    public class ScheduleSession : Parse.ParseObject
    {
        [Parse.ParseFieldName("speaker")]
        public string SpeakerName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("name")]
        public string SubjectName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("startTime")]
        public DateTime Time
        {
            get { return GetProperty<DateTime>().ToLocalTime(); }
            set { SetProperty<DateTime>(value); }
        }


        [Parse.ParseFieldName("company")]
        public Company Company
        {
            get { return GetProperty<Company>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("room")]
        public string Room
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }


        [Parse.ParseFieldName("descriptionText")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [Parse.ParseFieldName("favoriteCount")]
        public int FavoriteCount
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("capacity")]
        public int Capacity
        {
            get { return GetProperty<int>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("bio")]
        public string Bio
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
            get { return GetProperty<DateTime>().ToLocalTime(); }
            set { SetProperty<DateTime>(value); }
        }

        [Parse.ParseFieldName("umad")]
        public UMAD UMad
        {
            get { return GetProperty<UMAD>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("topicTags")]
        public string[] Tags
        {
            get { return GetProperty<string[]>(); }
            set { SetProperty(value); }
        }

        public Uri CompanyImageUri => Company?.ImageFile?.Url;

        public static ScheduleSession CurrentSession { get; set; }

        public async Task IncrementFavoriteCount()
        {
            this.Increment("favoriteCount");
            await this.SaveAsync();
        }
        public async Task DecrementFavoriteCount()
        {
            this.Increment("favoriteCount", -1);
            await this.SaveAsync();
        }

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
