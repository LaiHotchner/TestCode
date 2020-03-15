using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._2_DeviceManagement;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class ServiceImpl
    {
        private static readonly Dictionary<string, string> AngularFolderDictionary = new Dictionary<string, string>();

        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var content = GetContent(tableList);
            var filePath = folderPath + $"{Backend_Statistic.ServicesImplName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        static ServiceImpl()
        {
            // 基本设施
            AngularFolderDictionary.Add("Bridge", "basic-equipment\\bridge\\");
            AngularFolderDictionary.Add("Culvert", "basic-equipment\\culvert\\");
            AngularFolderDictionary.Add("LevelCrossing", "basic-equipment\\level-crossing\\");
            AngularFolderDictionary.Add("Marker", "basic-equipment\\marker\\");
            AngularFolderDictionary.Add("PublicCrossingBridge", "basic-equipment\\public-crossing-bridge\\");
            AngularFolderDictionary.Add("Tunnel", "basic-equipment\\tunnel\\");

            // 其他设施
            AngularFolderDictionary.Add("OtherDevice", "other-equipment\\other-device\\");

            // 安防设施
            AngularFolderDictionary.Add("EvacuationRoute", "security\\evacuation-route\\");
            AngularFolderDictionary.Add("PowerComm", "security\\power-comm\\");
            AngularFolderDictionary.Add("ProtectiveFence", "security\\protective-fence\\");
            AngularFolderDictionary.Add("SecurityDevice", "security\\security-device\\");
            AngularFolderDictionary.Add("VideoSurveillance", "security\\video-surveillance\\");
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var serviceField = $"{Backend_Statistic.FieldPrefix}Dao";

            stringBuilder.AppendLine("package " + Backend_Statistic.ServiceImplPackagePrefix + ";");
            stringBuilder.AppendLine();
#warning DeviceType
            stringBuilder.AppendLine("import com.infinite.common.constants.DeviceType;");
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass};");
            stringBuilder.AppendLine($"import {Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticSummaryClass};");
            stringBuilder.AppendLine($"import {Backend_DeviceManagement.devicePackage}.*;");
            stringBuilder.AppendLine($"import {Backend_Statistic.ServiceInterfacePackagePrefix}.{Backend_Statistic.ServicesName};");
            stringBuilder.AppendLine("import org.springframework.beans.factory.annotation.Autowired;");
            stringBuilder.AppendLine("import org.springframework.stereotype.Service;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("import java.util.ArrayList;");
            stringBuilder.AppendLine("import java.util.List;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("@Service");
            stringBuilder.AppendLine($"public class {Backend_Statistic.ServicesImplName} implements {Backend_Statistic.ServicesName} " + "{");
            stringBuilder.AppendLine("");

            foreach (var table in tableList)
            {
                stringBuilder.AppendLine("    @Autowired");
                stringBuilder.AppendLine($"    private {Backend_DeviceManagement.GetServiceInterfaceClassName(table)} {GetServiceFieldName(table)};");
            }
            stringBuilder.AppendLine("    @Override");
            stringBuilder.AppendLine($"    public {Entity.StatisticSummaryClass}> {Backend_Statistic.GetAllStatisticResult_MethodName}(int adminCode) " + "{");
            stringBuilder.AppendLine($"        List<{Entity.StatisticInfoClass}> result = new ArrayList<>();");

            stringBuilder.AppendLine("        return result;");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
        private static string GetServiceFieldName(HotchnerTable table)
        {
            return $"{table.camelcaseMethodName}Service";
        }
    }

}
