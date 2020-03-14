using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public static class ServiceInterface
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Anchors.ServicesName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Anchors.ServiceInterfacePackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"import {Backend_Anchors.EntityPackagePrefix}.{Entity.AnchorsClass};");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"public interface {Backend_Anchors.ServicesName} " + "{");
            stringBuilder.AppendLine($"    List<{Entity.AnchorsClass}> {Backend_Anchors.GetAnchorsByDeviceType_MethodName}(int deviceType);");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
