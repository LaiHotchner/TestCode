using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._3_Retrieval.Backend
{
    public class Entity
    {
        private static string _entityFolderPath;

        public static string RetrievalResultClass = "RetrievalResult";
        private static Dictionary<string, string> RetrievalResultFieldDictionary = new Dictionary<string, string>();

        public static string RetrievalParameterClass= "RetrievalParameter";
        private static Dictionary<string, string> RetrievalParameterFieldDictionary = new Dictionary<string, string>();

        static Entity()
        {
            RetrievalResultFieldDictionary.Add("id", "设备Id");
            RetrievalResultFieldDictionary.Add("deviceType", "设备类型");
            RetrievalResultFieldDictionary.Add("deviceTypeLabel", "设备类型描述");
            RetrievalResultFieldDictionary.Add("name", "名称");
            RetrievalResultFieldDictionary.Add("belongedLine", "所在线");
            RetrievalResultFieldDictionary.Add("centerDistance", "中心里程");
            RetrievalResultFieldDictionary.Add("direction", "上下行");
            RetrievalResultFieldDictionary.Add("description", "备注或者描述信息");

            RetrievalParameterFieldDictionary.Add("keyword", "关键字");
        }

        public static void Generate(string entityFolderPath)
        {
            _entityFolderPath = entityFolderPath;

            GenerateEntity(RetrievalResultClass, RetrievalResultFieldDictionary);
            GenerateEntity(RetrievalParameterClass, RetrievalParameterFieldDictionary);
        }

        private static void GenerateEntity(string entityName, Dictionary<string, string> fieldDict)
        {
            StringBuilder entityBuilder = new StringBuilder();
            entityBuilder.AppendLine("package " + Backend_Retrieval.EntityPackagePrefix + ";");
            entityBuilder.AppendLine("");
            entityBuilder.AppendLine($"public class {entityName} " + "{");
            entityBuilder.AppendLine("");

            foreach (var item in fieldDict)
            {
                var propertyName = CommonMethod.GetFirstUpString(item.Key);
                entityBuilder.AppendLine("    // " + item.Value);
                entityBuilder.AppendLine($"    private String {item.Key};");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine($"    public String get{propertyName}() " + "{");
                entityBuilder.AppendLine($"        return {item.Key};");
                entityBuilder.AppendLine("    }");
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine($"    public void set{propertyName}(String value) " + "{");
                entityBuilder.AppendLine($"        this.{item.Key} = value;");
                entityBuilder.AppendLine("    }");
            }

            entityBuilder.AppendLine("}");
            var content = entityBuilder.ToString();
            var filePath = _entityFolderPath + $"\\{entityName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }
    }
}
