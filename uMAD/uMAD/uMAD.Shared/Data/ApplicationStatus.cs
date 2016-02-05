namespace uMAD.Data
{
    [Parse.ParseClassName("UMAD_Application_Status")]
    public class ApplicationStatus : Parse.ParseObject
    {
        [Parse.ParseFieldName("application")]
        public Application Application
        {
            get { return GetProperty<Application>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("status")]
        public string Status
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
    }
}