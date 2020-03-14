using System.Collections.Generic;

namespace CodeSqlGenerate.Data
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
}
