using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ufollow.Domain.Repositories;

namespace ufollow.Infrastructure.AmazonS3
{
    public class AmazonS3Api : IFileRepository
    {
        private AmazonS3Options _options;
        private AmazonS3Client _client;

        public AmazonS3Api(AmazonS3Options options)
        {
            _options = options;

            var region = RegionEndpoint.GetBySystemName(_options.Region);
            _client = new AmazonS3Client(_options.AccessKeyId, _options.SecretAccessKey, region);
        }

        public async Task Delete(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName
            };

            await _client.DeleteObjectAsync(request);
        }

        public async Task<(byte[] ContentBody, string ContentType)> GetBytes(string fileName)
        {
            var request = new GetObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName
            };

            using (var response = await _client.GetObjectAsync(request))
            using (var responseStream = response.ResponseStream)
            using (var memoryStream = new MemoryStream())
            {
                await responseStream.CopyToAsync(memoryStream);
                return (memoryStream.ToArray(), response.Headers.ContentType);
            }
        }

        public async Task<(string ContentBody, string ContentType)> GetString(string fileName)
        {
            var request = new GetObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName
            };

            using (var response = await _client.GetObjectAsync(request))
            using (var stream = response.ResponseStream)
            using (var reader = new StreamReader(stream))
            {
                return (reader.ReadToEnd(), response.Headers.ContentType);
            }
        }

        public async Task PutBytes(string fileName, byte[] contentBody, string contentType)
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName,
                ContentType = contentType
            };

            using (var stream = new MemoryStream(contentBody))
            {
                request.InputStream = stream;
                await _client.PutObjectAsync(request);
            }
        }

        public async Task PutString(string fileName, string contentBody, string contentType)
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName,
                ContentBody = contentBody,
                ContentType = contentType
            };

            await _client.PutObjectAsync(request);
        }

    }
}
