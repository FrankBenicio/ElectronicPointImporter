using Domain.Interfaces;
using Domain.Models;
using Domain.Requests;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/department-payment")]
    [ApiController]
    public class DepartmentPaymentController : ControllerBase
    {
        [HttpGet("archive/{archiveId}")]
        public async Task<IActionResult> Get([FromServices] IValidator<ArchiveIdRequest> validator, [FromServices] IGetListDepartmentPaymentByArchiveIdUseCase getListDepartmentPaymentByArchiveIdUseCase, Guid archiveId)
        {
            try
            {
                var archiveRequest = new ArchiveIdRequest
                {
                    Id = archiveId
                };

                ValidationResult paramsValidation = await validator.ValidateAsync(archiveRequest);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                var result = await getListDepartmentPaymentByArchiveIdUseCase.Execute(archiveRequest);

                return Ok(result);

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IValidator<DepartmentPayment> validator, [FromServices] ICreateDepartmentPaymentUseCase createDepartmentPaymentUseCase, [FromBody] DepartmentPayment model)
        {
            try
            {
                ValidationResult paramsValidation = await validator.ValidateAsync(model);

                if (!paramsValidation.IsValid)
                    return BadRequest(paramsValidation.Errors);

                await createDepartmentPaymentUseCase.Execute(model);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

    }
}
