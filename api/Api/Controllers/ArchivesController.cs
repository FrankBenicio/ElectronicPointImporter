using Domain.Interfaces;
using Domain.Requests;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/archives")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Post([FromServices] IValidator<List<ArchiveUploadRequest>> validator, [FromServices] ICreateArchiveUseCase createArchiveUseCase, [FromForm] List<IFormFile> archives)
        {
            try
            {

                if (!archives.Any())
                    return BadRequest("File not found.");

                var archivesRequest = archives.Select(x => new ArchiveUploadRequest { File = x.OpenReadStream(), FileName = x.FileName }).ToList();

                ValidationResult paramsValidation = await validator.ValidateAsync(archivesRequest);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                foreach (var archive in archivesRequest)
                {
                    await createArchiveUseCase.Execute(archive);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGetListArchivesUseCase getListArchivesUseCase)
        {
            try
            {

                var result = await getListArchivesUseCase.Execute();

                return Ok(result);

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] IValidator<ArchiveIdRequest> validator, [FromServices] IGetArchiveUseCase getArchiveUseCase, Guid id)
        {
            try
            {
                var archiveRequest = new ArchiveIdRequest
                {
                    Id = id
                };

                ValidationResult paramsValidation = await validator.ValidateAsync(archiveRequest);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                var result = await getArchiveUseCase.Execute(archiveRequest);

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("process/{id}")]
        public async Task<IActionResult> PutProcess([FromServices] IValidator<ArchiveIdRequest> validator, [FromServices] IProcessArchiveUseCase processArchiveUseCase, Guid id)
        {
            try
            {
                var archiveRequest = new ArchiveIdRequest
                {
                    Id = id
                };

                ValidationResult paramsValidation = await validator.ValidateAsync(archiveRequest);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                await processArchiveUseCase.Execute(archiveRequest);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }


        [HttpPut("finalize/{id}")]
        public async Task<IActionResult> PutFinalize([FromServices] IValidator<ArchiveIdRequest> validator, [FromServices] IFinalizeArchiveUseCase finalizeArchiveUseCase, Guid id)
        {
            try
            {
                var archiveRequest = new ArchiveIdRequest
                {
                    Id = id
                };

                ValidationResult paramsValidation = await validator.ValidateAsync(archiveRequest);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                await finalizeArchiveUseCase.Execute(archiveRequest);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
