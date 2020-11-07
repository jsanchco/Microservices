namespace Service.Common.Filter
{
    public abstract class FilterBase
    {
        public int page { get; set; }
        public int take { get; set; }

        public FilterBase()
        {
            page = 1;
            take = 10;
        }
    }
}
