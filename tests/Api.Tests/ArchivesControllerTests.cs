using Api.Controllers;
using Domain.Interfaces;
using Domain.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests
{
    public class ArchivesControllerTests
    {
        [Fact]
        public async Task UploadCsvShouldReturnOk()
        {
            var file = new Mock<IFormFile>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("colum1,colum2");
            writer.Flush();
            stream.Position = 0;
            var fileName = "arquivo.csv";
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);

            var files = new List<IFormFile> { file.Object };

            var validator = new InlineValidator<List<ArchiveUploadRequest>>();

            var mockCreateArchiveUseCase = new Mock<ICreateArchiveUseCase>();

            var controller = new ArchivesController();

            var result = await controller.Post(validator, mockCreateArchiveUseCase.Object, files);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task UploadCsvShouldReturnBadRequestWhenWhenFileIsNull()
        {
            var file = new Mock<IFormFile>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("colum1,colum2");
            writer.Flush();
            stream.Position = 0;
            var fileName = "";
            //file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);

            var files = new List<IFormFile> { file.Object };

            var validator = new InlineValidator<List<ArchiveUploadRequest>>();
            validator.RuleForEach(x => x)
                .ChildRules(item =>
            {
                item.RuleFor(f => f.FileName)
                    .Must(f => !string.IsNullOrEmpty(f))
                    .WithMessage("FileName not found");

                item.RuleFor(x => x.File)
                    .Must(f => f is not null)
                    .WithMessage("File not found");
            });

            var mockCreateArchiveUseCase = new Mock<ICreateArchiveUseCase>();

            var controller = new ArchivesController();

            var result = await controller.Post(validator, mockCreateArchiveUseCase.Object, files);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadCsvShouldReturnBadRequestWhenFileIsEmpty()
        {
            var files = new List<IFormFile> { };

            var validator = new InlineValidator<List<ArchiveUploadRequest>>();

            var mockCreateArchiveUseCase = new Mock<ICreateArchiveUseCase>();

            var controller = new ArchivesController();

            var result = await controller.Post(validator, mockCreateArchiveUseCase.Object, files);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadCsvShouldReturnBadRequestWhenException()
        {
            var file = new Mock<IFormFile>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("colum1,colum2");
            writer.Flush();
            stream.Position = 0;
            var fileName = "arquivo.csv";
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);

            var files = new List<IFormFile> { file.Object };

            var validator = new InlineValidator<List<ArchiveUploadRequest>>();

            var mockCreateArchiveUseCase = new Mock<ICreateArchiveUseCase>();

            mockCreateArchiveUseCase.Setup(x => x.Execute(It.IsAny<ArchiveUploadRequest>())).ThrowsAsync(new Exception());

            var controller = new ArchivesController();

            var result = await controller.Post(validator, mockCreateArchiveUseCase.Object, files);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetAllShouldReturnOk()
        {
            var mockGetListArchivesUseCase = new Mock<IGetListArchivesUseCase>();
            var controller = new ArchivesController();

            var result = await controller.GetAll(mockGetListArchivesUseCase.Object);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllShouldReturnBadRequestWhenException()
        {
            var mockGetListArchivesUseCase = new Mock<IGetListArchivesUseCase>();
            var controller = new ArchivesController();

            mockGetListArchivesUseCase.Setup(x => x.Execute()).ThrowsAsync(new Exception());

            var result = await controller.GetAll(mockGetListArchivesUseCase.Object);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async Task GetShouldReturnOk()
        {
            var mockGetArchiveUseCase = new Mock<IGetArchiveUseCase>();
            var controller = new ArchivesController();

            var validator = new InlineValidator<ArchiveIdRequest>();

            var result = await controller.Get(validator, mockGetArchiveUseCase.Object, Guid.NewGuid());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetShouldReturnBadRequestWhenException()
        {
            var mockGetArchiveUseCase = new Mock<IGetArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.NewGuid()
            };

            var validator = new InlineValidator<ArchiveIdRequest>();

            mockGetArchiveUseCase.Setup(x => x.Execute(It.IsAny<ArchiveIdRequest>())).ThrowsAsync(new Exception());

            var result = await controller.Get(validator, mockGetArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetShouldReturnBadRequestWhenIdIsEmpty()
        {
            var mockGetArchiveUseCase = new Mock<IGetArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.Empty
            };

            var validator = new InlineValidator<ArchiveIdRequest>();
            validator.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");

            var result = await controller.Get(validator, mockGetArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutProcessShouldReturnOk()
        {
            var mockPutProcessArchiveUseCase = new Mock<IProcessArchiveUseCase>();
            var controller = new ArchivesController();

            var validator = new InlineValidator<ArchiveIdRequest>();

            var result = await controller.PutProcess(validator, mockPutProcessArchiveUseCase.Object, Guid.NewGuid());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutProcessShouldReturnBadRequestWhenException()
        {
            var mockPutProcessArchiveUseCase = new Mock<IProcessArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.NewGuid()
            };

            var validator = new InlineValidator<ArchiveIdRequest>();

            mockPutProcessArchiveUseCase.Setup(x => x.Execute(It.IsAny<ArchiveIdRequest>())).ThrowsAsync(new Exception());

            var result = await controller.PutProcess(validator, mockPutProcessArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutProcessShouldReturnBadRequestWhenIdIsEmpty()
        {
            var mockPutProcessArchiveUseCase = new Mock<IProcessArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.Empty
            };

            var validator = new InlineValidator<ArchiveIdRequest>();
            validator.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");

            var result = await controller.PutProcess(validator, mockPutProcessArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutFinalizeShouldReturnOk()
        {
            var mockPutFinalizeArchiveUseCase = new Mock<IFinalizeArchiveUseCase>();
            var controller = new ArchivesController();

            var validator = new InlineValidator<ArchiveIdRequest>();

            var result = await controller.PutFinalize(validator, mockPutFinalizeArchiveUseCase.Object, Guid.NewGuid());

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutFinalizeShouldReturnBadRequestWhenException()
        {
            var mockPutFinalizeArchiveUseCase = new Mock<IFinalizeArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.NewGuid()
            };

            var validator = new InlineValidator<ArchiveIdRequest>();

            mockPutFinalizeArchiveUseCase.Setup(x => x.Execute(It.IsAny<ArchiveIdRequest>())).ThrowsAsync(new Exception());

            var result = await controller.PutFinalize(validator, mockPutFinalizeArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutFinalizeShouldReturnBadRequestWhenIdIsEmpty()
        {
            var mockPutFinalizeArchiveUseCase = new Mock<IFinalizeArchiveUseCase>();
            var controller = new ArchivesController();

            var request = new ArchiveIdRequest
            {
                Id = Guid.Empty
            };

            var validator = new InlineValidator<ArchiveIdRequest>();
            validator.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("ArchiveId not found");

            var result = await controller.PutFinalize(validator, mockPutFinalizeArchiveUseCase.Object, request.Id);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
