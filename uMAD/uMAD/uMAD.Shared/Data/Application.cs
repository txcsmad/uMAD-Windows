using System;
using System.Collections.Generic;
using System.Text;

namespace uMAD.Data
{
    [Parse.ParseClassName("UMAD_Application")]
    public class Application : Parse.ParseObject
    {
        [Parse.ParseFieldName("dietaryRestrictions")]
        public string DietaryRestrictions
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("firstTime")]
        public bool FirstTime
        {
            get { return GetProperty<bool>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("umad")]
        public UMAD UMad
        {
            get { return GetProperty<UMAD>(); }
            set { SetProperty(value); }
        }

        [Parse.ParseFieldName("user")]
        public User User
        {
            get { return GetProperty<User>(); }
            set { SetProperty(value); }
        }
    }
}
