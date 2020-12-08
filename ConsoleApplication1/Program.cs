using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = "select * from [dbo].[CaseTraffics] where Upfile1 like '%/%'";
            //var connectionString = "Data Source=10.128.0.41;Initial Catalog=TnpdDB;Persist Security Info=True;User ID=tnpd;Password=!QAZ2wsx#EDC";
            var connectionString = "Data Source=192.168.1.128;Initial Catalog=TnpdDB2018;Persist Security Info=True;User ID=tncgb;Password=!QAZ2wsx";
            string TrafficFiles = @"E:\website\Tnpd\tnpd\TrafficFiles\";

            SqlConnection conn=new SqlConnection(connectionString);
            SqlCommand comm=new SqlCommand(sql,conn);
            SqlDataAdapter da=new SqlDataAdapter(comm);
            DataTable dt=new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string[] Upfile1s = row["Upfile1"].ToString().Split('/');
                string[] Upfile2s = row["Upfile2"].ToString().Split('/');
                string[] Upfile3s = row["Upfile3"].ToString().Split('/');
                string[] Upfile4s = row["Upfile4"].ToString().Split('/');
                string[] Upfile5s = row["Upfile5"].ToString().Split('/');
                string[] Upfile6s = row["Upfile6"].ToString().Split('/');

                if (Upfile1s.Length==4)
                {
                    string Upfile1 = Upfile1s[Upfile1s.Length-1];
                    string sourcePath = TrafficFiles + row["Upfile1"].ToString().Replace("/", "\\");
                    string dPath = TrafficFiles + Upfile1s[3];
                    try
                    {
                        Console.WriteLine("copy " + sourcePath+ " to " + dPath);
                        System.IO.File.Copy(sourcePath, dPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                      
                        //throw;
                    }
                   

                    string Upfile2 = Upfile2s.Length == 1 ? "" : Upfile2s[Upfile1s.Length - 1];
                    string Upfile3 = Upfile3s.Length == 1 ? "" : Upfile3s[Upfile2s.Length - 1];
                    string Upfile4 = Upfile4s.Length == 1 ? "" : Upfile4s[Upfile3s.Length - 1];
                    string Upfile5 = Upfile5s.Length == 1 ? "" : Upfile5s[Upfile4s.Length - 1];
                    string Upfile6 = Upfile6s.Length == 1 ? "" : Upfile6s[Upfile5s.Length - 1];
                    sql =
                        "update [CaseTraffics] set Upfile1='" + Upfile1 + "',Upfile2='" + Upfile2 + "',Upfile3='" + Upfile3 + "',Upfile4='" + Upfile4 + "',Upfile5='" + Upfile5 + "',Upfile6='" + Upfile6 + "' where id=" + row["Id"];
                    SqlCommand comm1 = new SqlCommand(sql, conn);
                    conn.Open();
                    comm1.ExecuteNonQuery();
                    conn.Close();

                }
               
            }

        }
    }
}
