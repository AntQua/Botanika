﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CLOUD462022.Services.Interfaces;
using CLOUD462022.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;

        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);

            return await blobClient.DeleteIfExistsAsync();

        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

            var blobs = blobContainerClient.GetBlobsAsync();

            var blobString = new List<string>();

            await foreach (var item in blobs)
            {
                blobString.Add(item.Name);
            }

            return blobString;
        }

      
        public async Task<string> GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri; 
        }

        public async Task<List<Blob>> GetBlobsWithUri (string containerName)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            var blobList = new List<Blob>();

            await foreach (var item in blobs)
            {
                var blobClient = blobContainerClient.GetBlobClient(item.Name);
                Blob blobIndividual = new()
                {
                    Uri = blobClient.Uri.AbsoluteUri 
                };

                BlobProperties properties = await blobClient.GetPropertiesAsync();

                if (properties.Metadata.ContainsKey("title"))
                {
                    blobIndividual.Title = properties.Metadata["title"];
                }

                blobList.Add(blobIndividual);
            }

            return blobList;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName, Blob blob)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainerClient.GetBlobClient(name);

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            IDictionary<string, string> metadata = new Dictionary<string, string>();

            metadata.Add("title", blob.Title);

            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders, metadata);

            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
