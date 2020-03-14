namespace CodeSqlGenerate.Data
{
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
