namespace Demo_3.Interface
{
    public interface ICommandHandler
    {
        public interface ICommandHandler<TCommand>
        {
            Task HandleAsync(TCommand command);
        }
    }
}
