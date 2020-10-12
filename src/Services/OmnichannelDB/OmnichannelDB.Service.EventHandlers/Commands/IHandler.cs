using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers.Commands
{
    public interface IHandler<TCommand> where TCommand : class
    {
        public Task Execute(TCommand command);
    }
}
