using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public class Entity
    {
        private static string _entityFolderPath;

        public static string PositionClass = "Position";
        private static Dictionary<string, string> PositionFieldDictionary = new Dictionary<string, string>();

        public static string AnchorsClass = "Anchors";
        private static Dictionary<string, string> AnchorsFieldDictionary = new Dictionary<string, string>();

        static Entity()
        {
            PositionFieldDictionary.Add("x", "double");
            PositionFieldDictionary.Add("y", "double");

            AnchorsFieldDictionary.Add("deviceType", "String");
            AnchorsFieldDictionary.Add("positions", "List<Position>");
        }

        public static void GenerateAllEntity(string entityFolderPath)
        {
            _entityFolderPath = entityFolderPath;

            GenerateEntity(PositionClass, PositionFieldDictionary);
            GenerateEntity(AnchorsClass, AnchorsFieldDictionary);
        }

        private static void GenerateEntity(string entityName, Dictionary<string, string> fieldDict)
        {
            StringBuilder entityBuilder = new StringBuilder();
            entityBuilder.AppendLine("package " + Backend_Anchors.EntityPackagePrefix + ";");
            if (entityName == "Anchors")
            {
                entityBuilder.AppendLine("");
                entityBuilder.AppendLine("import java.util.List;");
            }
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

            if (entityName == "Anchors")
            {
                entityBuilder.AppendLine("    public Anchors(String name) {");
                entityBuilder.AppendLine("        deviceType = name;");
                entityBuilder.AppendLine("    }");
            }

            entityBuilder.AppendLine("}");
            var content = entityBuilder.ToString();
            var filePath = _entityFolderPath + $"\\{entityName}.java";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }
    }
}
