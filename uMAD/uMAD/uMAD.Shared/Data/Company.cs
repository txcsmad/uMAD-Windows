namespace uMAD.Data
{
    [Parse.ParseClassName("Company")]
    public class Company : Parse.ParseObject
    {
        [Parse.ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("thumbnail")]
        public Parse.ParseFile ThumbnailFile
        {
            get { return GetProperty<Parse.ParseFile>(); }
            set { SetProperty(value);}
        }

        [Parse.ParseFieldName("website")]
        public string WebsiteUrl
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value);}
        }

        [Parse.ParseFieldName("image")]
        public Parse.ParseFile ImageFile
        {
            get { return GetProperty<Parse.ParseFile>(); }
            set { SetProperty(value);}
        }

        [Parse.ParseFieldName("twitterHandle")]
        public string TwitterHandle
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

    }
}