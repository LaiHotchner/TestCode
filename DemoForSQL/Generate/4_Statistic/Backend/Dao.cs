using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class Dao
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{GetDaoClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        internal static string GetContent(HotchnerTable table)
        {
            var daoClassName = GetDaoClassName(table);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("package " + Backend_Statistic.DaoPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass};");
            stringBuilder.AppendLine("import org.apache.ibatis.annotations.Param;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Repository;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@Repository");
            stringBuilder.AppendLine("public interface " + daoClassName + " {");
            stringBuilder.AppendLine($"    {Entity.StatisticInfoClass} {GetEachStatisticInfo_MethodName(table)}(@Param(\"keyword\") String keyword);");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }


        public static string GetDaoClassName(HotchnerTable table)
        {
            return table.PascalMethodName + "StatisticDao";
        }

        public static string GetEachStatisticInfo_MethodName(HotchnerTable table)
        {
            return $"get{table.PascalMethodName}StatisticInfo";
        }
    }
}
