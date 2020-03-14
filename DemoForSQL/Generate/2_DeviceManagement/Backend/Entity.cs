using CodeSqlGenerate.Utility;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate.JavaCode.Curd
{
    internal class Entity
    {
        public static Dictionary<string, string> RowTypeJavaDict = new Dictionary<string, string>();

        static Entity()
        {
            RowTypeJavaDict.Add("bigint", "long");

            RowTypeJavaDict.Add("BLOB", "String");          // 修改为string，表示文件路径
            RowTypeJavaDict.Add("CHAR", "boolean");
            RowTypeJavaDict.Add("CHAR(1)", "boolean");
            RowTypeJavaDict.Add("CHAR(12)", "String");
            RowTypeJavaDict.Add("CHAR(2)", "String");
            RowTypeJavaDict.Add("CHAR(4)", "String");
            RowTypeJavaDict.Add("DATE", "Date");
            RowTypeJavaDict.Add("INTEGER", "int");
            RowTypeJavaDict.Add("NUMBER", "double");
            RowTypeJavaDict.Add("NUMBER(10;2)", "double");
            RowTypeJavaDict.Add("NUMBER(11;8)", "double");
            RowTypeJavaDict.Add("NUMBER(12;2)", "double");
            RowTypeJavaDict.Add("NUMBER(12;3)", "double");
            RowTypeJavaDict.Add("NUMBER(12;8)", "double");
            RowTypeJavaDict.Add("NUMBER(2)", "double");
            RowTypeJavaDict.Add("NUMBER(3)", "double");
            RowTypeJavaDict.Add("NUMBER(4)", "double");
            RowTypeJavaDict.Add("NUMBER(5;2)", "double");
            RowTypeJavaDict.Add("NUMBER(6)", "double");
            RowTypeJavaDict.Add("NUMBER(6;2)", "double");
            RowTypeJavaDict.Add("NUMBER(7;3)", "double");
            RowTypeJavaDict.Add("NUMBER(8;2)", "double");
            RowTypeJavaDict.Add("NUMBER(9;3)", "double");
            RowTypeJavaDict.Add("RAW", "long");                       // 用作ID，统一使用integer
            RowTypeJavaDict.Add("RAW(16)", "long");                   // 用作ID，统一使用integer
            RowTypeJavaDict.Add("TIMESTAMP(6)", "Date");
            RowTypeJavaDict.Add("VARCHAR2", "String");
            RowTypeJavaDict.Add("VARCHAR2(1)", "String");
            RowTypeJavaDict.Add("VARCHAR2(10)", "String");
            RowTypeJavaDict.Add("VARCHAR2(100)", "String");
            RowTypeJavaDict.Add("VARCHAR2(1000)", "String");
            RowTypeJavaDict.Add("VARCHAR2(12)", "String");
            RowTypeJavaDict.Add("VARCHAR2(14)", "String");
            RowTypeJavaDict.Add("VARCHAR2(140)", "String");
            RowTypeJavaDict.Add("VARCHAR2(15)", "String");
            RowTypeJavaDict.Add("VARCHAR2(1500)", "String");
            RowTypeJavaDict.Add("VARCHAR2(160)", "String");
            RowTypeJavaDict.Add("VARCHAR2(18)", "String");
            RowTypeJavaDict.Add("VARCHAR2(2)", "String");
            RowTypeJavaDict.Add("VARCHAR2(20)", "String");
            RowTypeJavaDict.Add("VARCHAR2(200)", "String");
            RowTypeJavaDict.Add("VARCHAR2(2000)", "String");
            RowTypeJavaDict.Add("VARCHAR2(25)", "String");
            RowTypeJavaDict.Add("VARCHAR2(30)", "String");
            RowTypeJavaDict.Add("VARCHAR2(300)", "String");
            RowTypeJavaDict.Add("VARCHAR2(4)", "String");
            RowTypeJavaDict.Add("VARCHAR2(40)", "String");
            RowTypeJavaDict.Add("VARCHAR2(400)", "String");
            RowTypeJavaDict.Add("VARCHAR2(4000)", "String");
            RowTypeJavaDict.Add("VARCHAR2(50)", "String");
            RowTypeJavaDict.Add("VARCHAR2(500)", "String");
            RowTypeJavaDict.Add("VARCHAR2(6)", "String");
            RowTypeJavaDict.Add("VARCHAR2(60)", "String");
            RowTypeJavaDict.Add("VARCHAR2(600)", "String");
            RowTypeJavaDict.Add("VARCHAR2(70)", "String");
            RowTypeJavaDict.Add("VARCHAR2(8)", "String");
            RowTypeJavaDict.Add("VARCHAR2(80)", "String");
        }

        internal static void GenerateAllEntity(List<HotchnerTable> tableList, string entityFolderPath)
        {
            foreach (var table in tableList)
            {
                var entityName = Backend_DeviceManagement.GetEntityName(table);
                StringBuilder entityBuilder = new StringBuilder();
                entityBuilder.AppendLine("package "+ Backend_DeviceManagement.EntityPackagePrefix + ";");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine("import java.sql.Timestamp;");
                entityBuilder.AppendLine("import java.util.Date;");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine($"public class {entityName} " + "{");

                foreach (var row in table.RowList)
                {
                    entityBuilder.AppendLine($"    // {row.Description}");
                    entityBuilder.AppendLine($"    private {RowTypeJavaDict[row.RowType]} {row.Name.ToLower()};");
                }
                entityBuilder.AppendLine("");

                foreach (var row in table.RowList)
                {
                    string javeType = RowTypeJavaDict[row.RowType];
                    string firstUpOtherLow = CommonMethod.GetFirstUpAndOtherLowString(row.Name);
                    string nameLower = row.Name.ToLower();

                    entityBuilder.AppendLine($"    public {javeType} get{firstUpOtherLow}() " + "{");
                    entityBuilder.AppendLine($"        return {row.Name.ToLower()};");
                    entityBuilder.AppendLine("    }");
                    entityBuilder.AppendLine("");
                    entityBuilder.AppendLine($"    public void set{firstUpOtherLow}({javeType} {nameLower}) " + "{");
                    entityBuilder.AppendLine($"        this.{nameLower} = {nameLower};");
                    entityBuilder.AppendLine("    }");
                    entityBuilder.AppendLine("");
                }

                entityBuilder.AppendLine("}");

                var content = entityBuilder.ToString();
                var filePath = entityFolderPath + $"\\{entityName}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }
    }
}
