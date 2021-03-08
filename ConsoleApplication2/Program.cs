using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
//            SqlConnection conn =
//                new SqlConnection(
//                    "Data Source=192.168.1.128;Initial Catalog=TnpdDB2018;Persist Security Info=True;User ID=tncgb;Password=!QAZ2wsx");
            
//            SqlCommand comm = new SqlCommand(@"SELECT          News.Id
//            FROM              News INNER JOIN
//            NewsCatalogNews ON News.Id = NewsCatalogNews.News_Id INNER JOIN
//            NewsCatalogs ON NewsCatalogNews.NewsCatalog_Id = NewsCatalogs.Id INNER JOIN
//            WebNewsCatalogs ON NewsCatalogs.WebCategoryId = WebNewsCatalogs.Id
//            WHERE          (WebNewsCatalogs.Id = 1 ) and StartDate>2021/1/1", conn);
//            SqlDataAdapter adapter = new SqlDataAdapter(comm);
//            DataTable dt = new DataTable();
//            adapter.Fill(dt);
//            Console.WriteLine("取得好人好事資料共" + dt.Rows.Count + "筆");
//            StringBuilder sb = new StringBuilder();
//            foreach (DataRow row in dt.Rows)
//            {
//                sb.Append("INSERT INTO NewsCatalogNews (NewsCatalog_Id, News_Id) VALUES (1, " + row[0]+ ");\n");
//            }
//            System.IO.File.WriteAllText("F:\\website\\sql.txt",sb.ToString());
            string[] file=System.IO.File.ReadAllText("F:\\website\\sql.txt").Split('\n');
            StringBuilder sb = new StringBuilder();
            foreach (string s in file)
            {
                sb.Append("update NewsCatalogNews set NewsCatalog_Id =2,News_Id="+ s+" where NewsCatalog_Id =1 and News_Id=" +s + '\n');
            }
            System.IO.File.WriteAllText("F:\\website\\sql1.txt", sb.ToString());
           
            Console.WriteLine("");
        }
    }
}
