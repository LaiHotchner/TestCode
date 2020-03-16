using System.Collections.Generic;
using System.IO;
using CodeSqlGenerate.Data;
using System.Text;

namespace CodeSqlGenerate.Generate._2_DeviceManagement.Backend
{
    public class Dao
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_DeviceManagement.GetDaoClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var daoClassName = Backend_DeviceManagement.GetDaoClassName(table);
            var entityName = Backend_DeviceManagement.GetEntityName(table);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("package " + Backend_DeviceManagement.DaoPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine("import org.apache.ibatis.annotations.Param;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Repository;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@Repository");
            stringBuilder.AppendLine("public interface " + daoClassName + " {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetCreateMethodName(table)}(@Param(\"infoList\")List<{entityName}> infoList);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(@Param(\"id\") long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_DeviceManagement.GetUpdateMethodName(table)}(@Param(\"info\") {entityName} info);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    {entityName} {Backend_DeviceManagement.GetByIdMethodName(table)}(@Param(\"id\") long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    List<{entityName}> {Backend_DeviceManagement.GetAllMethodName(table)}(@Param(\"parameter\")PagingParameter parameter);");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }
}
