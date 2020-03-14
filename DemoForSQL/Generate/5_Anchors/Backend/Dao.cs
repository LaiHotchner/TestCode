using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public static class Dao
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Anchors.DaoName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Anchors.DaoPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Anchors.EntityPackagePrefix}.{Entity.PositionClass};");
            stringBuilder.AppendLine("import org.springframework.stereotype.Repository;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Repository");
            stringBuilder.AppendLine($"public interface {Backend_Anchors.DaoName} " + "{");

            foreach (var table in tableList)
            {
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine($"    List<{Entity.PositionClass}> {Backend_Anchors.GetPositions_EachTable(table)}();");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
