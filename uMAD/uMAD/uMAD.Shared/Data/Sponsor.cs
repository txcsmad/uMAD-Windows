using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace uMAD.Data
{
    [Parse.ParseClassName("Sponsors")]
    public class Sponsor : Parse.ParseObject
    {

        public Sponsor()
        {

        }

        [Parse.ParseFieldName("companyImage")]
        public ParseFile CompanyLogo
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [Parse.ParseFieldName("companyWebsite")]
        public string CompanyURL
        {

            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [Parse.ParseFieldName("companyName")]
        public string CompanyName
        {

            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [Parse.ParseFieldName("sponsorLevel")]
        public int SponsorLevel
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        public Uri CompanyLogoURI
        {
            get
            {
                if (CompanyLogo == null)
                    return null;
                return CompanyLogo.Url;
            }
        }
    }
}
