using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._2_DeviceManagement;
using CodeSqlGenerate.Generate._3_Retrieval;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval.Backend
{
    internal class Controller
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Retrieval.ControllerName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var filedName = $"{Backend_Retrieval.FieldPrefix}Service";

            stringBuilder.AppendLine("package " + Backend_Retrieval.ControllerPackagePrefix + ";");
            stringBuilder.AppendLine();
            #region Impport
            stringBuilder.AppendLine("import com.alibaba.fastjson.JSON;");
            stringBuilder.AppendLine("import com.alibaba.fastjson.JSONObject;");
            stringBuilder.AppendLine("import com.infinite.common.base.BaseController;");
            stringBuilder.AppendLine("import com.infinite.common.base.ResponseBase;");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalParameterClass};");
            stringBuilder.AppendLine($"import {Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalResultClass};");
            stringBuilder.AppendLine($"import {Backend_Devices.EntityPackagePrefix}.*;");
            stringBuilder.AppendLine($"import {Backend_Retrieval.ServiceInterfacePackagePrefix}.{Backend_Retrieval.ServicesName};");
            stringBuilder.AppendLine($"import {Backend_Devices.DeviceServiceInterfacePackagePrefix}.*;");
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
            stringBuilder.AppendLine($"public class {Backend_Retrieval.ControllerName} extends BaseController " + "{");
            stringBuilder.AppendLine();
            #region field
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Retrieval.ServicesName} {filedName};");
            stringBuilder.AppendLine();

            foreach (var table in tableList)
            {
                var deviceServiceFieldName = GetDeviceServiceFieldName(table);
                var deviceServiceInterfaceClassName = Backend_Devices.GetServiceInterfaceClassName(table);
                stringBuilder.AppendLine("    @Autowired");
                stringBuilder.AppendLine("    private " + deviceServiceInterfaceClassName + " " + deviceServiceFieldName + ";");
            }
            stringBuilder.AppendLine();
            #endregion
            #region GetListMethodName
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_Retrieval.GetRetrievalAllMethodName}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_Retrieval.GetRetrievalAllMethodName}(@RequestBody String params) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine("            String keyword = JSON.parseObject(params).getString(\"keyword\");");
#warning 这里应该直接解析参数对象
            stringBuilder.AppendLine($"            {Entity.RetrievalParameterClass} parameter = new {Entity.RetrievalParameterClass}();");
            stringBuilder.AppendLine("            parameter.setKeyword(keyword);");
            stringBuilder.AppendLine($"            List<{Entity.RetrievalResultClass}> infoList = {filedName}.{Backend_Retrieval.GetRetrievalAllMethodName}(parameter);");
            stringBuilder.AppendLine("            if (infoList == null || infoList.size() <= 0) {");
            stringBuilder.AppendLine("                return setResultError(\"查询无数据\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(infoList);");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            #endregion
            #region GetDetail
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_Retrieval.GetRetrievalDetailMethodName}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_Retrieval.GetRetrievalDetailMethodName}(@RequestBody String params) " + "{");
            stringBuilder.AppendLine("        try {");
#warning 这里应该直接解析参数对象
            stringBuilder.AppendLine("            JSONObject parameter = JSON.parseObject(params);");
            stringBuilder.AppendLine("            int id = parameter.getIntValue(\"id\");");
            stringBuilder.AppendLine("            String deviceType = parameter.getString(\"deviceType\");");

            var index = 0;
            foreach (var table in tableList)
            {
                var deviceType = table.PascalMethodName;
                var deviceEntityName = Backend_Devices.GetEntityName(table);
                var deviceServiceFieldName = GetDeviceServiceFieldName(table);
                var deviceGetByIdMethodName = Backend_Devices.GetByIdMethodName(table); ;
                if (index == 0)
                {
                    stringBuilder.AppendLine("            if (deviceType.equals(\"" + deviceType + "\")) {");
                }
                else
                {
                    stringBuilder.AppendLine("            } else if (deviceType.equals(\"" + deviceType + "\")) {");
                }
                stringBuilder.AppendLine($"                {deviceEntityName} info = {deviceServiceFieldName}.{deviceGetByIdMethodName}(id);");
                stringBuilder.AppendLine("                return setResultSuccess(info);");
                index = 1;
            }
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(\"无匹配数据\");");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            #endregion
            return stringBuilder.ToString();
        }

        private static string GetDeviceServiceFieldName(HotchnerTable table)
        {
            return table.camelcaseMethodName + "Service";
        }
    }
}
