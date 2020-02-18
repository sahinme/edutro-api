using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.BlobService
{
    public class BlobService:IBlobService
    {
        private readonly IOptions<MyConfig> _config;  
        public BlobService(IOptions<MyConfig> config)  
        {  
            _config = config;  
        }  
         public async Task<string> InsertFile(IFormFile asset)
         {
             string containerUrl = "edutrocontainer";
             string url =
                 "DefaultEndpointsProtocol=https;AccountName=edutrostorage;AccountKey=y/du5yukFcdZ6A7Z6gJd//pYk+//QbosxpEx52VJE2XkMSKzI7npZywOiuNQXvAV0uM6rnirTWw1XU3M7wrWGw==;EndpointSuffix=core.windows.net";
                    try  
                    {  
                        if (CloudStorageAccount.TryParse(url, out CloudStorageAccount storageAccount))  
                        {  
                            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();  
          
                            CloudBlobContainer container = blobClient.GetContainerReference(containerUrl);  
          
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(asset.FileName);  
          
                            await blockBlob.UploadFromStreamAsync(asset.OpenReadStream());
        
                            return blockBlob.Uri.AbsoluteUri;
                        }  
                        else  
                        {  
                            return "YUKLENNEMEDI";  
                        }  
                    }  
                    catch  
                    {  
                        return "YUKLENEMEDI";  
                    }  
                }  
    }
}