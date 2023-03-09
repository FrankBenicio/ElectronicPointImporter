using Domain.Requests;
using Domain.Validators;
using FluentValidation.TestHelper;
using System.Net;

namespace Domain.Tests.Validators
{
    public class ArchiveIdRequestValidatorTests
    {
        [Fact]
        public void ShouldHaveErrorWhenIdIsEmpty()
        {
            var model = new ArchiveIdRequest
            {
                Id = Guid.Empty
            };

            var validator = new ArchiveIdRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void ShouldHaveErrorWhenIdIsNull()
        {
            var model = new ArchiveIdRequest
            {

            };

            var validator = new ArchiveIdRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void ShouldHaveNotErrorWhenId()
        {
            var model = new ArchiveIdRequest
            {
                Id = Guid.NewGuid()
            };

            var validator = new ArchiveIdRequestValidator();

            var result = validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
