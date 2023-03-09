using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [TempData]
        public string MessageError { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FechamentoPonto([FromServices] IArchiveApi archiveApi)
        {
            var list = await archiveApi.GetArchives();
            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> FechamentoPonto([FromServices] IArchiveApi archiveApi, ArchiveCommand command)
        {
            if (command.Archives is null)
            {
                MessageError = "Por favor informe pelos menos 1 arquivo csv.";
                return RedirectToAction(nameof(FechamentoPonto));
            }

            var files = command.Archives.Select(x => new StreamPart(x.OpenReadStream(), x.FileName));
            await archiveApi.PostArchives(files);

            return RedirectToAction(nameof(FechamentoPonto));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}