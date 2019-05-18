/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Threading.Tasks;

namespace vshed.IO
{
    
    public static class ExtendedFileInfo
    {
        public static string md5sum(this FileInfo fileInfo)
        {
            fileInfo.md5sumAsync().Wait();            
            return (string)fileInfo.GetValue("md5sum");
        }
        public static Task<string> md5sumAsync(this FileInfo fileInfo)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                try
                {
                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        string hash = (string)fileInfo.GetValue("md5sum")
                                        ?? BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "");
                        if (!(hash is null)) { fileInfo.SetValue("md5sum", hash); }
                        return Task.FromResult<string>(hash);
                    }
                }
                catch (System.UnauthorizedAccessException) { return null; }
                catch (System.IO.IOException) { return null; }
            }
        }

        private static DataTable tableSchema;
        private static DataRow NewRow(FileInfo fileInfo)
        {
            if (tableSchema is null)
            {
                //Build columns from object
                tableSchema = new DataTable("Temp");
                foreach (var s in fileInfo.GetType().GetProperties())
                { tableSchema.Columns.Add(s.Name); }
                tableSchema.Columns.Add("md5sum");
            }
            return tableSchema.NewRow();
        }
        public static DataRow ToDataRow(this FileInfo fileInfo, DataRow dataRow = null)
        {
            if (dataRow is null) { dataRow = NewRow(fileInfo); }
            //Fill in row columns - FileInfo Property or ExtendedFileInfo Method or Null
            foreach (string c in dataRow.Table.Columns.Cast<DataColumn>().Select(n => n.ColumnName))
            {
                dataRow[c] = fileInfo.GetType()?.GetProperty(c)?.GetValue(fileInfo) ?? typeof(ExtendedFileInfo)?.GetMethod(c).Invoke(fileInfo, new[] { fileInfo }) ?? null; 
            }
            return dataRow;
        }

        public static DataRow[] ToDataRows(this List<FileInfo> fileInfos, DataTable dataTable = null)
        { return ToDataRows(fileInfos.ToArray(), dataTable); }
        public static DataRow[] ToDataRows(this FileInfo[] fileInfos, DataTable dataTable = null)
        {
            List<DataRow> dataRows = new List<DataRow>();

            if (dataTable is null)
            {
                dataTable = new DataTable("Files");
                foreach (var s in fileInfos[0].GetType().GetProperties())
                { dataTable.Columns.Add(s.Name); }
                dataTable.Columns.Add("md5sum");
            }
            
            foreach (FileInfo f in fileInfos)
            { dataRows.Add(f.ToDataRow(dataTable.NewRow())); }

            return dataRows.ToArray();
        }

        public static DataTable ToDataTable(this List<FileInfo> fileInfos, DataTable dataTable = null)
        { return ToDataTable(fileInfos.ToArray(), dataTable); }
        public static DataTable ToDataTable(this FileInfo[] fileInfos, DataTable dataTable = null)
        {
            if (dataTable is null)
            {
                dataTable = new DataTable("Files");
                foreach (var s in fileInfos[0].GetType().GetProperties())
                { dataTable.Columns.Add(s.Name); }
                dataTable.Columns.Add("md5sum");
            }

            foreach (FileInfo f in fileInfos)
            { dataTable.Rows.Add(f.ToDataRow(dataTable.NewRow())); }
            return dataTable;
        }

    }
}
