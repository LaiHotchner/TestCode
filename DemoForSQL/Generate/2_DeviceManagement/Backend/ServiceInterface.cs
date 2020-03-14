using System.Text;

namespace CodeSqlGenerate.Generate.JavaCode.Curd
{
    internal class ServiceInterface
    {
        internal static string GetContent(HotchnerTable table)
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
