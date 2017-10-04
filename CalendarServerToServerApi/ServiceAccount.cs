using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Calendar.v3.Data;

namespace GoogleSamplecSharpSample.Calendarv3.Auth
{
    //public class ServiceAccountJson
    //{
    //    public string type { get; set; }
    //    public string project_id { get; set; }
    //    public string private_key_id { get; set; }
    //    public string private_key { get; set; }
    //    public string client_email { get; set; }
    //    public string client_id { get; set; }
    //    public string auth_uri { get; set; }
    //    public string token_uri { get; set; }
    //    public string auth_provider_x509_cert_url { get; set; }
    //    public string client_x509_cert_url { get; set; }
    //}

   

    public static class ServiceAccountExample
    {

        /// <summary>
        /// Authenticating to Google using a Service account
        /// Documentation: https://developers.google.com/accounts/docs/OAuth2#serviceaccount
        /// </summary>
        /// <param name="serviceAccountEmail">From Google Developer console https://console.developers.google.com</param>
        /// <param name="serviceAccountCredentialFilePath">Location of the .p12 or Json Service account key file downloaded from Google Developer console https://console.developers.google.com</param>
        /// <returns>AnalyticsService used to make requests against the Analytics API</returns>
       
        public static CalendarService AuthenticateServiceAccount(string serviceAccountEmail, string serviceAccountCredentialFilePath, string[] scopes)
		{
            try
            {
                if (string.IsNullOrEmpty(serviceAccountCredentialFilePath))
                    throw new Exception("Path to the service account credentials file is required.");
                if (!File.Exists(serviceAccountCredentialFilePath))
                    throw new Exception("The service account credentials file does not exist at: " + serviceAccountCredentialFilePath);
                if (string.IsNullOrEmpty(serviceAccountEmail))
                    throw new Exception("ServiceAccountEmail is required.");
               
                // For Json file
                if (Path.GetExtension(serviceAccountCredentialFilePath).ToLower() == ".json")
                {
                    GoogleCredential credential;
                    //using(FileStream stream = File.Open(serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    

                    using (var stream = new FileStream(serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read))
                    {
                        credential = GoogleCredential.FromStream(stream)
                             .CreateScoped(scopes).CreateWithUser("k.sachdeva@abroadshiksha.com");
                    }
                   
                    // Create the  Calendar service.
                    return new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                       // ApiKey = "AIzaSyB8vsdQeTlY79HSoZA013gJECEHrpOFkN8",
                        ApplicationName = "Calendar Service account Authentication Sample",
                    });
                }
                else if (Path.GetExtension(serviceAccountCredentialFilePath).ToLower() == ".p12")
                {   // If its a P12 file

                    var certificate = new X509Certificate2(serviceAccountCredentialFilePath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
                    var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                    {
                        Scopes = scopes
                    }.FromCertificate(certificate));

                    // Create the  Calendar service.
                    return new CalendarService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "Calendar Authentication Sample",

                    });
                }
                else
                {
                    throw new Exception("Unsupported Service accounts credentials.");
                }

            }
            catch (Exception ex)
            {                
                throw new Exception("CreateServiceAccountCalendarFailed", ex);
            }
        }

       
    }
}