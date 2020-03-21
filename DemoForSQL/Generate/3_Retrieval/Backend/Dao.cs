using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval.Backend
{
    internal class Dao
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Retrieval.DaoName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Retrieval.DaoPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import com.infinite.icts.entity.retrieval.RetrievalParameter;");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalResultClass};");
            stringBuilder.AppendLine("import org.springframework.stereotype.Repository;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Repository");
            stringBuilder.AppendLine($"public interface {Backend_Retrieval.DaoName} " + "{");

            foreach (var table in tableList)
            {
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine($"    List<{Entity.RetrievalResultClass}> " +
                                         $"{Backend_Retrieval.GetListMethodName_EachDao_GetRetrievalResult(table)}" +
                                         $"(RetrievalParameter retrievalPara);");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
