using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ASP7.Controllers
{
    public class FileController : Controller
    {
        public IActionResult DownloadFile()
        {
            return View("DownloadFile");
        }

        [HttpPost]
        public IActionResult CreateFile(string filename, string name, string surname)
        {
            string content = $"{name} {surname}";
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Files/{filename}.txt");
            System.IO.File.WriteAllText(file_path, content, Encoding.UTF8);
            return PhysicalFile(file_path, "text/plain", $"{filename}.txt");
        }
    }
}
