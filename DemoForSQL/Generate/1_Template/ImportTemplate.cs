using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeSqlGenerate.Data;
using CodeSqlGenerate.Utility;

namespace CodeSqlGenerate.Generate._1_Template
{
    public static class ImportTemplate
    {
        private static readonly string DesktopOutputPath = @"C:\Users\Hotch\Desktop\Template\";
        private static readonly string PostmanOutputPath = @"C:\Users\Hotch\Postman\files\Upload\";

        // 元素数据类型和C#数据类型映射字典
        public static Dictionary<string, string> RowTypeCSharpDict = new Dictionary<string, string>();
        // 不同城市下派出所最大数量
        public static Dictionary<int, int> CityMaxDictionary = new Dictionary<int, int>();
        // 所在线
        public static Dictionary<int, string> SzxDictionary = new Dictionary<int, string>();
        // 所属铁路单位
        public static Dictionary<int, string> SstldwDictionary = new Dictionary<int, string>();

        static ImportTemplate()
        {
            #region RowTypeCSharpDict
            RowTypeCSharpDict.Add("bigint", "long");

            RowTypeCSharpDict.Add("BLOB", "String");                    // 修改为string，表示文件路径
            RowTypeCSharpDict.Add("CHAR", "Boolean");
            RowTypeCSharpDict.Add("CHAR(1)", "Boolean");
            RowTypeCSharpDict.Add("CHAR(12)", "String");
            RowTypeCSharpDict.Add("CHAR(2)", "String");
            RowTypeCSharpDict.Add("CHAR(4)", "String");
            RowTypeCSharpDict.Add("DATE", "DateTime");
            RowTypeCSharpDict.Add("INTEGER", "int");
            RowTypeCSharpDict.Add("NUMBER", "double");
            RowTypeCSharpDict.Add("NUMBER(10;2)", "double");
            RowTypeCSharpDict.Add("NUMBER(11;8)", "double");
            RowTypeCSharpDict.Add("NUMBER(12;2)", "double");
            RowTypeCSharpDict.Add("NUMBER(12;3)", "double");
            RowTypeCSharpDict.Add("NUMBER(12;8)", "double");
            RowTypeCSharpDict.Add("NUMBER(2)", "double");
            RowTypeCSharpDict.Add("NUMBER(3)", "double");
            RowTypeCSharpDict.Add("NUMBER(4)", "double");
            RowTypeCSharpDict.Add("NUMBER(5;2)", "double");
            RowTypeCSharpDict.Add("NUMBER(6)", "double");
            RowTypeCSharpDict.Add("NUMBER(6;2)", "double");
            RowTypeCSharpDict.Add("NUMBER(7;3)", "double");
            RowTypeCSharpDict.Add("NUMBER(8;2)", "double");
            RowTypeCSharpDict.Add("NUMBER(9;3)", "double");
            RowTypeCSharpDict.Add("RAW", "long");                       // 用作ID，统一使用integer
            RowTypeCSharpDict.Add("RAW(16)", "long");                   // 用作ID，统一使用integer
            RowTypeCSharpDict.Add("TIMESTAMP(6)", "DateTime");
            RowTypeCSharpDict.Add("VARCHAR2", "String");
            RowTypeCSharpDict.Add("VARCHAR2(1)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(10)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(100)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(1000)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(12)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(14)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(140)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(15)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(1500)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(160)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(18)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(2)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(20)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(200)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(2000)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(25)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(30)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(300)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(4)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(40)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(400)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(4000)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(50)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(500)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(6)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(60)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(600)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(70)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(8)", "String");
            RowTypeCSharpDict.Add("VARCHAR2(80)", "String");
            #endregion

            #region 所属派出所索引
            CityMaxDictionary.Add(1, 6);
            CityMaxDictionary.Add(2, 4);
            CityMaxDictionary.Add(3, 4);
            CityMaxDictionary.Add(4, 2);
            CityMaxDictionary.Add(5, 3);
            CityMaxDictionary.Add(6, 2);
            CityMaxDictionary.Add(7, 4);
            CityMaxDictionary.Add(8, 4);
            CityMaxDictionary.Add(9, 1);
            CityMaxDictionary.Add(10, 2);
            #endregion

            #region 所在线
            SzxDictionary.Add(0, "浙赣铁路");
            SzxDictionary.Add(1, "沪杭铁路");
            SzxDictionary.Add(2, "萧甬铁路");
            SzxDictionary.Add(3, "金千铁路");
            SzxDictionary.Add(4, "金温货线");
            SzxDictionary.Add(5, "杭牛铁路");
            SzxDictionary.Add(6, "宣杭铁路");
            SzxDictionary.Add(7, "新长铁路");
            SzxDictionary.Add(8, "甬台温铁路");
            SzxDictionary.Add(9, "温福铁路");
            SzxDictionary.Add(10, "宁杭客运专线");
            SzxDictionary.Add(11, "杭甬客运专线");
            SzxDictionary.Add(12, "沪杭城际高速");
            SzxDictionary.Add(13, "杭长客运专线");
            SzxDictionary.Add(14, "金温铁路");
            SzxDictionary.Add(15, "杭黄客运专线");
            SzxDictionary.Add(16, "九景衢铁路");
            SzxDictionary.Add(17, "衢宁铁路");
            #endregion

            #region 所属铁路单位
            SstldwDictionary.Add(0, "杭州铁路办事处");
            SstldwDictionary.Add(1, "南京铁路办事处");
            SstldwDictionary.Add(2, "合肥铁路办事处");
            SstldwDictionary.Add(3, "徐州铁路办事处");
            SstldwDictionary.Add(4, "杭州房建段");
            SstldwDictionary.Add(5, "杭州职培基地");
            SstldwDictionary.Add(6, "杭州疾控所");
            SstldwDictionary.Add(7, "杭州枢纽指挥部");
            SstldwDictionary.Add(8, "杭州电务段");
            SstldwDictionary.Add(9, "宁波工务段");
            SstldwDictionary.Add(10, "杭州客运段");
            SstldwDictionary.Add(11, "杭州北车辆段");
            SstldwDictionary.Add(12, "杭州供电段");
            SstldwDictionary.Add(13, "杭州机务段");
            SstldwDictionary.Add(14, "杭州货运中心");
            SstldwDictionary.Add(15, "金华货运中心");
            SstldwDictionary.Add(16, "杭州站");
            SstldwDictionary.Add(17, "宁波车务段");
            SstldwDictionary.Add(18, "金华车务段");
            SstldwDictionary.Add(19, "嘉兴车务段");
            #endregion
        }

        public static void GenerateTemplate(List<HotchnerTable> tableList)
        {
            CommonMethod.ClearFolderIfExistFiles(DesktopOutputPath);
            CommonMethod.ClearFolderIfExistFiles(PostmanOutputPath);

            foreach (var table in tableList)
            {
                var templateContent = GenerateTemplateContent(table);
                SaveTemplateToCsv(templateContent, table);
            }
        }

        private static string GenerateTemplateContent(HotchnerTable table)
        {
            StringBuilder templateBuilder = new StringBuilder();

            foreach (var row in table.RowList)
            {
                var rowName = row.Name.ToLower();
                if (rowName == "id") { continue; }

                templateBuilder.Append(rowName + "(" + row.Description + "),");
            }
            templateBuilder.Remove(templateBuilder.Length - 1, 1);
            templateBuilder.Append("\r\n");

            Random random = new Random();
            int generateCount = random.Next(50, 200);
            for (int i = 0; i < generateCount; i++)
            {
                var rowBuilder = new StringBuilder();
                foreach (var row in table.RowList)
                {
                    var rowName = row.Name.ToLower();
                    var rowType = RowTypeCSharpDict[row.RowType];

                    if (rowName == "id") { continue; }
                    if (GenerateMc(i, rowBuilder, row, table)) { continue; }
                    if (GenerateAdminCode(random, rowBuilder, row)) { continue; }
                    if (GenerateLongtitude(random, rowBuilder, row)) { continue; }
                    if (GenerateLatitude(random, rowBuilder, row)) { continue; }
                    if (GenerateSzx(random, rowBuilder, row)) { continue; }
                    if (GenerateSxx(random, rowBuilder, row)) { continue; }
                    if (GenerateZxlc(random, rowBuilder, row)) { continue; }
                    if (GenerateSstldw(random, rowBuilder, row)) { continue; }

                    if (rowType == "long")
                    {
                        rowBuilder.Append(random.Next(100, 100000) + ",");
                    }
                    else if (rowType == "int")
                    {
                        rowBuilder.Append(random.Next(100, 1000) + ",");
                    }
                    else if (rowType == "double")
                    {
                        rowBuilder.Append(random.NextDouble() * random.Next(10, 100) + ",");
                    }
                    else if (rowType == "Boolean")
                    {
                        rowBuilder.Append(random.Next(0, 2) % 2 == 0 ? "true," : "false,");
                    }
                    else if (rowType == "String")
                    {
                        rowBuilder.Append(row.Description + random.Next(10, 100) + ",");
                    }
                    else if (rowType == "DateTime")
                    {
                        rowBuilder.Append(DateTime.Now.AddDays(random.Next(0, 100)).AddHours(random.Next(1, 24)).ToString() + ",");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                rowBuilder.Remove(rowBuilder.Length - 1, 1);

                templateBuilder.AppendLine(rowBuilder.ToString());
            }

            var templateContent = templateBuilder.ToString();
            return templateContent;
        }
        private static void SaveTemplateToCsv(string templateContent, HotchnerTable table)
        {
            var templateFileName = CommonMethod.GetTemplateName(table);
            var filePath = $"{DesktopOutputPath}{templateFileName}";
            var postmanPath = $"{PostmanOutputPath}{templateFileName}";

            File.WriteAllText(filePath, templateContent, Encoding.UTF8);
            File.WriteAllText(postmanPath, templateContent, Encoding.UTF8);
        }

        private static bool GenerateAdminCode(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name == "admin_code")
            {
                var prefix = "101";
                var cityIndex = random.Next(1, 11);
                var cityStr = string.Format("{0:d3}", cityIndex);
                var localPoliceStationIndex = random.Next(1, CityMaxDictionary[cityIndex]);
                var localPoliceStationStr = string.Format("{0:d3}", localPoliceStationIndex);
                var adminCode = prefix + cityStr + localPoliceStationStr + ",";
                rowBuilder.Append(adminCode);
                return true;
            }
            return false;
        }
        private static bool GenerateLongtitude(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name.ToLower() == "x")
            {
                double longitude = 120.1 + random.Next(0, 10000) / 100000.0;
                rowBuilder.Append(longitude.ToString() + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateLatitude(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name.ToLower() == "y")
            {
                double latitude = 30.2 + random.Next(0, 10000) / 100000.0;
                rowBuilder.Append(latitude.ToString() + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateSzx(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name.ToLower() == "szx")
            {
                var szxName = SzxDictionary[random.Next(0, 18)];
                rowBuilder.Append(szxName + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateSxx(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            // 上下行
            if (row.Name.ToLower() == "sxx")
            {
                var sxxName = random.Next(0, 2) % 2 == 0 ? "上行" : "下行";
                rowBuilder.Append(sxxName + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateZxlc(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name.ToLower() == "zxlc")
            {
                //var zxlcName = "k" + random.Next(0, 500) + "+" + random.Next(1, 10) * 100;
                var zxlcName = random.Next(10, 50) * 10;
                rowBuilder.Append(zxlcName + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateMc(int index, StringBuilder rowBuilder, HotchnerRow row, HotchnerTable table)
        {
            if (row.Name.ToLower() == "mc")
            {
                var mc = table.Label + index;
                rowBuilder.Append(mc + ",");
                return true;
            }
            return false;
        }
        private static bool GenerateSstldw(Random random, StringBuilder rowBuilder, HotchnerRow row)
        {
            if (row.Name.ToLower() == "sstldw")
            {
                var sstldwName = SstldwDictionary[random.Next(0, 20)];
                rowBuilder.Append(sstldwName + ",");
                return true;
            }
            return false;
        }
    }
}