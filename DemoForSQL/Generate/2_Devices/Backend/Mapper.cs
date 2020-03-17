using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeSqlGenerate.Data;

namespace CodeSqlGenerate.Generate._2_Devices.Backend
{
    internal class Mapper
    {
        internal static void Generate(string folderPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                var content = GetContent(table);
                var filePath = folderPath + $"{Backend_Devices.GetMapperFileName(table)}.xml";
                File.WriteAllText(filePath, content, new UTF8Encoding(false));
            }
        }

        private static string GetContent(HotchnerTable table)
        {
            var dbTableName = table.DbTableName;
            var entityName = Backend_Devices.GetEntityName(table);
            var daoClassName = Backend_Devices.GetDaoClassName(table);

            StringBuilder outterBuilder = new StringBuilder();
            outterBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF" + "-" + "8\" ?>");
            outterBuilder.AppendLine("<!DOCTYPE mapper PUBLIC \" -//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >");
            outterBuilder.AppendLine();
            outterBuilder.AppendLine("<mapper namespace=\"" + Backend_Devices.DaoPackagePrefix + "." + daoClassName + "\">");

            var innerBuilder = new StringBuilder();

            #region insert
            outterBuilder.AppendLine($"    <insert id=\"{Backend_Devices.GetCreateMethodName(table)}\">");
            outterBuilder.AppendLine($"        INSERT INTO public.{dbTableName}(");

            innerBuilder.Append("            ");
            foreach (var row in table.RowList)
            {
                var fieldName = row.Name.ToLower();
                if (fieldName == "id") { continue; }
                innerBuilder.Append($"{fieldName}, ");
            }
            innerBuilder.Remove(innerBuilder.Length - 2, 2);    // 删除最后一个逗号
            innerBuilder.Append(")");

            outterBuilder.AppendLine(innerBuilder.ToString());
            outterBuilder.AppendLine("        VALUES ");
            outterBuilder.AppendLine("        <foreach collection=\"infoList\" item=\"info\" index=\"currentIndex\" separator=\",\">");

            innerBuilder.Clear();
            innerBuilder.Append("            (");
            foreach (var row in table.RowList)
            {
                var fieldName = row.Name.ToLower();
                if (fieldName == "id") { continue; }
                innerBuilder.Append("#{info." + fieldName + "}, ");
            }
            innerBuilder.Remove(innerBuilder.Length - 2, 2);     // 删除最后一个逗号
            innerBuilder.Append(")");

            outterBuilder.AppendLine(innerBuilder.ToString());
            outterBuilder.AppendLine("        </foreach>");
            outterBuilder.AppendLine("    </insert>");
            #endregion

            #region delete 逻辑删除
            outterBuilder.AppendLine($"    <update id=\"{Backend_Devices.GetDeleteByIdMethodName(table)}\">");
            outterBuilder.AppendLine($"        UPDATE public.{dbTableName} set available = 0");
            outterBuilder.AppendLine("             WHERE id=#{id}");
            outterBuilder.AppendLine("    </update>");
            #endregion

            #region update
            innerBuilder.Clear();
            outterBuilder.AppendLine($"    <update id=\"{Backend_Devices.GetUpdateMethodName(table)}\">");
            outterBuilder.AppendLine($"        UPDATE public.{dbTableName}");

            innerBuilder.Append("            SET ");
            foreach (var row in table.RowList)
            {
                var fieldName = row.Name.ToLower();
                if (fieldName == "id") { continue; }
                innerBuilder.Append(fieldName + "=#{info." + fieldName + "}, ");
            }
            innerBuilder.Remove(innerBuilder.Length - 2, 2);    //  删除最后一个逗号
            outterBuilder.AppendLine(innerBuilder.ToString());
            outterBuilder.AppendLine("            WHERE id=#{info.id}");
            outterBuilder.AppendLine("    </update>");
            #endregion

            #region select
            outterBuilder.AppendLine($"    <select id=\"{Backend_Devices.GetByIdMethodName(table)}\"  resultType=\"{Backend_Devices.EntityPackagePrefix}.{entityName}\">");
            outterBuilder.AppendLine($"        SELECT * FROM public.{dbTableName}");
            outterBuilder.AppendLine("            WHERE id=#{id} and available = 1");
            outterBuilder.AppendLine("    </select>");

            outterBuilder.AppendLine($"    <select id=\"{Backend_Devices.GetAllMethodName(table)}\"  resultType=\"{Backend_Devices.EntityPackagePrefix}.{entityName}\">");
            outterBuilder.AppendLine($"        SELECT * FROM public.{dbTableName} where available = 1");
            outterBuilder.AppendLine("    </select>");
            #endregion

            outterBuilder.AppendLine("</mapper>");
            return outterBuilder.ToString();
        }
    }
}
