using System.Threading.Tasks;

namespace OmnichannelDB.Service.EventHandlers.Hadlers
{
    public interface IHandler<TCommand> where TCommand : class
    {
        public Task Execute(TCommand command);
    }
}
