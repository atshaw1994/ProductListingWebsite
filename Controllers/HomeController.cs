using Markdig;
using Microsoft.AspNetCore.Mvc;
using ProductListingWebsite.Models;
using System.Diagnostics;

namespace ProductListingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string readmeHtml = "";
            string url = "https://raw.githubusercontent.com/atshaw1994/ProductListingWebsite/refs/heads/master/README.md";

            using (HttpClient client = new())
            {
                try
                {
                    // Fetch the raw markdown string
                    string markdown = await client.GetStringAsync(url);

                    // Configure Markdig pipeline (UseAdvanced adds tables, emojis, etc.)
                    var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

                    // Convert Markdown to HTML
                    readmeHtml = Markdown.ToHtml(markdown, pipeline);
                }
                catch (Exception)
                {
                    readmeHtml = "<p>Error loading content from GitHub.</p>";
                }
            }

            // Pass the HTML string to the View
            ViewBag.ReadmeContent = readmeHtml;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
