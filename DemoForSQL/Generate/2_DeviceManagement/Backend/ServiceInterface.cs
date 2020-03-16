using System.Collections.Generic;
using System.IO;
using CodeSqlGenerate.Data;
using System.Text;

namespace CodeSqlGenerate.Generate._2_DeviceManagement.Backend
{
    internal class ServiceInterface
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_DeviceManagement.GetServiceInterfaceClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var entityName = Backend_DeviceManagement.GetEntityName(table);
            var serviceClassName = Backend_DeviceManagement.GetServiceInterfaceClassName(table);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("package " + Backend_DeviceManagement.DeviceServiceInterfacePackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("public interface " + serviceClassName + " {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetCreateMethodName(table)}(List<{entityName}> infoList);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetUpdateMethodName(table)}({entityName} info);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    {entityName} {Backend_DeviceManagement.GetByIdMethodName(table)}(long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    List<{entityName}> {Backend_DeviceManagement.GetAllMethodName(table)}(PagingParameter parameter);");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }
}
