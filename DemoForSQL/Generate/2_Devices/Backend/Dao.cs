using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeSqlGenerate.Data;

namespace CodeSqlGenerate.Generate._2_Devices.Backend
{
    public class Dao
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_Devices.GetDaoClassName(table)}.java";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var daoClassName = Backend_Devices.GetDaoClassName(table);
            var entityName = Backend_Devices.GetEntityName(table);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("package " + Backend_Devices.DaoPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine($"import {Backend_Devices.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine("import org.apache.ibatis.annotations.Param;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Repository;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@Repository");
            stringBuilder.AppendLine("public interface " + daoClassName + " {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetCreateMethodName(table)}(@Param(\"infoList\")List<{entityName}> infoList);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetDeleteByIdMethodName(table)}(@Param(\"id\") long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    int {Backend_Devices.GetUpdateMethodName(table)}(@Param(\"info\") {entityName} info);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    {entityName} {Backend_Devices.GetByIdMethodName(table)}(@Param(\"id\") long id);");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    List<{entityName}> {Backend_Devices.GetAllMethodName(table)}(@Param(\"parameter\")PagingParameter parameter);");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }
}
