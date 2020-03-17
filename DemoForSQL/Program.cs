using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._0_Sql;
using CodeSqlGenerate.Generate._1_Template;
using CodeSqlGenerate.Generate._2_Devices;
using CodeSqlGenerate.Generate._3_Retrieval;
using CodeSqlGenerate.Generate._4_Statistic;
using CodeSqlGenerate.Generate._5_Anchors;
using CodeSqlGenerate.Generate.Commond;
using System.Collections.Generic;
using System.IO;
using CodeSqlGenerate.Utility;

namespace CodeSqlGenerate
{
    public class Program
    {
        public static bool IsOutputToProject = true;
        public static readonly string SourcePath = @"C:\0_Infinite\ICTS\0_Input\";
        public static readonly string OutputPath = @"C:\0_Infinite\ICTS\2_Output\";

        // 设备管理详情页面中和检索结果详情页面中，显示的字段数量
        public static readonly int DetailViewColumnCount = 11;

        static void Main(string[] args)
        {
            PreproccessFolder();

            var allTableList = ReadAllTables();

            SqlScript.GenerateSqlScript("CreateDeviceSql", allTableList);
            ImportTemplate.GenerateTemplate(allTableList);

            Backend_Devices.GenerateCode(allTableList);
            Frontend_Devices.GenerateCode(allTableList);

            Backend_Retrieval.GenerateCode(allTableList);
            Frontend_Retrieval.GenerateCode(allTableList);

            Backend_Anchors.GenerateCode(allTableList);
            Frontend_Anchors.GenerateCode(allTableList);

            Backend_Statistic.GenerateCode(allTableList);
            Frontend_Statistic.GenerateCode(allTableList);

            Frontend_Api_Url.GenerateCode(allTableList);
        }

        private static List<HotchnerTable> ReadAllTables()
        {
            var allTableList = new List<HotchnerTable>();
            var folder = new DirectoryInfo(SourcePath);
            foreach (FileInfo file in folder.GetFiles("*.csv"))
            {
                var tableList = OpenCsv.OpenCSV(file.FullName);
                allTableList.AddRange(tableList);
            }
            return allTableList;
        }

        private static void PreproccessFolder()
        {
            CommonMethod.CreateDirectoryIfNotExist(OutputPath);
            CommonMethod.ClearFolderIfExistFiles(OutputPath);
        }
    }
}
