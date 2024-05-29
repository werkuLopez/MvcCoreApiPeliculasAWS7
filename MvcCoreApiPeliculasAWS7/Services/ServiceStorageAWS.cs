using Amazon.S3;
using Amazon.S3.Model;
using MvcCoreApiPeliculasAWS7.Models;

namespace MvcCoreApiPeliculasAWS7.Services
{
    public class ServiceStorageAWS
    {
        private IAmazonS3 client;
        private string BucketName;

        public ServiceStorageAWS(IAmazonS3 client, keysModel keys)
        {
            this.client = client;
            this.BucketName = keys.BucketName;
        }

        public async Task<bool> UploadFileAsync(string fileName, Stream st)
        {
            PutObjectRequest request =
                new PutObjectRequest
                {
                    InputStream = st,
                    Key = fileName,
                    BucketName = this.BucketName,
                };

            PutObjectResponse response = await client.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
