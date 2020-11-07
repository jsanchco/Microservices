using Service.Common.Filter;

namespace OmnichannelDB.Service.Queries.Filters
{
    public class FilterPlayers : FilterBase
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
