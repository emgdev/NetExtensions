using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace OptionsBasic
{
    public interface IMessenger
    {
        Task WriteMessage(string message);
    }

    public class S3Messenger : IMessenger
    {
        private readonly IAmazonS3 _s3;
        private readonly S3Options _options;

        public S3Messenger(IAmazonS3 s3, IOptions<S3Options> options)
        {
            _s3 = s3 ?? throw new ArgumentNullException(nameof(s3));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task WriteMessage(string message)
        {
            var now = DateTimeOffset.UtcNow;

            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = $"{_options.Prefix}/{now.Year:0000}/{now.Month:00}/{now.Day:00}/{now.Hour:00}/{Guid.NewGuid()}.txt",
                ContentBody = message
            });
        }
    }

    public class S3Options
    {
        public string BucketName { get; set; }

        public string Prefix { get; set; }
    }
}
