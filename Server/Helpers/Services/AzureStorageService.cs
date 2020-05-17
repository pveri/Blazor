using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public class AzureStorageService : IFileStorageService
    {
        readonly string conn;
        public AzureStorageService(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("AzureStorage");
        }
        public async Task DeleteFile(string fileRoute, string containerName)
        {
            var acct = CloudStorageAccount.Parse(conn);
            var client = acct.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName); // I.E - Folder on device
            var blobName = Path.GetFileName(fileRoute);
            var blob = container.GetBlobReference(blobName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute)
        {
            if (!String.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }
            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName)
        {
            var acct = CloudStorageAccount.Parse(conn);
            var client = acct.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName); // I.E - Folder on device
            _ = await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new Microsoft.WindowsAzure.Storage.Blob.BlobContainerPermissions
            {
                PublicAccess = Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Blob
            });
            var fileName = $"{Guid.NewGuid()}.{extension}";
            var blob = container.GetBlockBlobReference(fileName);
            await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            blob.Properties.ContentType = "image/jpg";
            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}
