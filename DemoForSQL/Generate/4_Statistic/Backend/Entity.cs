using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class Entity
    {

        private static string _entityFolderPath;

        public static string StatisticInfoClass = "StatisticInfo";
        private static Dictionary<string, string> StatisticInfoFieldDictionary = new Dictionary<string, string>();

        public static string StatisticSummaryClass = "StatisticSummary";
        private static Dictionary<string, string> StatisticSummaryFieldDictionary = new Dictionary<string, string>();

        static Entity()
        {
            StatisticInfoFieldDictionary.Add("deviceType", "String");
            StatisticInfoFieldDictionary.Add("count", "int");

            StatisticSummaryFieldDictionary.Add("equipmentCount", "int[]");
            StatisticSummaryFieldDictionary.Add("categoryCount", "int[]");
        }

        public static void Generate(string entityFolderPath)
        {
            _entityFolderPath = entityFolderPath;

            GenerateEntity(StatisticInfoClass, StatisticInfoFieldDictionary);
            GenerateEntity(StatisticSummaryClass, StatisticSummaryFieldDictionary);
        }

        private static void GenerateEntity(string entityName, Dictionary<string, string> fieldDict)
        {
            StringBuilder entityBuilder = new StringBuilder();
            entityBuilder.AppendLine("package " + Backend_Statistic.EntityPackagePrefix + ";");
            entityBuilder.AppendLine("");
            entityBuilder.AppendLine($"public class {entityName} " + "{");
            entityBuilder.AppendLine("");

            foreach (var item in fieldDict)
            {
                var propertyName = CommonMethod.GetFirstUpString(item.Key);
                entityBuilder.AppendLine($"    private {item.Value} {item.Key};");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine($"    public {item.Value} get{propertyName}() " + "{");
                entityBuilder.AppendLine($"        return {item.Key};");
                entityBuilder.AppendLine("    }");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine($"    public void set{propertyName}({item.Value} value) " + "{");
                entityBuilder.AppendLine($"        this.{item.Key} = value;");
                entityBuilder.AppendLine("    }");
                entityBuilder.AppendLine("");
            }

            entityBuilder.AppendLine("}");
            var content = entityBuilder.ToString();
            var filePath = _entityFolderPath + $"\\{entityName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }
    }
}
