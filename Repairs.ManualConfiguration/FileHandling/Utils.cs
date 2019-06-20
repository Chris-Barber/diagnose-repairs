namespace Repairs.ManualConfiguration.FileHandling
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics;
    using System.IO;

    using Microsoft.VisualBasic.FileIO;

    public static class Utils
    {
        public static DataTable ReadXls(string path)
        {
            var xtn = Path.GetExtension(path);
            var dt = new DataTable();

            var connString = xtn == ".xls" ? $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={path};Extended Properties=Excel 8.0" : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";

            // Create the connection object 
            var oleDbConnection = new OleDbConnection(connString);
            try
            {
                // Open connection
                oleDbConnection.Open();

                var schema = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (schema == null || schema.Rows.Count < 1)
                {
                    throw new ApplicationException("Error: Could not determine the name of the first worksheet.");
                }

                var firstSheetName = schema.Rows[0]["TABLE_NAME"].ToString();
                
                // Create OleDbCommand object and select data from worksheet Sheet1
                var cmd = new OleDbCommand("SELECT * FROM [" + firstSheetName + "]", oleDbConnection);

                // Create new OleDbDataAdapter 
                var oleDbDataAdapter = new OleDbDataAdapter
                {
                    SelectCommand = cmd
                };

                // Fill the DataSet from the data extracted from the worksheet.
                oleDbDataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                // Close connection
                oleDbConnection.Close();
            }

            return dt;
        }

        public static IList<string[]> ReadCsv(string path)
        {
            IList<string[]> rows = new List<string[]>();
            var parser = new TextFieldParser(path)
            {
                HasFieldsEnclosedInQuotes = true
            };

            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                rows.Add(fields);
            }

            parser.Close();

            return rows;
        }
    }
}
