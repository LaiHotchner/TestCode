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
            //GenerateEachMapper(folderPath, tableList);
            GenerateStatisticAll(folderPath, tableList);
        }

        private static void GenerateStatisticAll(string folderPath, List<HotchnerTable> tableList)
        {
            StringBuilder outerBuilder = new StringBuilder();
            outerBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            outerBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outerBuilder.AppendLine();
            outerBuilder.AppendLine($"<mapper namespace=\"{Backend_Statistic.DaoPackagePrefix}.StatisticDao\">");

            outerBuilder.AppendLine($"    <select id=\"{Backend_Statistic.GetAllStatisticResult_MethodName}\" " +
                                     $"parameterType=\"java.lang.String\" " +
                                     $"resultType=\"{Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass}\">");
            for (int i = 0; i < tableList.Count; i++)
            {
                var table = tableList[i];

                outerBuilder.AppendLine($"        SELECT count(1), '{table.PascalMethodName}' as deviceType FROM public.{table.DbTableName}");
                outerBuilder.AppendLine("            where available = 1 ${condition}");
                // 不是最后一个，需要添加 union all
                if (i != tableList.Count - 1)
                {
                    outerBuilder.AppendLine("        union all");
                }
            }

            outerBuilder.AppendLine("    </select>");
            outerBuilder.AppendLine("</mapper>");

            var content = outerBuilder.ToString();
            var filePath = folderPath + "StatisticMapper.xml";
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }



        private static void GenerateEachMapper(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{GetMapperFileName(table)}.xml";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }
        private static string GetContent(HotchnerTable table)
        {
            StringBuilder outterBuilder = new StringBuilder();
            outterBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            outterBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outterBuilder.AppendLine();
            outterBuilder.AppendLine($"<mapper namespace=\"{Backend_Statistic.DaoPackagePrefix}.{Dao.GetDaoClassName(table)}\">");

            outterBuilder.AppendLine($"    <select id=\"{Dao.GetEachStatisticInfo_MethodName(table)}\" " +
                                     $"parameterType=\"java.lang.String\" " +
                                     $"resultType=\"{Backend_Statistic.EntityPackagePrefix}.{Entity.StatisticInfoClass}\">");
            outterBuilder.AppendLine($"                SELECT count(1), '{table.PascalMethodName}' as deviceType FROM public.{table.DbTableName}");
            outterBuilder.AppendLine("                    where available = 1 ${condition}");
            outterBuilder.AppendLine("    </select>");
            outterBuilder.AppendLine("</mapper>");

            return outterBuilder.ToString();
        }
        private static string GetMapperFileName(HotchnerTable table)
        {
            return table.PascalMethodName + "StatisticMapper";
        }
    }
}
