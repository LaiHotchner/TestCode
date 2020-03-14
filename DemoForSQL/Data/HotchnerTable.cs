using System.Collections.Generic;

namespace CodeSqlGenerate.Data
{
    public class HotchnerTable
    {
        public int Index { get; set; }

        public string DbTableName { get; set; }

        public string AngularComponentName { get; set; }

        public string camelcaseMethodName { get; set; }

        public string PascalMethodName { get; set; }

        public string Label { get; set; }

        public string TableDescription { get; set; }

        public List<HotchnerRow> RowList { get; set; }

        public HotchnerTable()
        {
            RowList = new List<HotchnerRow>();
        }

        public override string ToString()
        {
            return $"Index:{Index}, \r\n" +
                $"DbTableName:{DbTableName}, AngularComponentName:{AngularComponentName}, \r\n" +
                $"camelcaseMethodName:{camelcaseMethodName},  PascalMethodName:{PascalMethodName}, \r\n" +
                $"Label:{Label}, TableDescription:{TableDescription}\r\n";
        }

        public void AppendAdminCodeRowToTable()
        {
            var row = new HotchnerRow
            {
                Name = "admin_code",
                RowType = "bigint",
                Description = "所属派出所id",
                SupportRetrival = true
            };
            if (!RowList.Contains(row))
            {
                RowList.Add(row);
            }
        }
    }
}
