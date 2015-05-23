using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelLibrary.SpreadSheet;
using System.Data;

namespace core_lib.common
{
    public class ExcelUtil
    {
        public static void CreateExcel(List<object[]>[] ds, List<string[]> listFields, string[] sheetNames, string filename)
        {
            GC.Collect();

            Workbook workbook = new Workbook();
            for (int i = 0; i < sheetNames.Length; i++)
            {
                string sheetName = sheetNames[i];
                List<object[]> dt = ds[i];
                Worksheet worksheet = new Worksheet(sheetName);
                fillSheetContent(worksheet, dt, listFields[i]);
                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(filename);

            GC.Collect();

            //            FileInfo file = new FileInfo(filename);
            //            Response.Clear();
            //            Response.Charset = "UTF-8";
            //            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Add header, give a default file name for "File Download/Store as"
            //            Response.AddHeader("Content-Disposition", "attachment; filename="
            //          + Server.UrlEncode(file.Name));
            //Add header, set file size to enable browser display download progress
            //            Response.AddHeader("Content-Length", file.Length.ToString());
            //Set the return string is unavailable reading for client, and must be downloaded
            //            Response.ContentType = "application/ms-excel";
            //Send file string to client 
            //            Response.WriteFile(file.FullName);
            //Stop execute  
            //            Response.End();
        }

        private static void fillSheetContent(Worksheet worksheet, List<object[]> dt, string[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                worksheet.Cells[0, i] = new Cell(fields[i]);
            }
            for (int r = 0; r < dt.Count; r++)
            {
                for (int i = 0; i < dt[r].Length; i++)
                {
                    object o = dt[r][i];
                    if (o == null)
                        continue;
                    if (o is DateTime)
                    {
                        worksheet.Cells[r + 1, i] = new Cell(String.Format("{0:yyyyMMdd}", (DateTime)o));
                    }
                    else
                    {
                        worksheet.Cells[r + 1, i] = new Cell(o.ToString());
                    }
                }
            }
        }

        public static int dateCompare(DateTime d1, DateTime d2)
        {
            string v1 = String.Format("{0:yyyyMMdd}", d1);
            string v2 = String.Format("{0:yyyyMMdd}", d2);
            return v1.CompareTo(v2);
        }

        public static void CreateExcel(DataSet ds, string[] sheetNames, string filename)
        {
            GC.Collect();

            Workbook workbook = new Workbook();
            for (int i = 0; i < sheetNames.Length; i++)
            {
                string sheetName = sheetNames[i];
                DataTable dt = ds.Tables[sheetName];
                Worksheet worksheet = new Worksheet(dt.TableName);
                fillSheetContent(worksheet, dt);
                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(filename);

            GC.Collect();

            //            FileInfo file = new FileInfo(filename);
            //            Response.Clear();
            //            Response.Charset = "UTF-8";
            //            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Add header, give a default file name for "File Download/Store as"
            //            Response.AddHeader("Content-Disposition", "attachment; filename="
            //          + Server.UrlEncode(file.Name));
            //Add header, set file size to enable browser display download progress
            //            Response.AddHeader("Content-Length", file.Length.ToString());
            //Set the return string is unavailable reading for client, and must be downloaded
            //            Response.ContentType = "application/ms-excel";
            //Send file string to client 
            //            Response.WriteFile(file.FullName);
            //Stop execute  
            //            Response.End();
        }

        private static void fillSheetContent(Worksheet worksheet, DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    object o = dt.Rows[r][i];
                    if (o is DateTime)
                    {
                        worksheet.Cells[r + 1, i] = new Cell(String.Format("{0:yyyyMMdd}", (DateTime)o));
                    }
                    else
                    {
                        worksheet.Cells[r + 1, i] = new Cell(o.ToString());
                    }
                }
            }
        }
    }
}
