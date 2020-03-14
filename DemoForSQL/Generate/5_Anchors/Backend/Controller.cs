using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeSqlGenerate.Generate._5_AnchorGroup;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public static class Controller
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Anchors.ControllerName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var filedName = $"{Backend_Anchors.FieldPrefix}Service";

            stringBuilder.AppendLine("package " + Backend_Anchors.ControllerPackagePrefix + ";");
            stringBuilder.AppendLine();
            #region Impport
            stringBuilder.AppendLine("import com.infinite.common.base.BaseController;");
            stringBuilder.AppendLine("import com.infinite.common.base.ResponseBase;");
            stringBuilder.AppendLine($"import {Backend_Anchors.EntityPackagePrefix}.{Entity.AnchorsClass};");
            stringBuilder.AppendLine($"import {Backend_Anchors.ServiceInterfacePackagePrefix}.{Backend_Anchors.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestBody;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestMapping;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestMethod;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RestController;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            #endregion
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@RestController");
            stringBuilder.AppendLine("@RequestMapping(\"/device\")");
            stringBuilder.AppendLine($"public class {Backend_Anchors.ControllerName} extends BaseController " + "{");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Anchors.ServicesName} {filedName};");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_Anchors.GetAnchorsByDeviceType_MethodName}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_Anchors.GetAnchorsByDeviceType_MethodName}(@RequestBody String params) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine("            int deviceType = Integer.parseInt(params);");
            stringBuilder.AppendLine($"            List<{Entity.AnchorsClass}> infoList = {filedName}.{Backend_Anchors.GetAnchorsByDeviceType_MethodName}(deviceType);");
            stringBuilder.AppendLine("            if (infoList == null || infoList.size() <= 0) {");
            stringBuilder.AppendLine("                return setResultError(\"查询无数据\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(infoList);");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
