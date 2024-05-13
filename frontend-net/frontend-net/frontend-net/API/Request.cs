using frontend_net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace frontend_net.API
{
    public class Request
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string UrlApi;

        public Request(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            UrlApi = _configuration.GetValue<string>("UrlApi");
            _httpContextAccessor = httpContextAccessor;
        }

        public User LogIn(string email, string password)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "users/login");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync(string.Empty, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            user = new { email = email, password = password }
                        }
                        ),Encoding.UTF8,"application/json"));
                var resultJson = response.Result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(resultJson.Result))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson.Result);
                    User userObj = new User();
                    userObj.Username = obj["user"]["username"];
                    userObj.Email = obj["user"]["email"];
                    userObj.Bio = obj["user"]["bio"];
                    userObj.Image = obj["user"]["image"];
                    userObj.Token = obj["user"]["token"];

                    _httpContextAccessor.HttpContext.Session.SetString("Token", userObj.Token);

                    return userObj;
                }
                return null;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public User SignUp(string username, string email, string password)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "users");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync(string.Empty, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            user = new { username = username, email = email, password = password }
                        }
                        ), Encoding.UTF8, "application/json"));
                var resultJson = response.Result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(resultJson.Result))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson.Result);
                    User userObj = new User();
                    userObj.Username = obj["user"]["username"];
                    userObj.Email = obj["user"]["email"];
                    userObj.Bio = obj["user"]["bio"];
                    userObj.Image = obj["user"]["image"];
                    userObj.Token = obj["user"]["token"];

                    _httpContextAccessor.HttpContext.Session.SetString("Token", userObj.Token);

                    return userObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Article CreateArticle(string title, string description, string body, List<Tag> tags)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " + 
                    _httpContextAccessor.HttpContext.Session.GetString("Token"));
                var response = httpClient.PostAsync(string.Empty, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            article = new { title = title, description = description, body = body, tagList = tags }
                        }
                        ), Encoding.UTF8, "application/json"));
                var resultJson = response.Result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(resultJson.Result))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson.Result);
                    Article articleObj = new Article();
                    articleObj.Title = obj["article"]["title"];
                    articleObj.Description = obj["article"]["description"];
                    articleObj.Body = obj["article"]["body"];
                    articleObj.Tags = obj["article"]["tagList"].ToObject<List<string>>();

                    return articleObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Article GetArticle(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + id);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(string.Empty).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    Article articleObj = new Article();
                    articleObj.Title = obj["article"]["title"];
                    articleObj.Description = obj["article"]["description"];
                    articleObj.Body = obj["article"]["body"];
                    articleObj.Tags = obj["article"]["tagList"].ToObject<List<string>>();

                    return articleObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

//TODO: al crear usuario o hacer login, guardar TOKEN en sesion para llamarlo en otras peticiones