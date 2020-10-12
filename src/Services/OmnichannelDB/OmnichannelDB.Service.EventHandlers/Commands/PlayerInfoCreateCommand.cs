using MediatR;

namespace OmnichannelDB.Service.EventHandlers.Commands
{
    public class PlayerInfoCreateCommand : INotification
    {
        public string Username { get; set; }
        public string PersonalId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
