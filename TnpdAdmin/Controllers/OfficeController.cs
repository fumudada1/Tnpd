using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace TnpdAdmin.Controllers
{
    public class OfficeController : Controller
    {
        //
        // GET: /Office/
        [HttpPost]
        public ActionResult WordToPdf(string url)
        {
            
            //string filePath = Server.MapPath(Server.UrlDecode(url));
            //FileInfo myInfo = new FileInfo(filePath);
            //if (myInfo.Exists)
            //{
            //    if (myInfo.Extension.ToLower() == ".docx")
            //    {

            //        // PDF 儲存位置
            //        string targetpdf = filePath.ToLower().Replace(".docx", ".pdf");
            //        string targetodf = filePath.ToLower().Replace(".docx", ".odf");


            //        //建立 word application instance
            //        Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            //        //開啟 word 檔案
            //        var wordDocument = appWord.Documents.Open(filePath);
            //        //匯出為 odf
            //        if (!System.IO.File.Exists(targetodf))
            //        {
            //            wordDocument.SaveAs2(targetodf,
            //                                  Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatOpenDocumentText);
            //        }

            //        //匯出為 pdf
            //        if (!System.IO.File.Exists(targetpdf))
            //        {
            //            Microsoft.Office.Interop.Word.WdSaveFormat wdSaveFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
            //            object oWdSaveFmt = wdSaveFmt, oSaveAsSpec = targetpdf;
            //            wordDocument.SaveAs2(ref oSaveAsSpec, ref oWdSaveFmt);
            //        }




            //        //關閉 word 檔
            //        wordDocument.Close();
            //        //結束 word
            //        appWord.Quit();
            //        GC.Collect();
            //    }
            //}
            return Content("success");

        }
    }
}
