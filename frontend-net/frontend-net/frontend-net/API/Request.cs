using frontend_net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
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

        public User GetUser(string token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "user");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " + token);
                var response = httpClient.GetAsync(string.Empty).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    User userObj = new User();
                    userObj.Username = obj["user"]["username"];
                    userObj.Email = obj["user"]["email"];
                    userObj.Bio = obj["user"]["bio"];
                    userObj.Image = obj["user"]["image"];
                    userObj.Token = obj["user"]["token"];
                    return userObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "user");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string token = _httpContextAccessor.HttpContext.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
                }

                string json = JsonConvert.SerializeObject(
                    new
                    {
                        user = new { username = user.Username, email = user.Email, password = user.Password, image = user.Image, bio = user.Bio }
                    }
                );
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = httpClient.PutAsync(string.Empty, content).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    User userObj = new User();
                    userObj.Username = obj["user"]["username"];
                    userObj.Email = obj["user"]["email"];
                    userObj.Password = obj["user"]["password"];
                    userObj.Bio = obj["user"]["bio"];
                    userObj.Image = obj["user"]["image"];
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
                    articleObj.Slug = obj["article"]["slug"];
                    articleObj.CreatedAt = obj["article"]["createdAt"];
                    articleObj.Author = obj["article"]["author"].ToObject<User>();
                    List<string> tagList = obj["article"]["tagList"].ToObject<List<string>>();

                    return articleObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Article GetArticle(string slug)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + slug);
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
                    articleObj.Slug = obj["article"]["slug"];
                    articleObj.CreatedAt = obj["article"]["createdAt"];
                    articleObj.Author = obj["article"]["author"].ToObject<User>();
                    List<string> tagList = obj["article"]["tagList"].ToObject<List<string>>();

                    return articleObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Article> GetArticlesByUser(string username)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles?author=" + username);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(string.Empty).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    var articles = new List<Article>();
                    foreach (var item in obj["articles"])
                    {
                        Article articleObj = new Article();
                        articleObj.Title = item["title"];
                        articleObj.Description = item["description"];
                        articleObj.Body = item["body"];
                        articleObj.Slug = item["slug"];
                        articleObj.CreatedAt = item["createdAt"];
                        articleObj.Author = item["author"].ToObject<User>();
                        List<string> tagList = item["tagList"].ToObject<List<string>>();
                        articles.Add(articleObj);
                    }
                    return articles;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Article> GetAllArticles()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(string.Empty).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    var articles = new List<Article>();
                    foreach (var item in obj["articles"])
                    {
                        Article articleObj = new Article();
                        articleObj.Title = item["title"];
                        articleObj.Description = item["description"];
                        articleObj.Body = item["body"];
                        articleObj.Slug = item["slug"];
                        articleObj.CreatedAt = item["createdAt"];
                        articleObj.Author = item["author"].ToObject<User>();
                        List<string> tagList = item["tagList"].ToObject<List<string>>();
                        articles.Add(articleObj);
                    }
                    return articles;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Article UpdateArticle(Article article, string slug)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + slug);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
                    _httpContextAccessor.HttpContext.Session.GetString("Token"));
                var response = httpClient.PutAsync(string.Empty, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            article = new { title = article.Title, description = article.Description, body = article.Body, tagList = article.Tags }
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
                    articleObj.Slug = obj["article"]["slug"];
                    articleObj.CreatedAt = obj["article"]["createdAt"];
                    articleObj.Author = obj["article"]["author"].ToObject<User>();
                    List<string> tagList = obj["article"]["tagList"].ToObject<List<string>>();
                    return articleObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteArticle(string slug)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + slug);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
                    _httpContextAccessor.HttpContext.Session.GetString("Token"));
                var response = httpClient.DeleteAsync(string.Empty).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Comment> GetComments(string slug)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + slug + "/comments");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(string.Empty).Result;
                var resultJson = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(resultJson))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson);
                    List<Comment> comments = new List<Comment>();
                    foreach (var item in obj["comments"])
                    {
                        Comment commentObj = new Comment();
                        commentObj.Id = item["id"];
                        commentObj.Body = item["body"];
                        commentObj.CreatedAt = item["createdAt"];
                        commentObj.UpdatedAt = item["updatedAt"];
                        commentObj.Author = item["author"].ToObject<User>();
                        comments.Add(commentObj);
                    }
                    return comments;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Comment CreateComment(string slug, string body)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + slug + "/comments");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
                    _httpContextAccessor.HttpContext.Session.GetString("Token"));
                var response = httpClient.PostAsync(string.Empty, new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            comment = new { body = body }
                        }
                        ), Encoding.UTF8, "application/json"));
                var resultJson = response.Result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(resultJson.Result))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultJson.Result);
                    Comment commentObj = new Comment();
                    commentObj.Id = obj["comment"]["id"];
                    commentObj.CreatedAt = obj["comment"]["createdAt"];
                    commentObj.UpdatedAt = obj["comment"]["updatedAt"];
                    commentObj.Body = obj["comment"]["body"];
                    commentObj.Author = obj["comment"]["author"].ToObject<User>();

                    return commentObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteComment(string slug, int commentId)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = UrlApi + "articles/" + slug + "/comments/" + commentId;
                    Console.WriteLine("URL: " + url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
                        _httpContextAccessor.HttpContext.Session.GetString("Token"));
                    var response = httpClient.DeleteAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Response status code: " + response.StatusCode);
                        Console.WriteLine("Response content: " + response.Content.ReadAsStringAsync().Result);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        //public bool AddToFavorite(ArticleFavorite favorite)
        //{
        //    try
        //    {
        //        HttpClient httpClient = new HttpClient();
        //        httpClient.BaseAddress = new Uri(UrlApi + "articles/" + favorite.Article.Slug + "/favorite");
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
        //            _httpContextAccessor.HttpContext.Session.GetString("Token"));
        //        var response = httpClient.PostAsync(string.Empty, null);
        //        var resultJson = response.Result.Content.ReadAsStringAsync();
        //        if (response.Result.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public Article AddToFavorite(ArticleFavorite favorite)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(UrlApi + "articles/" + favorite.Article.Slug + "/favorite");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token " +
                    _httpContextAccessor.HttpContext.Session.GetString("Token"));
                var response = httpClient.PostAsync(string.Empty, null);
                var resultJson = response.Result.Content.ReadAsStringAsync().Result;
                if (response.Result.IsSuccessStatusCode)
                {
                    var updatedArticle = JsonConvert.DeserializeObject<Article>(resultJson);
                    return updatedArticle;
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