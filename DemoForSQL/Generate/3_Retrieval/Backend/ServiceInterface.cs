using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval.Backend
{
    internal class ServiceInterface
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Retrieval.ServicesName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Retrieval.ServiceInterfacePackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalParameterClass};");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalResultClass};");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"public interface {Backend_Retrieval.ServicesName} " + "{");
            stringBuilder.AppendLine($"    List<{Entity.RetrievalResultClass}> {Backend_Retrieval.GetRetrievalAllMethodName}({Entity.RetrievalParameterClass} parameter);");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
