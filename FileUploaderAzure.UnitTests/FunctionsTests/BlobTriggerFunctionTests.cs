using FileUploaderAzure.Functions.BlobTriggerFunctions;
using FileUploaderAzure.Functions.ServiceContracts;
using Moq;
using Azure.Storage.Blobs.Models;
using AutoFixture;
using FileUploaderAzure.UnitTests.FakeObjects;

namespace FileUploaderAzure.UnitTests.FunctionsTests
{
    public class BlobTriggerFunctionTests
    {
        private readonly BlobTriggerFunction _function;

        private readonly Fixture _fixture;

        private readonly Mock<IEmailSenderService> _mockEmailSenderService;

        public BlobTriggerFunctionTests()
        {
            _fixture = new Fixture();
            _mockEmailSenderService = new Mock<IEmailSenderService>();

            _function = new BlobTriggerFunction(_mockEmailSenderService.Object);
        }

        [Fact]
        public async Task Run_BlobHasEmailMetadata_SendsEmail()
        {
            // Arrange
            var email = _fixture.Create<string>();

            var properties = new BlobProperties();
            properties.Metadata.Add("Email", email);
            var sasUri = new Uri("https://test.com");
            var blobClient = new FakeBlobClient(properties, sasUri);

            // Act
            await _function.Run(blobClient);

            // Assert
            _mockEmailSenderService.Verify(s => s.SendEmail(email, sasUri.ToString()), Times.Once);
        }

        [Fact]
        public async Task Run_BlobHasEmailMetadata_ShouldIgnoreSendEmailCall()
        {
            // Arrange
            var email = _fixture.Create<string>();

            var properties = new BlobProperties();
            properties.Metadata.Add("NotEmail", email);
            var sasUri = new Uri("https://test.com");
            var blobClient = new FakeBlobClient(properties, sasUri);

            // Act
            await _function.Run(blobClient);

            // Assert
            _mockEmailSenderService.Verify(s => s.SendEmail(email, sasUri.ToString()), Times.Never);
        }
    }
}