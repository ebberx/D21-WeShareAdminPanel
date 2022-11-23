using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace D21WeShareAdminPanel.Model
{
    public class APIRequester {

        public static string RequestToken(string user, string password) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/UserGroup/GetToken/");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            // Set http request body
            string body = "{\"username\":\"" + user + "\",\"password\":\"" + password + "\"}";
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (System.IO.Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
            }

            // Execute http request and retrieve response
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            // Read response
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            return responseString;
        }
    }
}
