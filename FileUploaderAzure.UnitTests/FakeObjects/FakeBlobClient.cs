using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploaderAzure.UnitTests.FakeObjects
{
    /// <summary>
    /// Fake object for BlobClient class for unit testing purposes.
    /// </summary>
    public class FakeBlobClient : BlobClient
    {
        private readonly BlobProperties _properties;
        private readonly Uri _sasUri;

        public FakeBlobClient(BlobProperties properties, Uri sasUri) : base(new Uri("http://test/"), new BlobClientOptions())
        {
            _properties = properties;
            _sasUri = sasUri;
        }

        public override Task<Response<BlobProperties>> GetPropertiesAsync(
            BlobRequestConditions? conditions = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Response.FromValue(_properties, null!));
        }

        public override Uri GenerateSasUri(BlobSasBuilder builder)
        {
            return _sasUri;
        }
    }
}
