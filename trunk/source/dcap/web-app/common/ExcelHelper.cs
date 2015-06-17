using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using web_app.DcapServiceReference;

namespace web_app.common
{
    public class ExcelHelper
    {
        public static DataTable CreateEmptyDataTable(String tableName, String[] columnNames, Type[] columnTypes)
        {
            var dataTable = new DataTable(tableName);
            for (int i = 0; i < columnNames.Length; i++)
            {
                dataTable.Columns.Add(columnNames[i], columnTypes[i]);
            }
            return dataTable;
        }

        /// <summary>
        /// Write excel file of a list of object as T
        /// Assume that maximum of 24 columns 
        /// </summary>
        /// <typeparam name="T">Object type to pass in</typeparam>
        /// <param name="fileName">Full path of the file name of excel spreadsheet</param>
        /// <param name="objects">list of the object type</param>
        /// <param name="sheetName">Sheet names of Excel File</param>
        /// <param name="headerNames">Header names of the object</param>
        public void Create(
            string fileName,
            DataTable dt)
        {
            //Open the copied template workbook. 
            using (SpreadsheetDocument myWorkbook =
                   SpreadsheetDocument.Create(fileName,
                   SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = myWorkbook.AddWorkbookPart();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                // Create Styles and Insert into Workbook
                var stylesPart =
                    myWorkbook.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                Stylesheet styles = new CustomStylesheet();
                styles.Save(stylesPart);
                string relId = workbookPart.GetIdOfPart(worksheetPart);
                var workbook = new Workbook();
                var fileVersion =
                    new FileVersion
                    {
                        ApplicationName =
                            "Microsoft Office Excel"
                    };
                var worksheet = new Worksheet();
                int numCols = dt.Columns.Count;
                var columns = new Columns();
                for (int col = 0; col < numCols; col++)
                {
                    int width = dt.Columns[col].ToString().Length + 5;
                    Column c = new CustomColumn((UInt32)col + 1,
                                  (UInt32)numCols + 1, width);
                    columns.Append(c);
                }
                worksheet.Append(columns);
                var sheets = new Sheets();
                var sheet = new Sheet { Name = dt.TableName, SheetId = 1, Id = relId };
                sheets.Append(sheet);
                workbook.Append(fileVersion);
                workbook.Append(sheets);
                SheetData sheetData = CreateSheetData(dt);
                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                UpdateBorderStyle(workbookPart, worksheetPart, dt);
                worksheetPart.Worksheet.Save();
                myWorkbook.WorkbookPart.Workbook = workbook;
                myWorkbook.WorkbookPart.Workbook.Save();
                myWorkbook.Close();
            }
        }

        private void UpdateBorderStyle(WorkbookPart workbookPart, WorksheetPart worksheetPart, DataTable dt)
        {
            char achar = 'A';
            for (int nCol = 0; nCol < dt.Columns.Count; nCol++)
            {
                for (int nRow = 0; nRow < dt.Rows.Count; nRow++)
                {
                    string cellAddress = char.ToString((char)(achar + nCol)) + (nRow+2);
                    Cell cell = GetCell(worksheetPart, cellAddress);
                    CellFormat cellFormat = cell.StyleIndex != null ? GetCellFormat(workbookPart, cell.StyleIndex).CloneNode(true) as CellFormat : new CellFormat();
                    cellFormat.BorderId = InsertBorder(workbookPart, GenerateBorder());
                    cell.StyleIndex = InsertCellFormat(workbookPart, cellFormat);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Object type to pass in</typeparam>
        /// <param name="objects">list of the object type</param>
        /// <param name="headerNames">Header names of the object</param>
        /// <returns></returns>
        private static SheetData CreateSheetData(DataTable dt)
        {
            var sheetData = new SheetData();
            if (dt.Rows.Count > 0)
            {
                //Get a list of A to Z
                var az = new List<Char>(Enumerable.Range('A', 'Z' -
                                      'A' + 1).Select(i => (Char)i).ToArray());
                //A to E number of columns 
                List<Char> headers = az.GetRange(0, dt.Columns.Count);
                int numRows = dt.Rows.Count;
                int numCols = dt.Columns.Count;
                var header = new Row();
                int index = 1;
                header.RowIndex = (uint)index;
                for (int col = 0; col < numCols; col++)
                {
                    HeaderCell c = new HeaderCell(headers[col].ToString(), dt.Columns[col].ToString(), index);
                    header.Append(c);
                }
                sheetData.Append(header);
                for (int i = 0; i < numRows; i++)
                {
                    index++;
                    var obj1 = dt.Rows[i];
                    var r = new Row { RowIndex = (uint)index };
                    for (int col = 0; col < numCols; col++)
                    {
                        object obj = obj1[col];
                        if (obj != null)
                        {
                            if (obj.GetType() == typeof(string))
                            {
                                var c = new TextCell(headers[col].ToString(),
                                            obj.ToString(), index);
                                r.Append(c);
                            }
                            else if (obj.GetType() == typeof(bool))
                            {
                                string value =
                                    (bool)obj ? "Yes" : "No";
                                var c = new TextCell(headers[col].ToString(),
                                                        value, index);
                                r.Append(c);
                            }
                            else if (obj.GetType() == typeof(DateTime))
                            {
                                var c = new DateCell(headers[col].ToString(),
                                            (DateTime)obj, index);
                                r.Append(c);
                            }
                            else if (obj.GetType() == typeof(decimal) ||
                                        obj.GetType() == typeof(double))
                            {
                                var c = new FormatedNumberCell(
                                                headers[col].ToString(),
                                                obj.ToString(), index);
                                r.Append(c);
                            }
                            else
                            {
                                long value;
                                if (long.TryParse(obj.ToString(), out value))
                                {
                                    var c = new NumberCell(headers[col].ToString(),
                                                obj.ToString(), index);
                                    r.Append(c);
                                }
                                else
                                {
                                    var c = new TextCell(headers[col].ToString(),
                                                obj.ToString(), index);
                                    r.Append(c);
                                }
                            }
                        }
                    }
                    sheetData.Append(r);
                }
            }
            return sheetData;
        }

        private static Cell GetCell(WorksheetPart workSheetPart, string cellAddress)
        {
            return workSheetPart.Worksheet.Descendants<Cell>()
                                        .SingleOrDefault(c => cellAddress.Equals(c.CellReference));
        }

        private static CellFormat GetCellFormat(WorkbookPart workbookPart, uint styleIndex)
        {
            return workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First().Elements<CellFormat>().ElementAt((int)styleIndex);
        }

        private static uint InsertCellFormat(WorkbookPart workbookPart, CellFormat cellFormat)
        {
            CellFormats cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.Elements<CellFormats>().First();
            cellFormats.Append(cellFormat);
            return (uint)cellFormats.Count++;
        }

        private static Border GenerateBorder()
        {
            Border border2 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color1);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color2);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color3);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color4 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color4);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            return border2;
        }

        private static uint InsertBorder(WorkbookPart workbookPart, Border border)
        {
            Borders borders = workbookPart.WorkbookStylesPart.Stylesheet.Elements<Borders>().First();
            borders.Append(border);
            return (uint)borders.Count++;
        }
    }
}