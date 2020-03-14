using CodeSqlGenerate.Generate._3_Retrieval.Backend;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate.JavaCode._3_Retrieval
{
    internal class Mapper
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var mapperContent = GetContent(tableList);
            var mapperFilePath = folderPath + $"{Backend_Retrieval.MapperName}.xml";
            File.WriteAllText(mapperFilePath, mapperContent, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder outterBuilder = new StringBuilder();
            outterBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            outterBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outterBuilder.AppendLine();
            outterBuilder.AppendLine($"<mapper namespace=\"{Backend_Retrieval.DaoPackagePrefix}.{Backend_Retrieval.DaoName}\">");

            foreach (var table in tableList)
            {
                var dbTableName = table.DbTableName;
                outterBuilder.AppendLine($"    <select id=\"{Backend_Retrieval.GetListMethodName_EachDao_GetRetrievalResult(table)}\"  resultType=\"{Backend_Retrieval.EntityPackagePrefix}.{Entity.RetrievalResultClass}\">");
                outterBuilder.AppendLine("        SELECT id, mc as name, szx as belongedLine,");
                outterBuilder.AppendLine("               zxlc as centerDistance, sxx as direction,");
#warning 这里涉及到设备类型
                outterBuilder.AppendLine($"               bz as description, '{table.PascalMethodName}' as deviceType, '{table.Label}' as deviceTypeLabel");
                outterBuilder.AppendLine($"        FROM public.{dbTableName}");
                outterBuilder.AppendLine("            WHERE available = 1");
                var matchStr = GetSearchConcatString(table);
                outterBuilder.AppendLine(matchStr);
                outterBuilder.AppendLine("    </select>");

            }
            outterBuilder.AppendLine("</mapper>");
            return outterBuilder.ToString();
        }

        private static string GetSearchConcatString(HotchnerTable table)
        {
            var builder = new StringBuilder();
            builder.Append("            and CONCAT(");
            foreach (var row in table.RowList)
            {
                if (row.SupportRetrival)
                {
                    builder.Append(row.Name + ", ");
                }
            }
            builder.Remove(builder.Length - 2, 2);
            builder.Append(")like '%${keyword}%'");
            return builder.ToString();
        }
    }
}
