using CodeSqlGenerate.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate
{
    public static class SqlScript
    {
        public static readonly string SqlScriptOutputPath = @"C:\0_Infinite\ICTS\2_Generated\SqlScript\";

        public static Dictionary<string, string> RowTypeSqlDict = new Dictionary<string, string>();
        static SqlScript()
        {
            RowTypeSqlDict.Add("bigint", "bigint");

            RowTypeSqlDict.Add("BLOB", "character varying(400)");          // 修改为string，表示文件路径
            RowTypeSqlDict.Add("CHAR", "boolean");
            RowTypeSqlDict.Add("CHAR(1)", "boolean");
            RowTypeSqlDict.Add("CHAR(12)", "character varying(50)");       // Char 2,4,12都太短了，
            RowTypeSqlDict.Add("CHAR(2)", "character varying(50)");
            RowTypeSqlDict.Add("CHAR(4)", "character varying(50)");
            RowTypeSqlDict.Add("DATE", "date");
            RowTypeSqlDict.Add("INTEGER", "integer");
            RowTypeSqlDict.Add("NUMBER", "numeric");
            RowTypeSqlDict.Add("NUMBER(10;2)", "numeric(10,2)");
            RowTypeSqlDict.Add("NUMBER(11;8)", "numeric(11,8)");
            RowTypeSqlDict.Add("NUMBER(12;2)", "numeric(12,2)");
            RowTypeSqlDict.Add("NUMBER(12;3)", "numeric(12,3)");
            RowTypeSqlDict.Add("NUMBER(12;8)", "numeric(12,8)");
            RowTypeSqlDict.Add("NUMBER(2)", "numeric(2,0)");
            RowTypeSqlDict.Add("NUMBER(3)", "numeric(3,0)");
            RowTypeSqlDict.Add("NUMBER(4)", "numeric(4,0)");
            RowTypeSqlDict.Add("NUMBER(5;2)", "numeric(5,2)");
            RowTypeSqlDict.Add("NUMBER(6)", "numeric(6,0)");
            RowTypeSqlDict.Add("NUMBER(6;2)", "numeric(6,2)");
            RowTypeSqlDict.Add("NUMBER(7;3)", "numeric(7,3)");
            RowTypeSqlDict.Add("NUMBER(8;2)", "numeric(8,2)");
            RowTypeSqlDict.Add("NUMBER(9;3)", "numeric(9,3)");
            RowTypeSqlDict.Add("RAW", "bigint");                       // 用作ID，统一使用 bigint
            RowTypeSqlDict.Add("RAW(16)", "bigint");                   // 用作ID，统一使用 bigint
            RowTypeSqlDict.Add("TIMESTAMP(6)", "date");
            RowTypeSqlDict.Add("VARCHAR2", "character varying(20)");        // VARCHAR2,(1),(10)都太短了，改为20
            RowTypeSqlDict.Add("VARCHAR2(1)", "character varying(20)");
            RowTypeSqlDict.Add("VARCHAR2(10)", "character varying(20)");
            RowTypeSqlDict.Add("VARCHAR2(100)", "character varying(100)");
            RowTypeSqlDict.Add("VARCHAR2(1000)", "character varying(1000)");
            RowTypeSqlDict.Add("VARCHAR2(12)", "character varying(12)");
            RowTypeSqlDict.Add("VARCHAR2(14)", "character varying(14)");
            RowTypeSqlDict.Add("VARCHAR2(140)", "character varying(140)");
            RowTypeSqlDict.Add("VARCHAR2(15)", "character varying(15)");
            RowTypeSqlDict.Add("VARCHAR2(1500)", "character varying(1500)");
            RowTypeSqlDict.Add("VARCHAR2(160)", "character varying(160)");
            RowTypeSqlDict.Add("VARCHAR2(18)", "character varying(18)");
            RowTypeSqlDict.Add("VARCHAR2(2)", "character varying(2)");
            RowTypeSqlDict.Add("VARCHAR2(20)", "character varying(20)");
            RowTypeSqlDict.Add("VARCHAR2(200)", "character varying(200)");
            RowTypeSqlDict.Add("VARCHAR2(2000)", "character varying(2000)");
            RowTypeSqlDict.Add("VARCHAR2(25)", "character varying(25)");
            RowTypeSqlDict.Add("VARCHAR2(30)", "character varying(30)");
            RowTypeSqlDict.Add("VARCHAR2(300)", "character varying(300)");
            RowTypeSqlDict.Add("VARCHAR2(4)", "character varying(4)");
            RowTypeSqlDict.Add("VARCHAR2(40)", "character varying(40)");
            RowTypeSqlDict.Add("VARCHAR2(400)", "character varying(400)");
            RowTypeSqlDict.Add("VARCHAR2(4000)", "character varying(4000)");
            RowTypeSqlDict.Add("VARCHAR2(50)", "character varying(50)");
            RowTypeSqlDict.Add("VARCHAR2(500)", "character varying(500)");
            RowTypeSqlDict.Add("VARCHAR2(6)", "character varying(6)");
            RowTypeSqlDict.Add("VARCHAR2(60)", "character varying(60)");
            RowTypeSqlDict.Add("VARCHAR2(600)", "character varying(600)");
            RowTypeSqlDict.Add("VARCHAR2(70)", "character varying(70)");
            RowTypeSqlDict.Add("VARCHAR2(8)", "character varying(8)");
            RowTypeSqlDict.Add("VARCHAR2(80)", "character varying(80)");
        }

        public static void GenerateSqlScript(string createTypeName, List<HotchnerTable> tableList)
        {
            Console.WriteLine();
            Console.WriteLine("Start create Sql script");
            string fileFullPath = $"{SqlScriptOutputPath}Create{createTypeName}SqlScript.sql";
            CommonMethod.CreateDirectoryIfNotExist(SqlScriptOutputPath);
            CommonMethod.ClearFolderIfExistFiles(SqlScriptOutputPath);

            AddScriptDescription(fileFullPath, tableList);

            GenerateEachTable(fileFullPath, tableList);

            Console.WriteLine("Finished create Sql script");
        }


        private static void AddScriptDescription(string fileFullPath, List<HotchnerTable> tableList)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("--该SQL脚本包含以下表的创建：（共" + tableList.Count + "张表）");
            foreach (var table in tableList)
            {
                builder.AppendLine("--    " + table.DbTableName + "：" + table.TableDescription);
            }
            builder.AppendLine();
            var description = builder.ToString();
            File.AppendAllText(fileFullPath, description, Encoding.UTF8);
        }

        private static void GenerateEachTable(string fileFullPath, List<HotchnerTable> tableList)
        {
            foreach (var table in tableList)
            {
                string sqlContent = GetSqlContentByTable(table);
                File.AppendAllText(fileFullPath, sqlContent, Encoding.UTF8);
            }
        }

        private static string GetSqlContentByTable(HotchnerTable table)
        {
            StringBuilder sqlContentBuilder = new StringBuilder();
            sqlContentBuilder.AppendLine($"-- Table: public.{table.DbTableName}");
            sqlContentBuilder.AppendLine();
            sqlContentBuilder.AppendLine($"DROP TABLE IF EXISTS public.{table.DbTableName};");
            sqlContentBuilder.AppendLine($"DROP SEQUENCE  IF EXISTS public.{table.DbTableName}_id_seq;");
            sqlContentBuilder.AppendLine();
            sqlContentBuilder.AppendLine($"CREATE SEQUENCE public.\"{table.DbTableName}_id_seq\"");
            sqlContentBuilder.AppendLine("    INCREMENT 1");
            sqlContentBuilder.AppendLine("    START 1");
            sqlContentBuilder.AppendLine("    MINVALUE 1");
            sqlContentBuilder.AppendLine("    MAXVALUE 9223372036854775807");
            sqlContentBuilder.AppendLine("    CACHE 1;");
            sqlContentBuilder.AppendLine();
            sqlContentBuilder.AppendLine($"ALTER SEQUENCE public.\"{table.DbTableName}_id_seq\"");
            sqlContentBuilder.AppendLine("    OWNER TO sde;");
            sqlContentBuilder.AppendLine();
            sqlContentBuilder.AppendLine($"CREATE TABLE public.{table.DbTableName}");
            sqlContentBuilder.AppendLine("(");

            foreach (var row in table.RowList)
            {
                if (row.Name.ToUpper() == "ID")
                {
                    sqlContentBuilder.AppendLine($"    {row.Name.ToLower()} {RowTypeSqlDict[row.RowType]} NOT NULL DEFAULT nextval('{table.DbTableName}_{row.Name.ToLower()}_seq'::regclass),");
                }
                else if (row.RowType == "RAW(16)" || row.RowType == "RAW")
                {
                    sqlContentBuilder.AppendLine($"    {row.Name.ToLower()} {RowTypeSqlDict[row.RowType]} ,");  //NOT NULL
                }
                else
                {
                    sqlContentBuilder.AppendLine($"    {row.Name.ToLower()} {RowTypeSqlDict[row.RowType]} ,");
                }
            }
            // 逻辑删除标记
            sqlContentBuilder.AppendLine("    available integer DEFAULT 1,");


            if (table.DbTableName == "jj_aj_001")
            {
                // 这是一个mapping表，和其他表不一样，没有主键
                // 同时需要删除最后一个逗号，因为有\r\n，所以是倒数第三个
                sqlContentBuilder.Remove(sqlContentBuilder.Length - 3, 1);
            }
            else
            {
                sqlContentBuilder.AppendLine($"    CONSTRAINT pk_{table.DbTableName}_id PRIMARY KEY (id)");
            }

            sqlContentBuilder.AppendLine(")");
            sqlContentBuilder.AppendLine("TABLESPACE pg_default;");
            sqlContentBuilder.AppendLine();
            sqlContentBuilder.AppendLine($"ALTER TABLE public.{table.DbTableName}");
            sqlContentBuilder.AppendLine("    OWNER to sde;");
            sqlContentBuilder.AppendLine($"COMMENT ON TABLE public.{table.DbTableName}");
            sqlContentBuilder.AppendLine($"    IS '{table.TableDescription}';");
            sqlContentBuilder.AppendLine();

            foreach (var row in table.RowList)
            {
                sqlContentBuilder.AppendLine($"COMMENT ON COLUMN public.{table.DbTableName}.{row.Name}");
                sqlContentBuilder.AppendLine($"    IS '{row.Description}';");
            }
            sqlContentBuilder.AppendLine();

            var sqlcontent = sqlContentBuilder.ToString();
            return sqlcontent;
        }
    }
}
