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
        public string SpeakerName { get; set; }

        [Parse.ParseFieldName("company")]
        public string SubjectName { get; set; }

        [Parse.ParseFieldName("startTime")] 
        public DateTime Time { get; set; }
    }
}
