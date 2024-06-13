using Aptar.DynamicPoc.Data;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services.DynamicValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Volo.Abp.AspNetCore.Mvc;

namespace Aptar.DynamicPoc.Controllers;

[Route("requests")]
public class RequestsController : AbpController
{
    private readonly DynamicPocDbContext _dbContext;

    public RequestsController(DynamicPocDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRequests()
    {
        var requests = await _dbContext.Requests.ToListAsync();
        return Ok(requests);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetRequestTypes()
    {
        var requests = await _dbContext.RequestTypes.ToListAsync();
        return Ok(requests);
    }

    [HttpPost("{requestTypeId:int::min(1)}")]
    public async Task<IActionResult> AddRequest(int requestTypeId, [FromBody] JObject requestBody)
    {
        var requestType = await _dbContext.RequestTypes.FirstOrDefaultAsync(x => x.Id == requestTypeId);

        var validator = new DynamicRequestValidator(requestType.FormSchema, requestBody);
        var result = validator.Validate(requestBody);

        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors);
            return BadRequest(errors);
        }

        var request = new Request
        {
            RequestTypeId = requestTypeId,
            Body = requestBody
        };

        _dbContext.Requests.Add(request);

        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
