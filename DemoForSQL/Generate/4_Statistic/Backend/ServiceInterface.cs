using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class ServiceInterface
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent();
            var filePath = folderPath + $"{Backend_Statistic.ServicesName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Statistic.ServiceInterfacePackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticSummaryClass};");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"public interface {Backend_Statistic.ServicesName} " + "{");
            stringBuilder.AppendLine($"    {Entity.StatisticSummaryClass} {Backend_Statistic.GetAllStatisticResult_MethodName}(int adminCode);");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

    }
}
