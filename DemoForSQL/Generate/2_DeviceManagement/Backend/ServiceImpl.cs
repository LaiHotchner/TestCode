using System.Collections.Generic;
using System.IO;
using CodeSqlGenerate.Data;
using System.Text;

namespace CodeSqlGenerate.Generate._2_DeviceManagement.Backend
{
    internal class ServiceImpl
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_DeviceManagement.GetServiceImplClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var daoFieldName = table.camelcaseMethodName + "Dao";
            var entityName = Backend_DeviceManagement.GetEntityName(table);
            var daoClassName = Backend_DeviceManagement.GetDaoClassName(table);
            var serviceImplClassName = Backend_DeviceManagement.GetServiceImplClassName(table);
            var serviceInterfaceClassName = Backend_DeviceManagement.GetServiceInterfaceClassName(table);

            StringBuilder stringBuilder = new StringBuilder();
            #region import
            stringBuilder.AppendLine("package " + Backend_DeviceManagement.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.DaoPackagePrefix}.{daoClassName};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.DeviceServiceInterfacePackagePrefix}.{serviceInterfaceClassName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Service;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@Service");
            stringBuilder.AppendLine("public class " + serviceImplClassName + " implements " + serviceInterfaceClassName + " {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {daoClassName} {daoFieldName};");
            stringBuilder.AppendLine();
            #endregion
            #region Implement
            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public int {Backend_DeviceManagement.GetCreateMethodName(table)}(List<{entityName}> infoList) " + "{");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_DeviceManagement.GetCreateMethodName(table)}(infoList);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public int {Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(long id)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public int {Backend_DeviceManagement.GetUpdateMethodName(table)}({entityName} info)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_DeviceManagement.GetUpdateMethodName(table)}(info);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public {entityName} {Backend_DeviceManagement.GetByIdMethodName(table)}(long id)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_DeviceManagement.GetByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public List<{entityName}> {Backend_DeviceManagement.GetAllMethodName(table)}(PagingParameter parameter)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_DeviceManagement.GetAllMethodName(table)}(parameter);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            #endregion
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
