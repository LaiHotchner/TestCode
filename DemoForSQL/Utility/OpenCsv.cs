using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeSqlGenerate.Data;

namespace CodeSqlGenerate.Utility
{
    public static class OpenCsv
    {
        public static List<HotchnerTable> OpenCSV(string filePath)
        {
            Encoding encoding = Encoding.UTF8;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, encoding);

            HotchnerResult result = new HotchnerResult();
            string strLine;
            var table = new HotchnerTable();
            var rowList = new List<HotchnerRow>();
            // 前两行是描述字段
            sr.ReadLine();
            sr.ReadLine();
            while ((strLine = sr.ReadLine()) != null)
            {
                string[] aryLine = strLine.Split(',');
                if (string.IsNullOrEmpty(aryLine[1]))
                {
                    if (table.Index != 0)
                    {
                        table.RowList.AddRange(rowList);
#warning 给所有表添加一个admin_code，所属派出所字段
                        table.AppendAdminCodeRowToTable();
                        result.TableList.Add(table);
                    }

                    table = new HotchnerTable();
                    rowList = new List<HotchnerRow>();
                    continue;
                }

                if (!string.IsNullOrEmpty(aryLine[0]))
                {
                    int.TryParse(aryLine[0], out int index);
                    table.Index = index;
                    table.DbTableName = aryLine[1];
                    table.AngularComponentName = aryLine[2];
                    table.camelcaseMethodName = aryLine[3];
                    table.PascalMethodName = aryLine[4];
                    table.Label = aryLine[5];
                    table.TableDescription = aryLine[6];
                }
                else
                {
                    var row = new HotchnerRow
                    {
                        Name = aryLine[1].Trim().ToUpper(),
                        RowType = aryLine[2].Trim().ToUpper(),
                        Description = aryLine[3].Trim().ToUpper(),
                        SupportRetrival = !string.IsNullOrEmpty(aryLine[4].Trim())
                    };
                    if (!result.ColumnType.ContainsKey(row.RowType))
                    {
                        result.ColumnType.Add(row.RowType, row.RowType);
                    }
                    rowList.Add(row);
                }
            }
            // Add the last table in result
            table.RowList.AddRange(rowList);

            result.TableList.Add(table);
            sr.Close();
            fs.Close();

            var orderResult = GetOrderedTables(result);
            return orderResult;
        }

        private static List<HotchnerTable> GetOrderedTables(HotchnerResult result)
        {
            var listSort = from obj in result.TableList
                           orderby obj.Index
                           select obj;

            return listSort.ToList();
        }
    }
}
