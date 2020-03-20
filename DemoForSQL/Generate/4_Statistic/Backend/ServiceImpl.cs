using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class ServiceImpl
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Statistic.ServicesImplName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            var daoField = $"{Backend_Statistic.FieldPrefix}Dao";
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("package " + Backend_Statistic.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("import com.infinite.common.entity.AdminCode;");
            stringBuilder.AppendLine($"import {Backend_Statistic.DaoPackagePrefix}.*;");
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass};");
            stringBuilder.AppendLine($"import {Backend_Statistic.ServiceInterfacePackagePrefix}.{Backend_Statistic.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Service;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.ArrayList;");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Service");
            stringBuilder.AppendLine($"public class {Backend_Statistic.ServicesImplName} implements {Backend_Statistic.ServicesName} " + "{");

            stringBuilder.AppendLine("    @Autowired");
            stringBuilder.AppendLine($"    private {Backend_Statistic.DaoName} {daoField};");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public List<{Entity.StatisticInfoClass}> {Backend_Statistic.GetAllStatisticResult_MethodName}(int adminCode) " + "{");
            stringBuilder.AppendLine($"        List<{Entity.StatisticInfoClass}> result = new ArrayList<>();");
            stringBuilder.AppendLine($"        AdminCode code = new AdminCode();");
            stringBuilder.AppendLine($"        code.setAdminCode(adminCode);");
            stringBuilder.AppendLine($"        String queryStr = code.GetQueryStr();");
            stringBuilder.AppendLine($"        result = {daoField}.{Backend_Statistic.GetAllStatisticResult_MethodName}(queryStr);");

            stringBuilder.AppendLine("        return result;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private static string GetDaoFieldName(HotchnerTable table)
        {
            return $"{table.camelcaseMethodName}Dao";
        }



        // 暂时不需要各个设备的统计信息Dao
        //foreach (var table in tableList)
        //{
        //    stringBuilder.AppendLine("    @Autowired");
        //    stringBuilder.AppendLine($"    private {Dao.GetDaoClassName(table)} {GetDaoFieldName(table)};");
        //}
        //foreach (var table in tableList)
        //{
        //    stringBuilder.AppendLine($"        result.add({GetDaoFieldName(table)}.{Dao.GetEachStatisticInfo_MethodName(table)}(queryStr));");
        //}
    }
}
