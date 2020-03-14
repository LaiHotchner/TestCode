using CodeSqlGenerate.Generate;
using CodeSqlGenerate.Generate._3_Retrieval;
using CodeSqlGenerate.Generate._5_AnchorGroup;
using CodeSqlGenerate.Generate._5_Anchors;
using CodeSqlGenerate.Generate.Angular;
using CodeSqlGenerate.Generate.Curd;
using CodeSqlGenerate.Generate.JavaCode;
using CodeSqlGenerate.Generate.JavaCode._4_Statistic;
using CodeSqlGenerate.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeSqlGenerate
{
    public class Program
    {
        public static readonly string DataSourcePath = @"C:\0_Infinite\ICTS\0_Data_Source\InputFile";
        public static bool IsOutputToProject = true;

        // 设备管理详情页面中，显示的字段数量
        // 检索结果详情页面中，显示的字段数量
        public static readonly int DetailViewColumnCount = 11;

        static void Main(string[] args)
        {
            var allTableList = new List<HotchnerTable>();

            DirectoryInfo folder = new DirectoryInfo(DataSourcePath);

            foreach (FileInfo file in folder.GetFiles("*.csv"))
            {
                var tableList = OpenCsv.OpenCSV(file.FullName);
                allTableList.AddRange(tableList);
            }

            Backend_DeviceManagement.GenerateCode(allTableList);
            Frontend_DeviceManagement.GenerateCode(allTableList);

            Backend_Retrieval.GenerateCode(allTableList);
            Frontend_Retrieval.GenerateCode(allTableList);

            Backend_Anchors.GenerateCode(allTableList);
            Frontend_Anchors.GenerateCode(allTableList);

            Backend_Statistic.GenerateCode(allTableList);

            Frontend_Api_Url.GenerateCode(allTableList);
            ImportTemplate.GenerateTemplate(allTableList);
            SqlScript.GenerateSqlScript("Device", allTableList);

            Console.ReadLine();
        }
    }
}
