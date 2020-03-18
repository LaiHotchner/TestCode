using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._4_Statistic.Backend
{
    public static class Mapper
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{GetMapperFileName(table)}.xml";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        internal static string GetContent(HotchnerTable table)
        {
            StringBuilder outterBuilder = new StringBuilder();
            outterBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            outterBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outterBuilder.AppendLine();
            outterBuilder.AppendLine($"<mapper namespace=\"{Backend_Statistic.DaoPackagePrefix}.{Dao.GetDaoClassName(table)}\">");

            outterBuilder.AppendLine($"    <select id=\"{Dao.GetEachStatisticInfo_MethodName(table)}\"  resultType=\"{Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass}\">");
            outterBuilder.AppendLine($"                SELECT count(1), '{table.PascalMethodName}' as deviceType FROM public.{table.DbTableName} where available = 1 " + "${" + "keyword" + "}");
            outterBuilder.AppendLine("    </select>");
            outterBuilder.AppendLine("</mapper>");

            return outterBuilder.ToString();
        }

        public static string GetMapperFileName(HotchnerTable table)
        {
            return table.PascalMethodName + "StatisticMapper";
        }
    }
}
