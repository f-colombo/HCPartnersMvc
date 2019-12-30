using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HCPartnersMvc.Models;
using Newtonsoft.Json;

namespace HCPartnersMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            List<Registro> registrosModel = new List<Registro>();

            var response = this.GetRecords("https://saltala-cd91e.firebaseio.com/registros.json");
            registrosModel = JsonConvert.DeserializeObject<List<Registro>>(response);

            return View(registrosModel);
        }

        public ActionResult ExportAction()
        {
            List<Registro> registrosModel = new List<Registro>();

            var response = this.GetRecords("https://saltala-cd91e.firebaseio.com/registros.json");
            registrosModel = JsonConvert.DeserializeObject<List<Registro>>(response);

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine("Nombre;Apellido;Razon;Aplicale;");
            textWriter.Flush();

            foreach (var registro in registrosModel)
            {
                textWriter.WriteLine(registro.Nombre + ";" + registro.Apellido + ";" + registro.Razon + ";" + registro.Aplicale + ";");
                textWriter.Flush();
            }

            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();

            return File(bytesInStream, "application/octet-stream", "Reports.csv");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GetRecords(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                return response;
            }
        }
    }

}
