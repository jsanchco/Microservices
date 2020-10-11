using MediatR;

namespace OmnichannelDB.Service.EventHandlers.Commands
{
    public class DeviceVerificarionCreateCommand : INotification
    {
        public string PlaytechCode { get; set; }
        public string SiebelId { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
