using System.Collections.Generic;

namespace CodeSqlGenerate
{
    public class HotchnerResult
    {
        public List<HotchnerTable> TableList { get; private set; }

        public Dictionary<string, string> ColumnType { get; set; }

        public HotchnerResult()
        {
            ColumnType = new Dictionary<string, string>();
            TableList = new List<HotchnerTable>();
        }
    }

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


    public class HotchnerRow
    {
        public string Name { get; set; }

        public string RowType { get; set; }

        public string Description { get; set; }

        public bool SupportRetrival { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, RowType: {RowType}, Description: {Description}, SupportRetrival:{SupportRetrival}";
        }
    }
}
