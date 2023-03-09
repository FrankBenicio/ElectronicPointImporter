using Domain.Requests;
using Domain.Validators;
using FluentValidation.TestHelper;

namespace Domain.Tests.Validators
{
    public class ArchiveUploadRequestValidatorTests
    {
        [Fact]
        public void ShouldHaveErrorWhenFileNameIsEmpty()
        {
            var model = new ArchiveUploadRequest
            {
                FileName = string.Empty,
                File = new MemoryStream(),
            };

            var list = new List<ArchiveUploadRequest>
            {
                model
            };

            var validator = new ArchiveUploadRequestValidator();

            var result = validator.TestValidate(list);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldHaveErrorWhenFileIsNull()
        {
            var model = new ArchiveUploadRequest
            {
                FileName = "file.csv",
                File = null,
            };

            var list = new List<ArchiveUploadRequest>
            {
                model
            };

            var validator = new ArchiveUploadRequestValidator();

            var result = validator.TestValidate(list);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ShouldHaveNotError()
        {
            var model = new ArchiveUploadRequest
            {
                FileName = "file.csv",
                File = new MemoryStream(),
            };

            var list = new List<ArchiveUploadRequest>
            {
                model
            };

            var validator = new ArchiveUploadRequestValidator();

            var result = validator.TestValidate(list);

            Assert.True(result.IsValid);
        }
    }
}
