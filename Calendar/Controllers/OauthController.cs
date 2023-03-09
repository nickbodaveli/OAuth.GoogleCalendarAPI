using Calendar.Models;
using Google;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Web;

namespace Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        [HttpGet("GetAllEvent")]
        public async Task<Root> GetAllEvents()
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/primary/events");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("Key", "AIzaSyD393S9EaD1V7CCgYjDHjz2aF-jbABKIVw");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            var response = restClient.GetAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<Root>(response.Content);
                return result;
            }

            return null;
        }

        [HttpGet("GetCalendarByIdentifier/{identifier}")]
        public async Task<Event> GetEvent(string identifier)
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));


            RestClient restClient = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/primary/events/{identifier}");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("Key", "AIzaSyD393S9EaD1V7CCgYjDHjz2aF-jbABKIVw");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            var response = restClient.GetAsync(request).Result;
          
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<Event>(response.Content);
                return result;
            }

            return null;
        }

        [HttpGet("Authorization")]
        public async Task<string> CallBack(string code, string? error, string state)
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var credentialsFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\credentials.json";
            var credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            RestClient restClient = new RestClient("https://oauth2.googleapis.com/token");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("grant_type", "authorization_code");
            request.AddQueryParameter("redirect_uri", "https://localhost:7267/api/Oauth/Authorization");

            var response = restClient.PostAsync(request).Result;

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await System.IO.File.WriteAllTextAsync(tokenFile, response.Content);
            }

            return "";
        }

        [HttpPost("CreateEvent")]
        public async Task<bool> CreateEvent(Event calendarEvent)
        {
            try
            {
                var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
                var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));
                RestClient restClient = new RestClient("https://www.googleapis.com/calendar/v3/calendars/primary/events?conferenceDataVersion=1&sendNotifications=true");
                RestRequest request = new RestRequest();

                var model = JsonConvert.SerializeObject(calendarEvent, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                request.AddQueryParameter("Key", "AIzaSyD393S9EaD1V7CCgYjDHjz2aF-jbABKIVw");
                request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", model, ParameterType.RequestBody);

                var response = restClient.PostAsync(request).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //await System.IO.File.WriteAllTextAsync(tokenFile, response.Content);
                    var test = "working";
                }
            }
            catch (AggregateException e)
            {

                throw new Exception(e.Message);
            }

            return true;
        }

        [HttpPatch("UpdateEvent/{identifier}")]
        public async Task<bool> UpdateEvent(string identifier, Event calendarEvent)
        {
            try
            {
                var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
                var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));
                RestClient restClient = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/primary/events/{identifier}");
                RestRequest request = new RestRequest();

                var model = JsonConvert.SerializeObject(calendarEvent, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                request.AddQueryParameter("Key", "AIzaSyD393S9EaD1V7CCgYjDHjz2aF-jbABKIVw");
                request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", model, ParameterType.RequestBody);

                var response = restClient.PatchAsync(request).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //await System.IO.File.WriteAllTextAsync(tokenFile, response.Content);
                    var test = "working";
                }
            }
            catch (AggregateException e)
            {

                throw new Exception(e.Message);
            }

            return true;
        }

        [HttpDelete]
        public async Task<bool> DeleteEvent(string identifier)
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));


            RestClient restClient = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/primary/events/{identifier}");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("Key", "AIzaSyD393S9EaD1V7CCgYjDHjz2aF-jbABKIVw");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            var response = restClient.DeleteAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        [HttpPost("RefreshToken")]
        public async Task<bool> RefreshToken()
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var credentialsFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\credentials.json";
            var credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient("https://oauth2.googleapis.com/token");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("grant_type", "refresh_token");
            request.AddQueryParameter("refresh_token", tokens["refresh_token"].ToString());

            var response = restClient.PostAsync(request).Result;

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject newTokens = JObject.Parse(response.Content);
                newTokens["refresh_token"] = tokens["refresh_token"].ToString();
                System.IO.File.WriteAllText(tokenFile, newTokens.ToString());
                var rame = "successss";
            }

            return true;
        }

        [HttpPost("RevokeToken")]
        public async Task<bool> RevokeToken()
        {
            var tokenFile = "C:\\Users\\Linton\\source\\repos\\Calendar\\Calendar\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient("https://oauth2.googleapis.com/revoke");
            RestRequest request = new RestRequest();

            request.AddQueryParameter("token", tokens["access_token"].ToString());

            var response = restClient.PostAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rame = "success";
            }

            return true;
        }
    }
}
