using System;

namespace SignUp
{
    /// <summary>
    /// Constants.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Constants"/> class.
        /// </summary>
        public Constants()
        {
            
        }

        /// <summary>
        /// The azure app service URL.
        /// </summary>
        public static string AZURE_APP_SERVICE_URL = "https://brightsoftsignup.azurewebsites.net";
    
        /// <summary>
        /// These attributes are defined in Facebook and they decide what properties/attributes we are pulling from the Facebook profile
        /// </summary>
        public static class FacebookAttributes {
            public static string ID = "id"; 
            public static string Name = "name";
            public static string Email = "email";
        }

        /// <summary>
        /// Cross settings.
        /// </summary>
        public static class CrossSettingsKeys {
            public static string FacebookID = "facebookID";
            public static string FacebookName = "facebookName";
            public static string FacebookEmail = "facebookEmail";

            public static string GroupCode = "groupCode";
        }
    }
}
