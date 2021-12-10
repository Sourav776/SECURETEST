using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using SECURETEST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SECURETEST.Controllers
{
    public class ReportController : Controller
    {
        private DataTable _dt;
        private static DataTable dt = new DataTable();
        private readonly SECURESOFT _db = new SECURESOFT();
        // GET: Report
        public ActionResult Index()
        {
            return View(new ReportModel());
        }
        public ActionResult ExportReport(ReportModel model)
        {
            var itemParam = new SqlParameter
            {
                ParameterName = "@itemName",
                Value = string.IsNullOrEmpty(model.ItemName)?"":model.ItemName
            };
           var list= _db.Database.SqlQuery<ResultModel>("exec GET_SALE_DETAILS_SP @itemName ", itemParam).AsParallel().ToList();
            //var resultData = _db.SALE_REPO.AsNoTracking().Where(x => x.STOCK.ITEM_NAME == model.ItemName).Select(x=>new { x.SALE_INVOICE,x.SALE.DATE,x.STOCK.ITEM_NAME,x.SALE_PRICE,x.QUANTITY}).ToList();
            var jsonData = JsonConvert.SerializeObject(list);
            _dt = ToDataTable(jsonData);

            var localReport = new LocalReport();
            var expriencesDataSource = new ReportDataSource { Name = "DataSet" };
            localReport.DataSources.Add(expriencesDataSource);
            expriencesDataSource.Value = _dt;
            localReport.ReportPath = Server.MapPath("~/Reports/report.rdlc");
            var deviceInfo = @"<DeviceInfo>
                    <EmbedFonts>None</EmbedFonts>
                   </DeviceInfo>";
            var renderedBytes = localReport.Render("PDF", deviceInfo, out var mimeType, out var encoding, out var fileNameExtension, out var streams, out var warnings);
            Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        public static DataTable ToDataTable(string jsonData)
        {
            try
            {
                dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }
    }
}