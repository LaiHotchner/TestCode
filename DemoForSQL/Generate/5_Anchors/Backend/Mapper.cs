using CodeSqlGenerate.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate._5_Anchors.Backend
{
    public static class Mapper
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            var mapperContent = GetContent(tableList);
            var mapperFilePath = folderPath + $"{Backend_Anchors.MapperName}.xml";
            File.WriteAllText(mapperFilePath, mapperContent, new UTF8Encoding(false));
        }

        private static string GetContent(List<HotchnerTable> tableList)
        {
            StringBuilder outterBuilder = new StringBuilder();
            outterBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            outterBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outterBuilder.AppendLine();
            outterBuilder.AppendLine($"<mapper namespace=\"{Backend_Anchors.DaoPackagePrefix}.{Backend_Anchors.DaoName}\">");

            foreach (var table in tableList)
            {
                outterBuilder.AppendLine($"    <select id=\"{Backend_Anchors.GetPositions_EachTable(table)}\"  resultType=\"{Backend_Anchors.EntityPackagePrefix}.{Entity.PositionClass}\">");
                outterBuilder.AppendLine($"        SELECT x,y FROM public.{table.DbTableName} where available = 1");
                outterBuilder.AppendLine("    </select>");
            }
            outterBuilder.AppendLine("</mapper>");
            return outterBuilder.ToString();
        }
    }
}
