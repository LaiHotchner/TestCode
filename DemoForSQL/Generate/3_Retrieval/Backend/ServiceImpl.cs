using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval.Backend
{
    internal class ServiceImpl
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Retrieval.ServicesImplName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var daoField = $"{Backend_Retrieval.FieldPrefix}Dao";

            stringBuilder.AppendLine("package " + Backend_Retrieval.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Retrieval.DaoPackagePrefix}.{Backend_Retrieval.DaoName};");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalParameterClass};");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalResultClass};");
            stringBuilder.AppendLine($"import {Backend_Retrieval.ServiceInterfacePackagePrefix}.{Backend_Retrieval.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Service;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.ArrayList;");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Service");
            stringBuilder.AppendLine($"public class {Backend_Retrieval.ServicesImplName} implements {Backend_Retrieval.ServicesName} " + "{");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Retrieval.DaoName} {daoField};");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public List<{Entity.RetrievalResultClass}> {Backend_Retrieval.GetRetrievalAllMethodName}({Entity.RetrievalParameterClass} parameter) " + "{");
            stringBuilder.AppendLine($"        List<{Entity.RetrievalResultClass}> result = new ArrayList<>();");
            stringBuilder.AppendLine("        String keyword = parameter.getKeyword();");

            foreach (var table in tableList)
            {
                stringBuilder.AppendLine($"        result.addAll({daoField}.{Backend_Retrieval.GetListMethodName_EachDao_GetRetrievalResult(table)}(keyword));");
            }
            stringBuilder.AppendLine("        return result;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
