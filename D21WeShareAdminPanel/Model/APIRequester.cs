using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace D21WeShareAdminPanel.Model
{
    public class APIRequester {

        public static string Login(string user, string password) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/UserGroup/Login/");
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
            System.Net.HttpWebResponse response;
            try {
                response = (System.Net.HttpWebResponse)request.GetResponse();
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }
            
            // Read response
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string token = reader.ReadToEnd();

            return token;
        }

        public async static Task<string> Get(string url, string token) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "text/plain";
            request.Headers.Add("Token", token);

            // Execute http request and retrieve response
            System.Net.HttpWebResponse response;
            try {
                response = (System.Net.HttpWebResponse)await request.GetResponseAsync();
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }

            // Read response
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            return responseString;
        }
    }
}
