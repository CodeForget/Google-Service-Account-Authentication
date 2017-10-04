using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Calendar.v3;
using GoogleSamplecSharpSample.Calendarv3.Auth;
using System.IO;
using Google.Apis.Calendar.v3.Data;

namespace CalendarServerToServerApi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //CalendarService service = new CalendarService();
        Event myEvent = new Event
        {
            Summary = "Appointment by shivam",
            Location = "Somewhere",
            Start = new EventDateTime()
            {
                DateTime = new DateTime(2017, 10, 4, 2, 0, 0),
                TimeZone = "America/Los_Angeles"
            },
            End = new EventDateTime()
            {
                DateTime = new DateTime(2017, 10, 4, 2, 30, 0),
                TimeZone = "America/Los_Angeles"
            }
            //,
            // Recurrence = new String[] {
            //"RRULE:FREQ=WEEKLY;BYDAY=MO"
            //}
            //,
            // Attendees = new List<EventAttendee>()
            // {
            // new EventAttendee() { Email = "johndoe@gmail.com" }
            //}
        };

        protected void Page_Load(object sender, EventArgs e)
        {
           string[] scopes = new string[] {
     CalendarService.Scope.Calendar //, // Manage your calendars
 	//CalendarService.Scope.CalendarReadonly // View your Calendars
 };
            string caluser = "sudeeksha@abroadshiksha.com";
            string filePath = Server.MapPath("~/Key/key.json");
            //string filePath = @"D:\_Shivam\CalendarServerToServerApi\CalendarServerToServerApi\Key\key.json";
            var service=ServiceAccountExample.AuthenticateServiceAccount("abroadshiksha@abroadhikshaservices.iam.gserviceaccount.com",filePath, scopes);
            insert(service, caluser, myEvent);
        }
       
       

        public static Event insert(CalendarService service, string id, Event myEvent)
        {
            try
            {
                return service.Events.Insert(myEvent, id).Execute();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}