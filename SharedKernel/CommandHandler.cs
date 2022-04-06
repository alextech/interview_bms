using MediatR;

// common utility class I reuse between projects
// TODO MediatR 10 introduced contracts only package, making command definition more lightweight - separate from overall
// handling library. Need to research how to upgrade this to current style.
namespace SharedKernel
{
    public interface ICommand : IRequest<CommandResponse>
    {

    }

    public interface ICommand<T> : IRequest<CommandResponse<T>>
    {

    }

    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, CommandResponse>
        where TRequest : ICommand
        where TResponse : CommandResponse
    {

    }

    public class CommandResponse
    {
        public bool Success { get; }
        public string Description { get; }

        protected CommandResponse(bool success, string description)
        {
            Success = success;
            Description = description;
        }

        public static CommandResponse Ok(string detail)
        {
            return new CommandResponse(true, detail);
        }

        public static CommandResponse Problem(string detail)
        {
            return new CommandResponse(false, detail);
        }
    }

    public class CommandResponse<T> : CommandResponse
    {
        public T? Data { get; }

        private CommandResponse(T? data, bool success, string description) : base(success, description)
        {
            Data = data;
        }

        public static CommandResponse<T> Ok(T data, string description)
        {
            return new CommandResponse<T>(data, true, description);
        }

        public static CommandResponse<T> Problem(T? data, string description)
        {
            return new CommandResponse<T>(data, false, description);
        }
    }
}