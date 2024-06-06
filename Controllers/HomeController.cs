using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using use_open_source_fast_report.Models;
using FastReport.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Runtime.CompilerServices;
using FastReport.Data;
using Microsoft.AspNetCore.Html;
using FastReport.Utils;
using Newtonsoft.Json;
namespace use_open_source_fast_report.Controllers
{
    public class HomeController : Controller
    {
        static private Report report = null;//biến static lưu đánh dấu report
        private readonly IHostingEnvironment _hostingEnvironment;// lấy đường dẫn
        private readonly Context _context;// Tạo kết nối
        //Hàm khởi tạo
        public HomeController(Context context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        // Load report basic trong db với id=1 lên file Report
        /*        public void LoadReportBasic()
                {
                    try
                    {
                        // Create a string array with the lines of text
                        *//*string[] lines = { "First line", "Second line", "Third line" };*//*
                        string line = _context.Reports.SingleOrDefault(b => b.ID == 1).Content;
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
                }*/
        //Load report trong db với id lên file Report
        /*        public void LoadRepor(int id)
                {
                    var reports = _context.Reports.Where(t => t.ID == id);

                    if (reports.Count() != 0)
                    {
                        try
                        {
                            // Create a string array with the lines of text
                            string[] lines = { "First line", "Second line", "Third line" };
                            // Set a variable to the Documents path.
                            String line = _context.Reports.SingleOrDefault(b => b.ID == id).Content;
                            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            String pathReport = webRootPath + "/Reports/Report.frx";
                            // Write the string array to a new file named "WriteLines.txt".
                            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, pathReport)))
                            {
                                outputFile.WriteLine(line);
                            };
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }*/
        //Load report với nội dung (dùng trong edit xml)
        /*		public void LoadRepor(string content)
                {
                        try
                        {
                        // Create a string array with the lines of text
                        *//*string[] lines = { "First line", "Second line", "Third line" };*//*
                        // Set a variable to the Documents path.
                        report.Content = content;
                        String line = content;
                            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            String pathReport = webRootPath + "/Reports/Report.frx";
                            // Write the string array to a new file named "WriteLines.txt".
                            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, pathReport)))
                            {
                                outputFile.WriteLine(line);
                            };
                        }
                        catch (Exception e)
                        {

                        }
                    }*/
        /*		//Nạp report lên report design
                public void LoadReportToDesign()
                {
                    try
                    {
                        WebReport WebReport = new WebReport(); // Create a Web Report Object
                        WebReport.Width = "1000"; // Set the width of the report
                        WebReport.Height = "630"; // Set the height of the report
                        string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder                                                                   //string contentRootPath = _hostingEnvironment.ContentRootPath;
                        WebReport.Report.Load(webRootPath + "/Reports/Report.frx"); // Load the report into a WebReport object

        *//*                JsonDataConnection connection = new JsonDataConnection();
                        connection.ConnectionString = "Json=" + webRootPath + "/Reports/database.json";
                        connection.CreateAllTables();
                        WebReport.Dictionary.Connections.Add(connection);
                        WebReport.RegisterData(connection.DataSet);*//*

                        System.Data.DataSet dataSet = new System.Data.DataSet(); // Create a data source
                        dataSet.ReadXml(webRootPath + "/Reports/nwind.xml"); // Open the xml database
                        WebReport.Report.RegisterData(dataSet, "NorthWind"); //Register the data source in the report
                        WebReport.Mode = WebReportMode.Designer; // Set the mode of the web report object - display of the designer
                        WebReport.DesignerPath = "/WebReportDesigner/index.html"; // Specify the URL of the online designer
                        WebReport.DesignerSaveCallBack = "/Home/SaveDesignedReport"; // Set the view URL for the report save method
                        ViewBag.WebReport = WebReport; // pass report to View
                    }
                    catch (Exception e)
                    {

                    }
                }*/
        //Đọc file SaveReport
        public string ReadSaveReport()
        {
            string kq = "";
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(webRootPath + "/DesignedReports/SaveReport.frx"))
                {

                    // Read the stream as a string, and write the string to the console.
                    kq = sr.ReadToEnd();
                }
            }
            catch (Exception e) { }
            return kq;
        }
        //Save file SaveReport xuống db
        public void SaveRReportToDB()
        {
            String contenReport = ReadSaveReport();
            Report reportUpdate = new Report();
            reportUpdate.ID = report.ID;
            reportUpdate.Name = report.Name;
            reportUpdate.Description = report.Description;
            reportUpdate.Content = contenReport;
            _context.Update(reportUpdate);
            _context.SaveChanges();
        }
        public IActionResult Index()
        {
            return View();
        }
        //Lưu trong report design
        [HttpPost]
        public ActionResult SaveDesignedReport(string reportID, string reportUUID)
        {
            string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder ViewBag.Message = String.Format("Confirmed {0} {1}", reportID, reportUUID); // Set a message for view
            Stream reportForSave = Request.Body; // Write the result of the Post query to the stream
            string pathToSave = webRootPath + "/DesignedReports/SaveReport.frx"; // get the path to save the file
            using (FileStream file = new FileStream(pathToSave, FileMode.Create)) // Create a file stream {
            reportForSave.CopyTo(file); // Save the result of the query to a file
            SaveRReportToDB();//Lưu savereport xuống DB
            return View();
        }
        //Load report từ DB
        /*        [HttpGet]
                public IActionResult LoadReportDB(int id)
                {
                    try
                    {
                        LoadRepor(id);
                        LoadReportToDesign();
                        report = _context.Reports.SingleOrDefault(r => r.ID == id);
                        ViewBag.Content = report.Content;
                        return View();
                    }
                    catch (Exception e)
                    {
                        ViewBag.Content = "";
                        return View();
                    }
                }*/
        //Lấy nội dung report đưa lên textarea để edit
        [HttpGet]
        public JsonResult GetContent()
        {
            String content = _context.Reports.SingleOrDefault(b=>b.ID==report.ID).Content;
/*            string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder ViewBag.Message = String.Format("Confirmed {0} {1}", reportID, reportUUID); // Set a message for view          
            // Open the text file using a stream reader.
            using (var sr = new StreamReader(webRootPath + "/Reports/Report.frx"))
            {
                // Read the stream as a string, and write the string to the console.
                content = sr.ReadToEnd();
            }*/
            
            return new JsonResult(content);
        }
        //Lấy nội dung report từ textarea lưu về file report
        public JsonResult GetContenFromXML(EditXML model)
        {
            if (model.content != null)
            {
                WebReport WebReport = new WebReport(); // Create a Web Report Object
                WebReport.Width = "1200"; // Set the width of the report
                WebReport.Height = "550"; // Set the height of the report
                string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder                                                                   //string contentRootPath = _hostingEnvironment.ContentRootPath;
                WebReport.Report.LoadFromString(model.content); // Load the report into a WebReport object
                System.Data.DataSet dataSet = new System.Data.DataSet(); // Create a data source
                dataSet.ReadXml(webRootPath + "/Reports/nwind.xml"); // Open the xml database
                WebReport.Report.RegisterData(dataSet, "NorthWind"); //Register the data source in the report
                WebReport.Mode = WebReportMode.Designer; // Set the mode of the web report object - display of the designer
                WebReport.DesignerPath = "/WebReportDesigner/index.html"; // Specify the URL of the online designer
                WebReport.DesignerSaveCallBack = "/Home/SaveDesignedReport"; // Set the view URL for the report save method
                /*ViewBag.WebReport = WebReport; // pass report to View*/
                return new JsonResult(WebReport.Render());

            }
            else
            {
                return new JsonResult("Không thành công");
            }
        }
        /*        //Load report lên design online sau khi edit xml
                public IActionResult LoadReportEitXML()
                {
                    try
                    {
                        LoadReportToDesign();
                        ViewBag.Content = report.Content;
                        return View();
                    }
                    catch (Exception e)
                    {
                        ViewBag.Content = "";
                        return View();
                    }
                }*/
        //Lấy dữ liệu tất cả report
        [HttpGet]
        public JsonResult GetListReport()
        {
            var listReport = _context.Reports.ToList();
            return new JsonResult(listReport);
        }
        //load report with ajax
        [HttpGet]
        public JsonResult GetReportAjax(int id)
        {
            string datajson = "{\r\n    \"GoiThau\":[\r\n        {\r\n            \"MA\": null,\r\n            \"TEN\": null,\r\n            \"TEN_KHLCNT\": null,\r\n            \"USERNAME\": null,\r\n            \"FULLNAME\": null\r\n          }\r\n    ],\r\n    \"MoiThau\":[\r\n        {\r\n            \"STT\": null,\r\n            \"MAPL\": null,\r\n            \"TENPL\": null,\r\n            \"DVT\": null,\r\n            \"QUYCACH\": null,\r\n            \"KHOILUONG\": null,\r\n            \"DONGIA\": null,\r\n            \"YEUCAU\": null\r\n          }\r\n    ],\r\n    \"DuThau\":[\r\n        {\r\n            \"STT\": null,\r\n            \"MANHATHAU\": null,\r\n            \"TENNHATHAU\": null,\r\n            \"STTPL\": null,\r\n            \"MAPL\": null,\r\n            \"TENDUTHAU\": null,\r\n            \"SOLUUHANH\": null,\r\n            \"PHANLOAI\": null,\r\n            \"HANGSX\": null,\r\n            \"NUOCSX\": null,\r\n            \"CHUSOHUU\": null,\r\n            \"DVT\": null,\r\n            \"QUYCACH\": null,\r\n            \"SOLUONG\": null,\r\n            \"DONGIA\": null,\r\n            \"MAVTYT\": null,\r\n            \"CHUNGLOAI\": null,\r\n            \"KETLUAN\": null,\r\n            \"GHICHU\": null\r\n        }\r\n    ],\r\n    \"DangGiaChiTiet\":[\r\n        {\r\n            \"IDNOIDUNG\": null,\r\n            \"NOIDUNG\": null,\r\n            \"DAT\": null,\r\n            \"CHAPNHAN\": null,\r\n            \"KHONGDAT\": null,\r\n            \"NHANXET\": null,\r\n            \"GHICHU\": null\r\n        },\r\n        {\r\n            \"IDNOIDUNG\": null,\r\n            \"NOIDUNG\": null,\r\n            \"DAT\": null,\r\n            \"CHAPNHAN\": null,\r\n            \"KHONGDAT\": null,\r\n            \"NHANXET\": null,\r\n            \"GHICHU\": null\r\n        },\r\n        {\r\n            \"IDNOIDUNG\": null,\r\n            \"NOIDUNG\": null,\r\n            \"DAT\": null,\r\n            \"CHAPNHAN\": null,\r\n            \"KHONGDAT\": null,\r\n            \"NHANXET\": null,\r\n            \"GHICHU\": null\r\n        }\r\n    ]\r\n}";
            Data data = JsonConvert.DeserializeObject<Data>(datajson);
            report =_context.Reports.SingleOrDefault(r=> r.ID == id);
            WebReport WebReport = new WebReport(); // Create a Web Report Object
            WebReport.Width = "1200"; // Set the width of the report
            WebReport.Height = "550"; // Set the height of the report
            string webRootPath = _hostingEnvironment.WebRootPath; // Get the path to wwwroot folder                                                                   //string contentRootPath = _hostingEnvironment.ContentRootPath;
            WebReport.Report.LoadFromString(report.Content); // Load the report into a WebReport object

/*            WebReport.Report.RegisterData(data.DanhGiaChiTiets, "DanhGiaChiTiet");*/

            /*          System.Data.DataSet dataSet = new System.Data.DataSet(); // Create a data source
                      dataSet.ReadXml(webRootPath + "/Reports/nwind.xml"); // Open the xml database
                      WebReport.Report.RegisterData(dataSet, "NorthWind"); //Register the data source in the report*/
            WebReport.Mode = WebReportMode.Designer; // Set the mode of the web report object - display of the designer
            WebReport.DesignerPath = "/WebReportDesigner/index.html"; // Specify the URL of the online designer
            WebReport.DesignerSaveCallBack = "/Home/SaveDesignedReport"; // Set the view URL for the report save method
            /*            ViewBag.WebReport = WebReport; // pass report to View*/
            return new JsonResult(WebReport.Render());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
