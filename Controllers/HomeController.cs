using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Azure.Storage.Blobs;

using ServiceApp.Models;
using ServiceApp.ViewModels;

namespace ServiceApp.Controllers
{
    public class HomeController : Controller
    {

        private DatabaseContext db;

        public HomeController(DatabaseContext context) => db = context;

        public IActionResult Index() => View(db.Articles);

        [HttpGet]
        public IActionResult Article(int Id) {
            return View(db.Articles.FirstOrDefault(a => a.Id == Id));
        }

        [Route("/home/error/{code:int}")]
        public IActionResult Error(int code)
        {
            ViewData["ErrorMessage"] = $"Произошла ошибка. Код ошибки: {code}";
            return View();
        }

        [Authorize(Roles="admin")]
        [Route("/adminpanel")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("/create")]
        public IActionResult CreateView() => View("CreateArticle");

        [Authorize]
        [HttpPost]
        [Route("/create")]
        public IActionResult CreateArticle(CreateArticleModel createArticleModel)
        {

            IFormFile file = Request.Form.Files.First();

            string resultingFileName = SaveFileAsync(file).Result;

            Article article = new Article
            {
                Title = createArticleModel.Title,
                Content = createArticleModel.Content,
                AuthorId = createArticleModel.AuthorId,
                LastEditDate = DateTime.Now,
                Tags = new string[0],
                fileName = resultingFileName
            };

            db.Articles.Add(article);
            db.SaveChanges();

            return RedirectToAction("index", "home");

        }

        [Route("/delete/{Id:int}")]
        public IActionResult DeleteArticle(int Id)
        {

            int? AuthorId = db.Articles.FirstOrDefault(a => a.Id == Id).AuthorId;

            if (User.Identity.Name == db.Users.FirstOrDefault(u => u.Id == AuthorId).Username || User.IsInRole("admin")) {

                Article article = db.Articles.FirstOrDefault(a => a.Id == Id);
                if (article != null)
                {
                    db.Articles.Remove(article);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");

        }

        public static async Task PushToCloud(string fileName, string path)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=ggwp;AccountKey=1BMS2DPbo4xWmKqEETTB0wSJ+O/r8kVo/EVd+xcOx4mx/ikn3A9oR5zEVna52SnPJg6ELY+n/6nWER77WcKrng==;EndpointSuffix=core.windows.net";
            var serverClient = new BlobServiceClient(connectionString);
            var containerClient = serverClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(fileName);
            using FileStream uploadFileStream = System.IO.File.OpenRead(path);
            blobClient.Upload(uploadFileStream, true);
            uploadFileStream.Close();

            System.IO.File.Delete(fileName);

        }

        public static string GetFileName()
        {
            var fileName = Guid.NewGuid().ToString();
            return fileName;
        }

        public static async Task<string> SaveFileAsync(IFormFile file)
        {

            var originalFileName = Path.GetFileName(file.FileName);
            string extension = originalFileName.Substring(originalFileName.LastIndexOf('.') + 1, originalFileName.Length - 1 - originalFileName.LastIndexOf('.'));
            var uniqueFileName = GetFileName();

            using (var stream = System.IO.File.Create(uniqueFileName + '.' + extension))
            {
                await file.CopyToAsync(stream);
            }

            string resultingName = uniqueFileName + '.' + extension;

            await PushToCloud(resultingName, resultingName);

            return resultingName;

        }

    }
}
