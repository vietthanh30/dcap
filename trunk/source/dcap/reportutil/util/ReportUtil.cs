using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using core_lib.common;
using System.Windows.Forms;
using System.IO;

namespace reportutil.util
{
    public class ReportUtil
    {
        public static void CreateExcel(List<object[]>[] ds, List<string[]> listFields, string[] sheetNames, string filename)
        {
            ExcelUtil.CreateExcel(ds, listFields, sheetNames, filename);
        }

        public static void CreateExcel(DataSet ds, string[] sheetNames, string filename)
        {
            ExcelUtil.CreateExcel(ds, sheetNames, filename);
        }

        private static List<string> GetListTableIndex(List<Hashtable> listSource)
        {
            var result = new List<string>();
            for (var index = 0; index < listSource.Count; index++)
            {
                Hashtable source = listSource[index];
                result.Add((string)source[ConstUtil.TABLE_INDEX]);
            }
            result.Sort(SortTableIndex);
            return result;
        }

        private static int SortTableIndex(string s1, string s2)
        {
            return s1.CompareTo(s2);
        }

        public static DataTable CreateReportDataTable(DataTable sourceTable, List<string> columnNames)
        {
            var dataTable = new DataTable();
            foreach (var columnName in columnNames)
            {
                dataTable.Columns.Add(columnName);
            }
            foreach (DataRow oneRow in sourceTable.Rows)
            {
                var dataRow = dataTable.NewRow();
                foreach (var columnName in columnNames)
                {
                    var data = oneRow[columnName];
                    if (data != DBNull.Value && data.GetType().Equals(typeof(DateTime)))
                        dataRow[columnName] = DateUtil.GetDateTimeAsDdmmyyyy((DateTime)data);
                    else
                        dataRow[columnName] = Convert.ToString(data);
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        public static Hashtable CreateSource(DataTable dataTable, List<string> columnNames, string tableIndex, string isHeader, string includeHeader)
        {
            var map = new Hashtable();
            map.Add(ConstUtil.COLUMN_NAMES, columnNames);
            map.Add(ConstUtil.SOURCE_TABLE, dataTable);
            map.Add(ConstUtil.TABLE_INDEX, tableIndex);
            map.Add(ConstUtil.IS_HEADER, isHeader);
            map.Add(ConstUtil.INCLUDE_HEADER, includeHeader);
            return map;
        }

        public static Hashtable CreateSource(DataTable dataTable, List<string> columnNames, string tableIndex)
        {
            return CreateSource(dataTable, columnNames, tableIndex, "0");
        }

        public static Hashtable CreateSource(DataTable dataTable, List<string> columnNames, string tableIndex, string isHeader)
        {
            return CreateSource(dataTable, columnNames, tableIndex, isHeader, "0");
        }

        public static void UpdateParamUsingBookmark(Hashtable mapParams)
        {
            foreach (DictionaryEntry entry in mapParams)
            {
                WordAppUtil.SetParameterUsingBookmark(entry.Key.ToString(), entry.Value);
            }
        }

        public static bool CheckTemplatesDir()
        {
            if (!ParameterUtil.UseWord)
                return true;
            bool returnCode;
            GetTemplatesDoc(out returnCode);
            return returnCode;
        }

        public static string GetTemplatesDoc(out bool returnCode)
        {
            var templatesDoc = ParameterUtil.GetParameter("templates_doc");
            if (String.IsNullOrEmpty(templatesDoc))
            {
                MessageBox.Show("Thiếu thông tin cấu hình templates_doc", "Thông báo", MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                returnCode = false;
                return templatesDoc;
            }
            if (!Directory.Exists(templatesDoc))
            {
                MessageBox.Show("Không tồn tại thư mục: \"" + templatesDoc + "\"", "Thông báo", MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                returnCode = false;
                return templatesDoc;
            }
            returnCode = true;
            return templatesDoc;
        }

        public static bool UpdateTempFile(string templateDir, string reportName, out string tempFile)
        {
            bool returnCode;
            tempFile = "";
            string templatesDoc = GetTemplatesDoc(out returnCode);
            if (!returnCode)
                return false;
            string tempDir;
            if (!String.IsNullOrEmpty(templateDir))
                tempDir = templatesDoc + "/" + templateDir;
            else
                tempDir = templatesDoc;
            var templatePath = tempDir + "/" + reportName + ".doc";
            tempFile = tempDir + "/" + reportName + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".doc";
            File.Copy(templatePath, tempFile);
            WordAppUtil.Template = tempFile;
            return true;
        }

        public static String OnCreateReportUsingBookmarkAutoIndexWithSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams, string reportName, string templateDir)
        {
            return OnCreateReportUsingBookmark(reportTitle, listSource, mapParams, reportName, templateDir, true, true);
        }
        public static String OnCreateReportUsingBookmarkAutoIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams, string reportName, string templateDir)
        {
            return OnCreateReportUsingBookmark(reportTitle, listSource, mapParams, reportName, templateDir, false, true);
        }
        public static String OnCreateReportUsingBookmark(string reportTitle, List<Hashtable> listSource, Hashtable mapParams, string reportName, string templateDir,
            bool includeSumRow, bool autoIndex)
        {
            string tempFile;
            bool returnCode = UpdateTempFile(templateDir, reportName, out tempFile);
            if (!returnCode)
                return "";
            try
            {
                // tao wordApp tu template
                WordAppUtil.CreateMsWordFromTemplate();
                UpdateTableData(listSource, autoIndex, includeSumRow, false);
                UpdateParamUsingBookmark(mapParams);
            }
            finally
            {
                WordAppUtil.QuitMsWordApp(true);
            }
            return tempFile;
        }

        public static void DoMergeCell(Hashtable mapMergeCell)
        {
            foreach (DictionaryEntry entry in mapMergeCell)
            {
                int index;
                if (int.TryParse(entry.Key.ToString(), out index))
                    WordAppUtil.DoMergeCell(index, (Hashtable)entry.Value);
            }
        }

        public static void UpdateTableData(List<Hashtable> listSource, bool autoIndex, bool includeSumRow, bool isItalicHeader)
        {
            var mapTableIndex = new Hashtable();
            foreach (Hashtable map in listSource)
            {
                var columnNames = map[ConstUtil.COLUMN_NAMES] as List<string>;
                var sourceTable = map[ConstUtil.SOURCE_TABLE] as DataTable;
                var tableIndex = map[ConstUtil.TABLE_INDEX] as string;
                var isHeader = map[ConstUtil.IS_HEADER] as string;
                var includeHeader = map[ConstUtil.INCLUDE_HEADER] as string;
                WordAppUtil.ColumnNames = columnNames;
                WordAppUtil.SourceTable = sourceTable;
                if (!String.IsNullOrEmpty(tableIndex))
                {
                    var index = Convert.ToInt32(tableIndex);
                    int i;
                    if (mapTableIndex.ContainsKey(index))
                        i = (int)mapTableIndex[index];
                    else
                        i = 1;
                    var isFirstTime = (i == 1 && !mapTableIndex.ContainsKey(index));
                    if (autoIndex)
                        WordAppUtil.FillDataToWordDocTable2(index, includeSumRow, isFirstTime);
                    else
                    {
                        if ("1".Equals(isHeader))
                            WordAppUtil.FillDataToWordDocTableWithoutStt2(index, true, isItalicHeader, false, "1".Equals(includeHeader), includeSumRow, isFirstTime);
                        else
                            WordAppUtil.FillDataToWordDocTableWithoutStt2(index, false, false, false, "1".Equals(includeHeader), includeSumRow, isFirstTime);
                    }
                    if (mapTableIndex.ContainsKey(index))
                        mapTableIndex.Remove(index);
                    mapTableIndex.Add(index, i + 1);
                }
            }
        }

        public static void UpdateParamUsingMergeField(Hashtable mapParams, string prefixParam, string posfixParam)
        {
            foreach (DictionaryEntry entry in mapParams)
            {
                var key = entry.Key.ToString();
                if (key.Length < prefixParam.Length || !prefixParam.Equals(key.Substring(0, prefixParam.Length)))
                    key = prefixParam + key;
                if (!posfixParam.Equals(key.Substring(key.Length - posfixParam.Length)))
                    key += posfixParam;
                WordAppUtil.SetParameterUsingMergeField(key, entry.Value);
            }
        }

        public static String OnCreateReportUsingFieldManualIndexSumRowItalicHeader(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, true, true,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldManualIndexWithoutSumRowItalicHeader(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, false, true,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldAutoIndexSumRowItalicHeader(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, true, true,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldAutoIndexWithoutSumRowItalicHeader(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, false, true,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldManualIndexSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, true, false,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldManualIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, false, false,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldAutoIndexSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, mapParams, reportName, templateDir, true, false,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldAutoIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable fieldParams, Hashtable bookMarkParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, fieldParams, bookMarkParams, reportName, templateDir, false, false,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldAutoIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable fieldParams,
            string reportName, string templateDir, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingField(reportTitle, listSource, fieldParams, reportName, templateDir, false, false,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingField(string reportTitle, List<Hashtable> listSource, Hashtable fieldParams,
            string reportName, string templateDir, bool includeSumRow, bool isItalicHeader, bool autoIndex, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, fieldParams, reportName, templateDir,
                                        new Hashtable(), includeSumRow, isItalicHeader, autoIndex, prefixParam,
                                        posfixParam);
        }
        public static String OnCreateReportUsingField(string reportTitle, List<Hashtable> listSource, Hashtable fieldParams, Hashtable bookMarkParams,
            string reportName, string templateDir, bool includeSumRow, bool isItalicHeader, bool autoIndex, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, fieldParams, bookMarkParams, reportName, templateDir,
                                        new Hashtable(), includeSumRow, isItalicHeader, autoIndex, prefixParam,
                                        posfixParam);
        }

        public static String OnCreateReportUsingFieldMergeCellManualIndexSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, Hashtable mapMergeCell, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, mapParams, reportName, templateDir, mapMergeCell, true, false,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldMergeCellManualIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, Hashtable mapMergeCell, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, mapParams, reportName, templateDir, mapMergeCell, false, false,
                                     false, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldMergeCellAutoIndexSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, Hashtable mapMergeCell, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, mapParams, reportName, templateDir, mapMergeCell, true, false,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldMergeCellAutoIndexWithoutSumRow(string reportTitle, List<Hashtable> listSource, Hashtable mapParams,
            string reportName, string templateDir, Hashtable mapMergeCell, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, mapParams, reportName, templateDir, mapMergeCell, false, false,
                                     true, prefixParam, posfixParam);
        }
        public static String OnCreateReportUsingFieldMergeCell(string reportTitle, List<Hashtable> listSource, Hashtable fieldParams, string reportName, string templateDir, Hashtable mapMergeCell, bool includeSumRow, bool isItalicHeader, bool autoIndex, string prefixParam, string posfixParam)
        {
            return OnCreateReportUsingFieldMergeCell(reportTitle, listSource, fieldParams, new Hashtable(), reportName,
                                              templateDir, mapMergeCell, includeSumRow, isItalicHeader, autoIndex,
                                              prefixParam, posfixParam);
        }

        public static String OnCreateReportUsingFieldMergeCell(string reportTitle, List<Hashtable> listSource,
            Hashtable fieldParams, Hashtable bookMarkParams, string reportName, string templateDir,
            Hashtable mapMergeCell, bool includeSumRow, bool isItalicHeader, bool autoIndex, string prefixParam, string posfixParam)
        {
            string tempFile;
            bool returnCode = UpdateTempFile(templateDir, reportName, out tempFile);
            if (!returnCode)
                return "";
            try
            {
                // tao wordApp tu template
                WordAppUtil.CreateMsWordFromTemplate();
                UpdateTableData(listSource, autoIndex, includeSumRow, isItalicHeader);
                DoMergeCell(mapMergeCell);
                UpdateParamUsingMergeField(fieldParams, prefixParam, posfixParam);
                UpdateParamUsingBookmark(bookMarkParams);
            }
            finally
            {
                WordAppUtil.QuitMsWordApp(true);
            }
            return tempFile;
        }
    }
}
