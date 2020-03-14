﻿using CodeSqlGenerate.Data;
using System.Text;

namespace CodeSqlGenerate.Generate._2_DeviceManagement.Backend
{
    public class Controller
    {
        internal static string GetContent(HotchnerTable table)
        {
            var serviceFieldName = table.camelcaseMethodName + "Service";
            var entityName = Backend_DeviceManagement.GetEntityName(table);
            var controllerClassName = Backend_DeviceManagement.GetControllerClassName(table);
            var serviceInterfaceClassName = Backend_DeviceManagement.GetServiceInterfaceClassName(table);

            StringBuilder stringBuilder = new StringBuilder();
            #region packages
            stringBuilder.AppendLine("package " + Backend_DeviceManagement.ControllerPackagePrefix + ";");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import com.alibaba.fastjson.JSON;");
            stringBuilder.AppendLine("import com.infinite.common.base.BaseController;");
            stringBuilder.AppendLine("import com.infinite.common.base.ResponseBase;");
            stringBuilder.AppendLine($"import {Backend_Code.GetPagingParameterEntity()};");
            stringBuilder.AppendLine("import com.infinite.common.utils.CsvParse;");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.EntityPackagePrefix}.{entityName};");
            stringBuilder.AppendLine("import " + Backend_DeviceManagement.DeviceServiceInterfacePackagePrefix + "." + serviceInterfaceClassName + ";");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.web.bind.annotation.*;");
            stringBuilder.AppendLine("import org.springframework.web.multipart.MultipartFile;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("import java.io.IOException;");
            stringBuilder.AppendLine("import java.text.ParseException;");
            stringBuilder.AppendLine("import java.util.ArrayList;");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("//    " + table.PascalMethodName + "：" + table.TableDescription);
            stringBuilder.AppendLine("@RestController");
            stringBuilder.AppendLine("@RequestMapping(\"/device\")");
            stringBuilder.AppendLine("public class " + controllerClassName + " extends BaseController {");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine("    private " + serviceInterfaceClassName + " " + serviceFieldName + ";");
            stringBuilder.AppendLine();
            #endregion
            #region Create
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetCreateMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetCreateMethodName(table)}(@RequestBody String str) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine($"            {entityName} info = JSON.parseObject(str, {entityName}.class);");
            stringBuilder.AppendLine($"            List<{entityName}> infoList = new ArrayList<>();");
            stringBuilder.AppendLine("            infoList.add(info);");
            stringBuilder.AppendLine($"            int result = {serviceFieldName}.{Backend_DeviceManagement.GetCreateMethodName(table)}(infoList);");
            stringBuilder.AppendLine("            if (result == 0) {");
            stringBuilder.AppendLine("                return setResultError(\"添加失败\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(\"添加成功\");");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine();
            #endregion
            #region import
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetImportMethodName(table)}\", consumes = \"multipart/form-data\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetImportMethodName(table)}(@RequestParam(value = \"file\") MultipartFile file) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine($"            List<{entityName}> infoList = CsvParse.parseToObjectList(file.getInputStream(), {entityName}.class);");
            stringBuilder.AppendLine("            if (infoList.size() == 0) {");
            stringBuilder.AppendLine("                setResultError(\"添加失败，导入文件数据为空\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(infoList);");
            stringBuilder.AppendLine("        } catch (IOException | ParseException | IllegalAccessException | InstantiationException e) {");
            stringBuilder.AppendLine("            e.printStackTrace();");
            stringBuilder.AppendLine("            setResultError(\"添加失败，导入文件数据不匹配，请确认导入数据服务模板要求。\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        return setResultSuccess(\"导入成功\");");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine();
            #endregion
            #region importConfirm
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetImportConfirmMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetImportConfirmMethodName(table)}(@RequestBody String str) " + "{");
            stringBuilder.AppendLine($"        List<{entityName}> infoList = JSON.parseArray(str, {entityName}.class);");
            stringBuilder.AppendLine($"        {serviceFieldName}.{Backend_DeviceManagement.GetCreateMethodName(table)}(infoList);");
            stringBuilder.AppendLine("        return setResultSuccess(\"导入确认成功\");");
            stringBuilder.AppendLine("    }");
            #endregion
            #region delete
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetDeleteByIdMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(@RequestBody String str) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine($"            long id = JSON.parseObject(str).getLongValue(\"id\");");
            stringBuilder.AppendLine($"            int result = {serviceFieldName}.{Backend_DeviceManagement.GetDeleteByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("            if (result == 0) {");
            stringBuilder.AppendLine("                return setResultError(\"删除失败\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(\"删除成功\");");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            #endregion
            #region update
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetUpdateMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetUpdateMethodName(table)}(@RequestBody String str) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine($"            {entityName} info = JSON.parseObject(str, {entityName}.class);");
            stringBuilder.AppendLine($"            int result = {serviceFieldName}.{Backend_DeviceManagement.GetUpdateMethodName(table)}(info);");
            stringBuilder.AppendLine("            if (result == 0) {");
            stringBuilder.AppendLine("                return setResultError(\"修改失败\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(\"修改成功\");");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            #endregion
            #region getById
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetByIdMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetByIdMethodName(table)}(@RequestBody String str) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine($"            long id = JSON.parseObject(str).getLongValue(\"id\");");
            stringBuilder.AppendLine($"            {entityName} info = {serviceFieldName}.{Backend_DeviceManagement.GetByIdMethodName(table)}(id);");
            stringBuilder.AppendLine("            if (info == null) {");
            stringBuilder.AppendLine("                return setResultError(\"查询无数据\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(info);");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            #endregion
            #region getAll
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    @RequestMapping(value = \"/{Backend_DeviceManagement.GetAllMethodName(table)}\", method = RequestMethod.POST)");
            stringBuilder.AppendLine($"    public ResponseBase {Backend_DeviceManagement.GetAllMethodName(table)}(@RequestBody String params) " + "{");
            stringBuilder.AppendLine("        try {");
            stringBuilder.AppendLine("            PagingParameter parameter = new PagingParameter();");
            stringBuilder.AppendLine($"            List<{entityName}> infoList = {serviceFieldName}.{Backend_DeviceManagement.GetAllMethodName(table)}(parameter);");
            stringBuilder.AppendLine("            if (infoList == null || infoList.size() <= 0) {");
            stringBuilder.AppendLine("                return setResultError(\"查询无数据\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return setResultSuccess(infoList);");
            stringBuilder.AppendLine("        } catch (Exception e) {");
            stringBuilder.AppendLine("            return setResultError(\"服务器错误\");");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            #endregion
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
