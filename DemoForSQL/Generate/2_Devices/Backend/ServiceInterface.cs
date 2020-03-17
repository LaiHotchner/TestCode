using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeSqlGenerate.Data;

namespace CodeSqlGenerate.Generate._2_Devices.Backend
{
    internal class ServiceInterface
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_Devices.GetServiceInterfaceClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var entityName = Backend_Devices.GetEntityName(table);
            var serviceClassName = Backend_Devices.GetServiceInterfaceClassName(table);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("package " + Backend_Devices.DeviceServiceInterfacePackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_Devices.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("public interface " + serviceClassName + " {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetCreateMethodName(table)}(List<{entityName}> infoList);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetDeleteByIdMethodName(table)}(long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetUpdateMethodName(table)}({entityName} info);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    {entityName} {Backend_Devices.GetByIdMethodName(table)}(long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    List<{entityName}> {Backend_Devices.GetAllMethodName(table)}(PagingParameter parameter);");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }
}
