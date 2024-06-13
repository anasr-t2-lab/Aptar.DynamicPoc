using Aptar.DynamicPoc.Data;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Volo.Abp.AspNetCore.Mvc;

namespace Aptar.DynamicPoc.Controllers;

[Route("aptar-requests")]
public class AptarRequestsController : AbpController
{
    private readonly DynamicPocDbContext _dbContext;

    public AptarRequestsController(DynamicPocDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("upsert")]
    public async Task<IActionResult> Submit()
    {
        List<Field> fields = new List<Field>()
        {
            new Field
            {
                Key = "colorType",
                Type = "int",
                UiType = "radio",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Color Type" },
                    { "placeholder", "Color Type" },
                    { "uiRow", 1},
                    { "uiColPercentage", 50}
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule(),
                    new InOptionsRule()
                },
                Options = new List<Option>
                {
                    new Option { Label = "Pantone", Value = "Pantone" },
                    new Option { Label = "RGB", Value = "RGB" },
                    new Option { Label = "HEX", Value = "HEX" },
                    new Option { Label = "CMYK", Value = "CMYK" }
                }
            },
            new Field
            {
                Key = "color",
                Type = "string",
                UiType = "color",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Color" },
                    { "placeholder", "Color" },
                    { "uiRow", 1},
                    { "uiColPercentage", 50}
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule()
                }
            },
            new Field
            {
                Key = "partNumber",
                Type = "string",
                UiType = "input",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Part Number" },
                    { "placeholder", "e.g., p-12345" },
                    { "uiRow", 2},
                    { "uiColPercentage", 25}
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule(),
                    new PatternRule("^p-\\d+$", "Part number must start with \"p-\" and be followed by positive numbers.")
                }
            },
            new Field
            {
                Key = "translucencePercentage",
                Type = "int",
                UiType = "slider",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Translucence percentage" },
                    { "uiRow", 3},
                    { "uiColPercentage", 100}
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule(),
                    new MinRule(1, "Number must be at least 1."),
                    new MaxRule(100, "Number must be at most 100.")
                }
            },
            new Field
            {
                Key = "sampleSubmission",
                Type = "bool",
                UiType = "checkbox",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Sample Submission" },
                    { "uiRow", 4},
                    { "uiColPercentage", 25}
                }
            },
            new Field
            {
                Key = "shippingDate",
                Type = "DateTime",
                UiType = "date",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Shipping Date" },
                    { "uiRow", 5},
                    { "uiColPercentage", 50},
                    { "expression.hide", "!sampleSubmission" }
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule("sampleSubmission"),
                    new DateRangeRule(maxDate: new DateTime(2024,10,23))
                }
            },
            new Field
            {
                Key = "shippingAddress",
                Type = "string",
                UiType = "textarea",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Shipping Address" },
                    { "rows", 3 },
                    { "uiRow", 6},
                    { "uiColPercentage", 100},
                    { "expression.hide", "!sampleSubmission" }
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule("sampleSubmission"),
                    new MaxLengthRule(250)
                }
            }
        };

        RequestSchema schema = new RequestSchema
        {
            Name = "ColorMatch",
            Fields = fields
        };

        var requestSchema = await _dbContext.RequestSchemas.FirstOrDefaultAsync(x => x.Name == schema.Name);

        if (requestSchema is null)
        {
            _dbContext.RequestSchemas.Add(schema);
        }
        else
        {
            requestSchema.Fields = schema.Fields;
        }

        await _dbContext.SaveChangesAsync();
        return Ok(schema);
    }


    [HttpGet("schemas")]
    public async Task<IActionResult> GetRequestSchemas()
    {
        var requests = await _dbContext.RequestSchemas.ToListAsync();
        return Ok(requests);
    }

    [HttpPost]
    [Route("Validate")]
    public async Task<IActionResult> Validate([FromBody] JObject model)
    {
        var schema = await _dbContext.RequestSchemas.FirstAsync(x => x.Name == "ColorMatch");

        if (model == null)
        {
            return BadRequest("Model is null.");
        }
        var validator = new DynamicValidator(model, schema.Fields);

        var result = validator.Validate(model);

        if (result.IsValid)
        {
            return Ok("Model is valid.");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }


    [HttpPost("{requestTypeId:int::min(1)}")]
    public async Task<IActionResult> AddRequest(int requestTypeId, [FromBody] JObject requestBody)
    {
        var schema = await _dbContext.RequestSchemas.FirstOrDefaultAsync(x => x.Id == requestTypeId);
        if (schema is null)
            return NotFound();

        var validator = new DynamicValidator(requestBody, schema.Fields);
        var result = validator.Validate(requestBody);

        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors);
            return BadRequest(errors);
        }

        var request = new AptarRequest
        {
            RequestTypeId = requestTypeId,
            Body = requestBody
        };

        await _dbContext.AptarRequests.AddAsync(request);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
