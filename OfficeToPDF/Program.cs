using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                string path = ConfigurationManager.AppSettings["path"];
                System.IO.DirectoryInfo di = new DirectoryInfo(path);
                foreach (DirectoryInfo directory in di.GetDirectories())
                {
                    FileInfo[] file = directory.GetFiles();
                    var newfiles = file.Where(x => x.LastWriteTime.AddMinutes(3) >= DateTime.Now).ToList();
                    foreach (FileInfo info in newfiles)
                    {
                        if (info.Extension == ".docx")
                        {
                            Console.WriteLine(info.FullName);
                            WordToPdf(info.FullName);
                           

                          
                        }

                    }
                }
                System.Threading.Thread.Sleep(2000);
            } while (true);



        }

        private static void WordToPdf(string filePath)
        {

            FileInfo myInfo = new FileInfo(filePath);
            if (myInfo.Exists)
            {
                if (myInfo.Extension.ToLower() == ".docx")
                {
                    try
                    {
                        // PDF 儲存位置
                        string targetpdf = filePath.ToLower().Replace(".docx", ".pdf");
                        string targetodf = filePath.ToLower().Replace(".docx", ".odf");


                        //建立 word application instance
                        Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                        //開啟 word 檔案
                        var wordDocument = appWord.Documents.Open(filePath);
                        //匯出為 odf
                        if (!System.IO.File.Exists(targetodf))
                        {
                            wordDocument.SaveAs2(targetodf,
                                Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatOpenDocumentText);
                        }

                        //匯出為 pdf
                        if (!System.IO.File.Exists(targetpdf))
                        {
                            Microsoft.Office.Interop.Word.WdSaveFormat wdSaveFmt = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                            object oWdSaveFmt = wdSaveFmt, oSaveAsSpec = targetpdf;
                            wordDocument.SaveAs2(ref oSaveAsSpec, ref oWdSaveFmt);
                        }




                        //關閉 word 檔
                        wordDocument.Close(false);
                        //結束 word
                        appWord.Quit(false);
                        GC.Collect();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        GC.Collect();
                        
                    }
                    
                }
            }
        }
    }
}
