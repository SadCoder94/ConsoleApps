using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Headers> data =  ReadFromExcel<List<Headers>>(filePath);

            foreach (var item in data)
            {
                Console.WriteLine(item.col1+" "+item.col2+" "+item.col3);
            }
        }
        public static string filePath = @"D:\TrialExcel.xlsx";
        //https://www.thecodebuzz.com/read-write-excel-in-dotnet-core-epplus/

        private static T ReadFromExcel<T>(string path, bool hasHeader = true)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPack = new ExcelPackage())
            {
                //Load excel stream
                using (var stream = File.OpenRead(path))
                {
                    excelPack.Load(stream);
                }

                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets[0];

                //Get all details as DataTable -because Datatable make life easy :)
                DataTable excelasTable = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    //Get colummn details
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                        excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                    }
                }
                var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                //Get everything as generics and let end user decides on casting to required type
                var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
                return (T)Convert.ChangeType(generatedType, typeof(T));
            }
        }

        private byte[] WriteToExcel<T>(string reportName, List<T> reportValues)
        {
            DataTable dataforExtract = CreateInsertionStructure(reportValues);
            FileInfo templatefile = new FileInfo(@"GenericExtractReport.xlsx");
            ExcelPackage excelPackage = new ExcelPackage(templatefile);
            using (excelPackage)
            {
                var ws = excelPackage.Workbook.Worksheets[1];
                ws.Name = reportName;

                ws.Cells[2, 2].Value = reportName;
                ws.Cells[5, 2].LoadFromDataTable(dataforExtract, true);

                if (dataforExtract.Rows.Count == 0)//if the report does not contain any rows, enter the column headers or the report is blank. 
                {
                    var colindex = 2;
                    foreach (DataColumn column in dataforExtract.Columns)
                    {
                        ws.Cells[5, colindex].Value = column.ColumnName;
                        colindex++;
                    }
                }

                if (reportValues.Count > 0)//style row data only when available, leads to null exceptions otherwise.
                {
                    var end = ws.Dimension.End;
                    var cellRange = ws.Cells[6, 2, end.Row, end.Column];

                    cellRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    cellRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    cellRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    cellRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    cellRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(206, 206, 206));

                    cellRange.Style.Border.Top.Color.SetColor(Color.FromArgb(206, 206, 206));
                    cellRange.Style.Border.Left.Color.SetColor(Color.FromArgb(206, 206, 206));
                    cellRange.Style.Border.Right.Color.SetColor(Color.FromArgb(206, 206, 206));
                    cellRange.Style.Border.Bottom.Color.SetColor(Color.FromArgb(206, 206, 206));

                    cellRange.Style.WrapText = true;
                }
                return excelPackage.GetAsByteArray();
            }
        }

        private DataTable CreateInsertionStructure<T>(List<T> reportValues)
        {
            DataTable dataForEntry = new DataTable();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Dictionary<string, string> alterativeHeadings = GetAlternativeHeadings();

            foreach (PropertyInfo prop in props)
            {
                //Set column names as property names replacing underscore with space
                string propName = prop.Name;
                propName = propName.Replace('_', ' ');
                dataForEntry.Columns.Add(propName);
            }

            foreach (T item in reportValues)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataForEntry.Rows.Add(values);
            }

            foreach (DataColumn column in dataForEntry.Columns)
            {
                alterativeHeadings.TryGetValue(column.ColumnName, out string actualName);
                if (!string.IsNullOrEmpty(actualName))
                    column.ColumnName = actualName;
            }
            return dataForEntry;
        }

        private Dictionary<string, string> GetAlternativeHeadings()
        {
            string dictionaryPath = @"alternativeHeadings.json";

            try
            {
                using StreamReader r = new StreamReader(dictionaryPath);
                string json = r.ReadToEnd();
                Dictionary<string, string> dictionaryData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dictionaryData;
            }
            catch(Exception)
            {
                return new Dictionary<string, string>();
            }
        }

    }
}
