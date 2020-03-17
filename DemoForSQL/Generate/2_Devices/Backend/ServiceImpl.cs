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
                var filePath = folderPath + $"{Backend_Devices.GetServiceImplClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var daoFieldName = table.camelcaseMethodName + "Dao";
            var entityName = Backend_Devices.GetEntityName(table);
            var daoClassName = Backend_Devices.GetDaoClassName(table);
            var serviceImplClassName = Backend_Devices.GetServiceImplClassName(table);
            var serviceInterfaceClassName = Backend_Devices.GetServiceInterfaceClassName(table);

            StringBuilder stringBuilder = new StringBuilder();
            #region import
            stringBuilder.AppendLine("package " + Backend_Devices.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_Devices.DaoPackagePrefix}.{daoClassName};");
            stringBuilder.AppendLine($"import {Backend_Devices.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine($"import {Backend_Devices.DeviceServiceInterfacePackagePrefix}.{serviceInterfaceClassName};");
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
            stringBuilder.AppendLine($"    public int {Backend_Devices.GetCreateMethodName(table)}(List<{entityName}> infoList) " + "{");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_Devices.GetCreateMethodName(table)}(infoList);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public int {Backend_Devices.GetDeleteByIdMethodName(table)}(long id)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_Devices.GetDeleteByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public int {Backend_Devices.GetUpdateMethodName(table)}({entityName} info)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_Devices.GetUpdateMethodName(table)}(info);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public {entityName} {Backend_Devices.GetByIdMethodName(table)}(long id)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_Devices.GetByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public List<{entityName}> {Backend_Devices.GetAllMethodName(table)}(PagingParameter parameter)" + " {");
            stringBuilder.AppendLine($"        return {daoFieldName}.{Backend_Devices.GetAllMethodName(table)}(parameter);");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("");

            #endregion
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
