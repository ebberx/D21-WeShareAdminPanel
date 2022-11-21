using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace D21WeShareAdminPanel.Model
{
    public class APIRequester {

        public static void RequestToken() {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/UserGroup/GetToken/");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "text/html";

            // Set http request parameter
            request.Headers.Add("Username", "Esben");
            request.Headers.Add("Password", "password2");

            // Execute http request and retrieve response
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            // Read response
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            Debug.WriteLine(responseString);

        }
    }
}
