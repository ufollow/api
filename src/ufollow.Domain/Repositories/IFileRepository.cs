using System.Threading.Tasks;

namespace ufollow.Domain.Repositories
{
    public interface IFileRepository
    {
        Task Delete(string fileName);
        Task<(byte[] ContentBody, string ContentType)> GetBytes(string fileName);
        Task<(string ContentBody, string ContentType)> GetString(string fileName);
        Task PutBytes(string fileName, byte[] contentBody, string contentType);
        Task PutString(string fileName, string contentBody, string contentType);
    }
}
