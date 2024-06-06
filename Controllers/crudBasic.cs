

// Không sử dụng, tài liệu để kham khảo, test

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using use_open_source_fast_report.Models;
using System;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using FastReport.Web;

namespace use_open_source_fast_report.Controllers
{
    public class crudBasic : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Context _context;
        public crudBasic( Context context, IHostingEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Ten = _context.Reports.SingleOrDefault(lo => lo.ID == 1).Name;
            ViewBag.Name = Ten;
            return View();
        }
        public JsonResult Get()
        {
            var DSReport = _context.Reports.ToList();
            return new JsonResult(DSReport);
        }
        public JsonResult ReadfileFRX()
        {
            string kq = "";
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(webRootPath + "/Reports/Report.frx"))
                {
                  
                    // Read the stream as a string, and write the string to the console.
                     kq=sr.ReadToEnd();
                }
            }
            catch (Exception ex) { }
            return  new JsonResult(kq);
        }
        public JsonResult SaveReport()
        {
            string Content = "";
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(webRootPath + "/DesignedReports/SaveReport.frx"))
                {

                    // Read the stream as a string, and write the string to the console.
                    Content = sr.ReadToEnd();
                }
            }

            catch (Exception ex) { }
            Report report = new Report();
            report.Content = Content;
            report.Name = "Luu report";
            _context.Add(report);
            _context.SaveChanges();
            return new JsonResult("Thành công");
        }
        public JsonResult WriteFileFRX(int id)
        {
            // Create a string array with the lines of text
            /*string[] lines = { "First line", "Second line", "Third line" };*/
            string line = _context.Reports.SingleOrDefault(b => b.ID == id).Content;
            // Set a variable to the Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string webRootPath = _hostingEnvironment.WebRootPath;
            String pathReport = webRootPath + "/Reports/Report.frx";
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, pathReport)))
            {
                    outputFile.WriteLine(line);
            }
            return new JsonResult("Thành công");
        }
        public void LoadRepor(int id)
        {
            try
            {
                // Create a string array with the lines of text
                /*string[] lines = { "First line", "Second line", "Third line" };*/
                string line = _context.Reports.SingleOrDefault(b => b.ID == id).Content;
                // Set a variable to the Documents path.
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string webRootPath = _hostingEnvironment.WebRootPath;
                String pathReport = webRootPath + "/Reports/Report.frx";
                // Write the string array to a new file named "WriteLines.txt".
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, pathReport)))
                {
                    outputFile.WriteLine(line);
                };
            }
            catch
            {

            }
        }
        public void LoadReportToDesign()
        {
            try
            {
                WebReport WebReport = new WebReport(); // Create a Web Report Object
                WebReport.Width = "1000"; // Set the width of the report
                WebReport.Height = "1000"; // Set the height of the report
                string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder                                                                   //string contentRootPath = _hostingEnvironment.ContentRootPath;
                WebReport.Report.Load(webRootPath + "/Reports/Report.frx"); // Load the report into a WebReport object
                System.Data.DataSet dataSet = new System.Data.DataSet(); // Create a data source
                dataSet.ReadXml(webRootPath + "/Reports/nwind.xml"); // Open the xml database
                WebReport.Report.RegisterData(dataSet, "NorthWind"); //Register the data source in the report

                WebReport.Mode = WebReportMode.Designer; // Set the mode of the web report object - display of the designer
                WebReport.DesignerPath = "/WebReportDesigner/index.html"; // Specify the URL of the online designer
                WebReport.DesignerSaveCallBack = "/Home/SaveDesignedReport"; // Set the view URL for the report save method
                ViewBag.WebReport = WebReport; // pass report to View
            }
            catch
            {
            }
        }
        public JsonResult LoadReportDB(int id)
        {
            try
            {
                this.LoadRepor(id);
                this.LoadReportToDesign();
                return new JsonResult("Thành công");
            }
            catch
            {
                return new JsonResult("Không thành công");
            }

        }
    }
}
