using WebStore.Domain;

namespace WebStoreServer.Features.Senders
{
    public interface ISender
    {
        public Task<Result<string>> Send(string message);
    }
}
