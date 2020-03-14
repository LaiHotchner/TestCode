using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public static class ServiceImpl
    {

        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Anchors.ServicesImplName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var daoField = $"{Backend_Anchors.FieldPrefix}Dao";

            stringBuilder.AppendLine("package " + Backend_Anchors.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();
#warning here need refactor
            stringBuilder.AppendLine("import com.infinite.common.constants.DeviceType;");
            stringBuilder.AppendLine($"import {Backend_Anchors.DaoPackagePrefix}.{Backend_Anchors.DaoName};");
            stringBuilder.AppendLine($"import {Backend_Anchors.EntityPackagePrefix}.{Entity.AnchorsClass};");
            stringBuilder.AppendLine($"import {Backend_Anchors.ServiceInterfacePackagePrefix}.{Backend_Anchors.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Service;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.ArrayList;");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Service");
            stringBuilder.AppendLine($"public class {Backend_Anchors.ServicesImplName} implements {Backend_Anchors.ServicesName} " + "{");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Anchors.DaoName} {daoField};");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public List<{Entity.AnchorsClass}> {Backend_Anchors.GetAnchorsByDeviceType_MethodName}(int type) " + "{");
            stringBuilder.AppendLine($"        List<{Entity.AnchorsClass}> result = new ArrayList<>();");
            stringBuilder.AppendLine("");
            foreach (var table in tableList)
            {
                stringBuilder.AppendLine($"        if ((type & DeviceType.{table.PascalMethodName}) == DeviceType.{table.PascalMethodName}) "+ "{");
                stringBuilder.AppendLine($"            {Entity.AnchorsClass} anchors = new {Entity.AnchorsClass}(\"{table.PascalMethodName}\");");
                stringBuilder.AppendLine($"            anchors.setPositions({daoField}.{Backend_Anchors.GetPositions_EachTable(table)}());");
                stringBuilder.AppendLine("            result.add(anchors);");
                stringBuilder.AppendLine("        }");
            }
            stringBuilder.AppendLine("        return result;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
