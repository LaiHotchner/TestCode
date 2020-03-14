using System.IO;

namespace CodeSqlGenerate.Utility
{
    public static class CommonMethod
    {
        public static string GetFirstUpAndOtherLowString(string source)
        {
            return source.Substring(0, 1).ToUpper() + source.Substring(1).ToLower();
        }
        public static string GetFirstUpString(string source)
        {
            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }

        public static string GetTemplateName(HotchnerTable table)
        {
            return $"{table.Label}.csv";
            //return $"{table.PascalMethodName}_{table.TableDescription}.csv";
        }



        public static void CreateDirectoryIfNotExist(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static void ClearFolderIfExistFiles(string folder)
        {
            if (Directory.Exists(folder))
            {
                string[] files = Directory.GetFiles(folder);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
