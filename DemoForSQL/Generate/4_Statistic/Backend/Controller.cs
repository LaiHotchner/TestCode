using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class Controller
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent();
            var filePath = folderPath + $"{Backend_Statistic.ControllerName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var filedName = $"{Backend_Statistic.FieldPrefix}Service";

            stringBuilder.AppendLine("package " + Backend_Statistic.ControllerPackagePrefix + ";");
            stringBuilder.AppendLine();
            #region Impport
            stringBuilder.AppendLine("import com.alibaba.fastjson.JSON;");
            stringBuilder.AppendLine("import com.alibaba.fastjson.JSONObject;");
            stringBuilder.AppendLine("import com.infinite.common.base.BaseController;");
            stringBuilder.AppendLine("import com.infinite.common.base.ResponseBase;");
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass};");
            stringBuilder.AppendLine($"import {Backend_Statistic.ServiceInterfacePackagePrefix}.{Backend_Statistic.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestBody;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestMapping;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RequestMethod;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.RestController;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            #endregion
            stringBuilder.AppendLine("@RestController");
            stringBuilder.AppendLine("@RequestMapping(\"/device\")");
            stringBuilder.AppendLine($"public class {Backend_Statistic.ControllerName} extends BaseController " + "{");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Statistic.ServicesName} {filedName};");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_Statistic.GetAllStatisticResult_MethodName}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_Statistic.GetAllStatisticResult_MethodName}(@RequestBody String params) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine("            JSONObject parameter = JSON.parseObject(params);");
            stringBuilder.AppendLine("            int adminCode = parameter.getInteger(\"adminCode\");");
            stringBuilder.AppendLine($"            List<{Entity.StatisticInfoClass}> result = {filedName}.{Backend_Statistic.GetAllStatisticResult_MethodName}(adminCode);");
            stringBuilder.AppendLine("            return setResultSuccess(result);");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
