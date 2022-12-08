using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using D21WeShareAdminPanel.Model.DTO;
using System.Text.Json;
using System.CodeDom;

namespace D21WeShareAdminPanel.Model
{
    public class APIRequester {

        public static string Login(string user, string password) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/Main/Login/");
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
                requestStream.Close();
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
            string res = reader.ReadToEnd();
            
            // Contains token on successful login
            if(res.Contains("Already"))
                return null!;
            else
                return res;
        }

        public static void Logout(string token) {

            Debug.WriteLine("Logout\nToken: "+ token);

            // Send request to get user id from token
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/Main/GetUserIDOnToken/" + token);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            System.Net.HttpWebResponse response;
            try {
                response = (System.Net.HttpWebResponse)request.GetResponse();
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return;
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string userid = reader.ReadToEnd();

            Debug.WriteLine("User ID: " + userid);

            // Send logout request
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/Main/Logout/" + userid);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            try {
                response = (System.Net.HttpWebResponse)request.GetResponse();
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return;
            }

            reader = new System.IO.StreamReader(response.GetResponseStream());
            string res = reader.ReadToEnd();
            Debug.WriteLine(res);
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
            
            Debug.WriteLine("Get response:\n"+responseString);
            return responseString;
        }

        public async static Task<string> UpdateUser(UpdateUserDTO user, string token) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/ShareUser/UpdateUser");
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "text/plain";
            request.Headers.Add("Token", token);

            // Set http request body
            string body = JsonSerializer.Serialize(user);
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (System.IO.Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
            }

            Debug.WriteLine("body:\n" + body);

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
            Debug.WriteLine("response:\n" + responseString);

            return responseString;
        }

        public async static Task<string> UpdateGroup(GroupDTO group, string token) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/ShareGroup/UpdateGroupDetails/");
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "text/plain";
            request.Headers.Add("Token", token);

            // Set http request body
            string body = JsonSerializer.Serialize(group);
            Debug.WriteLine(body);
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (System.IO.Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
            }

            Debug.WriteLine("body:\n" + body);

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

            Debug.WriteLine("response:\n" + responseString);

            return responseString;
        }

        public async static Task<string> ResetPassword(int userid) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/ShareUser/ResetAccountPassword/" + userid);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            // Execute http request and retrieve response
            System.Net.HttpWebResponse response;
            try {
                response = (System.Net.HttpWebResponse)await request.GetResponseAsync();
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return null!;
            }
            //Debug.WriteLine(response);

            // Read response
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string token = reader.ReadToEnd();

            return token;
        }

        public async static Task<string> AddUser(NewUserDTO user) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/ShareUser/CreateUser");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            // Set http request body
            string body = JsonSerializer.Serialize(user);
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (System.IO.Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
                requestStream.Close();
            }

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
            string res = reader.ReadToEnd();

            return res;
        }


        public async static Task<string> DeleteUser(int userid) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/UserGroup/DeleteUser/" + userid);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

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
            string res = reader.ReadToEnd();

            return res;
        }

        public async static Task<string> AddGroup(NewGroupDTO group) {
            // Create http request
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://api-wan-kenobi.ovh/api/ShareGroup/CreateGroup");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "text/plain";

            // Set http request body
            string body = JsonSerializer.Serialize(group);
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (System.IO.Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
                requestStream.Close();
            }

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
            string res = reader.ReadToEnd();

            return res;
        }
    }
}
