using Aptar.DynamicPoc.Data;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation;
using Aptar.DynamicPoc.Services.SchemaDynamicValidation.Rules;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using NCalcAsync;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Aptar.DynamicPoc.Controllers;

public class AHomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}

[Route("api/vendor-validations")]
public class VendorValidationsController : AbpController
{
    private readonly DynamicPocDbContext _dbContext;

    public VendorValidationsController(DynamicPocDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetVendorValidations(int id)
    {
        RequestType vendorValidation = await _dbContext.RequestTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (vendorValidation is null)
            return NotFound();

        return Ok(vendorValidation);
    }

    //[HttpPost]
    //public async Task<IActionResult> ValidateForm(JObject request)
    //{
    //    //var request = new Request
    //    //{
    //    //    Subject = "Sample Request",
    //    //    Description = "This is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample description.",
    //    //    SamplesCount = 25,
    //    //    Material = (Material)3,
    //    //    Email = "sample",
    //    //    PhoneNumber = "hasjsakd",
    //    //    RequestDate = DateTime.Now - TimeSpan.FromDays(200)
    //    //};


    //    //        string formlyTemplate = @"
    //    //[
    //    //  {
    //    //    ""key"": ""subject"",
    //    //    ""type"": ""input"",
    //    //    ""props"": {
    //    //      ""label"": ""Subject"",
    //    //      ""required"": true,
    //    //      ""maxLength"": 50
    //    //    }
    //    //  },
    //    //  {
    //    //    ""key"": ""description"",
    //    //    ""type"": ""textarea"",
    //    //    ""props"": {
    //    //      ""label"": ""Description"",
    //    //      ""required"": true,
    //    //      ""maxLength"": 500
    //    //    }
    //    //  },
    //    //  {
    //    //    ""key"": ""samplesCount"",
    //    //    ""type"": ""input"",
    //    //    ""props"": {
    //    //      ""label"": ""Samples Count"",
    //    //      ""type"": ""number"",
    //    //      ""required"": true,
    //    //      ""min"": 1,
    //    //      ""max"": 10
    //    //    }
    //    //  },
    //    //  {
    //    //    ""key"": ""material"",
    //    //    ""type"": ""select"",
    //    //    ""props"": {
    //    //      ""label"": ""Material"",
    //    //      ""required"": true,
    //    //      ""options"": [
    //    //        { ""label"": ""Plastic"", ""value"": 1 },
    //    //        { ""label"": ""Aluminum"", ""value"": 2 },
    //    //        { ""label"": ""Glass"", ""value"": 3 }
    //    //      ]
    //    //    }
    //    //  },
    //    //  {
    //    //    ""key"": ""email"",
    //    //    ""type"": ""input"",
    //    //    ""props"": {
    //    //      ""label"": ""Email"",
    //    //      ""required"": true,
    //    //      ""type"": ""email""
    //    //    }
    //    //  },
    //    //  {
    //    //    ""key"": ""phoneNumber"",
    //    //    ""type"": ""input"",
    //    //    ""props"": {
    //    //      ""label"": ""Phone Number"",
    //    //      ""required"": false,
    //    //      ""pattern"": ""^[0-9]*$""
    //    //    }
    //    //  },
    //    //          {
    //    //            ""key"": ""requestDate"",
    //    //            ""type"": ""date"",
    //    //            ""props"": {
    //    //              ""label"": ""Request Date"",
    //    //              ""required"": true,
    //    //              ""minDate"": ""2024-01-01"",
    //    //              ""maxDate"": ""2024-12-31""
    //    //            }
    //    //    }
    //    //]
    //    //";
    //    var vendorValidation = (await _dbContext.VendorValidations.FirstOrDefaultAsync(x => x.Id == 1));

    //    JArray formlyValidations = JArray.Parse(vendorValidation.GetFormSchemaAsString());
    //    var validator = new DynamicValidator(formlyValidations);

    //    //var formData = JObject.Parse(JsonConvert.SerializeObject(request));

    //    var result = validator.Validate(request);

    //    if (!result.IsValid)
    //    {
    //        var errors = string.Join(", ", result.Errors);
    //        return BadRequest(errors);
    //    }
    //    return Ok();
    //}


    [HttpPost("validate2")]
    public async Task<IActionResult> ValidateForm2([FromBody] JObject request)
    {
        //var request = new Request
        //{
        //    Subject = "",
        //    Description = "This is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample description.",
        //    SamplesCount = 25,
        //    Material = (Material)5,
        //    Email = "sample",
        //    PhoneNumber = "hasjsakd",
        //    RequestDate = DateTime.Now - TimeSpan.FromDays(200)
        //};


        //string formlyTemplate = @"
        //[
        //  {
        //    ""key"": ""subject"",
        //    ""type"": ""input"",
        //    ""props"": {
        //      ""label"": ""Subject"",
        //      ""required"": true,
        //      ""maxLength"": 50
        //    }
        //  },
        //  {
        //    ""key"": ""description"",
        //    ""type"": ""textarea"",
        //    ""props"": {
        //      ""label"": ""Description"",
        //      ""required"": true,
        //      ""maxLength"": 500
        //    }
        //  },
        //  {
        //    ""key"": ""samplesCount"",
        //    ""type"": ""input"",
        //    ""props"": {
        //      ""label"": ""Samples Count"",
        //      ""type"": ""number"",
        //      ""required"": true,
        //      ""min"": 1,
        //      ""max"": 10
        //    }
        //  },
        //  {
        //    ""key"": ""material"",
        //    ""type"": ""select"",
        //    ""props"": {
        //      ""label"": ""Material"",
        //      ""required"": true,
        //      ""options"": [
        //        { ""label"": ""Plastic"", ""value"": 1 },
        //        { ""label"": ""Aluminum"", ""value"": 2 },
        //        { ""label"": ""Glass"", ""value"": 3 }
        //      ]
        //    }
        //  },
        //  {
        //    ""key"": ""email"",
        //    ""type"": ""input"",
        //    ""props"": {
        //      ""label"": ""Email"",
        //      ""required"": true,
        //      ""type"": ""email""
        //    }
        //  },
        //  {
        //    ""key"": ""phoneNumber"",
        //    ""type"": ""input"",
        //    ""props"": {
        //      ""label"": ""Phone Number"",
        //      ""required"": false,
        //      ""pattern"": ""^[0-9]*$""
        //    }
        //  },
        //          {
        //            ""key"": ""requestDate"",
        //            ""type"": ""date"",
        //            ""props"": {
        //              ""label"": ""Request Date"",
        //              ""required"": true,
        //              ""minDate"": ""2024-01-01"",
        //              ""maxDate"": ""2024-12-31""
        //            }
        //    }
        //]
        //";
        //var requestType = (await _dbContext.RequestTypes.FirstOrDefaultAsync(x => x.Id == 2));


        //JArray formlyValidations = JArray.Parse(requestType.GetFormSchemaAsString());
        ////var formData = JObject.Parse(JsonConvert.SerializeObject(request));
        //var validator = new DynamicRequestValidator(formlyValidations, request);


        //var result = validator.Validate(request);

        //if (!result.IsValid)
        //{
        //    var errors = string.Join(", ", result.Errors);
        //    return BadRequest(errors);
        //}
        return Ok();
    }


    [HttpGet("CodingSebExpression")]
    public async Task<IActionResult> EvaluateCodingSeb()
    {
        // make an object of ColorRequest
        //var request = new ColorRequest
        //{
        //    Color = "Red",
        //    ColorType = ColorType.RGB,
        //    PartNumber = "p-123",
        //    SampleSubmission = true,
        //    ShippingAddress = "1234, 5th street, New York",
        //    TranslucencePercentage = 10
        //};



        //var validator = new ExpressionValidator();
        //string expression = @"SampleSubmission && Color ==""Red""";

        //validator.AddRule(expression, "error");

        //var jObject = JObject.Parse(JsonConvert.SerializeObject(request));
        //var valresult = validator.Validate(jObject);




        //var evaluator = new ExpressionEvaluator();
        //evaluator.Variables["model"] = request;

        //bool result = evaluator.Evaluate<bool>(expression);

        //////////////////////////////////////////
        //var validator = new ExpressionValidator();

        //// Add combined rules from expressions
        //validator.AddRule("IsActive && Age > 18", "IsActive must be true and Age must be greater than 18.");
        ////validator.AddRule("Name != null && Name != ''", "Name is required.");
        ////validator.AddRule("Email != null && Email.Contains('@')", "Email must be a valid email address.");

        //var jsonObject = new JObject
        //{
        //    ["Name"] = "John",
        //    ["Age"] = 19,
        //    ["Email"] = "john@example.com",
        //    ["IsActive"] = false
        //};

        //var result = validator.Validate(jsonObject);

        return Ok();
    }


    #region NCalc
    //[HttpGet("NCalc")]
    //public async Task<IActionResult> EvaluateNCalc()
    //{

    //    var obj = new ColorRequest
    //    {
    //        Color = "Red",
    //        ColorType = ColorType.RGB,
    //        PartNumber = "p-123",
    //        SampleSubmission = true,
    //        ShippingAddress = "1234, 5th street, New York",
    //        TranslucencePercentage = 10
    //    };


    //    // Create the expression
    //    var expression = new Expression(@"model.SampleSubmission && model.Color ==""Red""");

    //    // Add the person's properties to the expression's parameters
    //    expression.Parameters["name"] = person.Name;
    //    expression.Parameters["age"] = person.Age;

    //    // Evaluate the expression
    //    var result = (bool)expression.Evaluate();


    //    return Ok(result);

    //}


    //public static object? EvaluateExpression(string expressionStr, object obj)
    //{
    //    var expression = new Expression(expressionStr);

    //    expression.EvaluateParameter += (name, args) =>
    //    {
    //        // Split the name to access nested properties
    //        string[] parts = name.Split('.');
    //        if (parts.Length == 2 && parts[0] == "person")
    //        {
    //            var property = obj.GetType().GetProperty(parts[1]);
    //            if (property != null)
    //            {
    //                args.Result = property.GetValue(obj);
    //            }
    //        }
    //    };

    //    expression.EvaluateFunction += (name, args) =>
    //    {
    //        // Split the name to access nested methods
    //        string[] parts = name.Split('.');
    //        if (parts.Length == 2 && parts[0] == "person")
    //        {
    //            var method = obj.GetType().GetMethod(parts[1], BindingFlags.Public | BindingFlags.Instance);
    //            if (method != null)
    //            {
    //                // If the method does not require parameters, pass an empty object array
    //                //args.Result = method.Invoke(obj, method.GetParameters().Length == 0 ? null : args.EvaluatedParameters);
    //            }
    //        }
    //    };

    //    var result = expression.Evaluate();
    //    return result;
    //}
    #endregion


    #region Z
    //[HttpGet("Z")]
    //public async Task<IActionResult> EvaluateZ()
    //{
    //    var model = new ColorRequest
    //    {
    //        Color = "Red",
    //        ColorType = ColorType.RGB,
    //        PartNumber = "p-123",
    //        SampleSubmission = true,
    //        ShippingAddress = "1234, 5th street, New York",
    //        TranslucencePercentage = 10
    //    };

    //    string expression = @"model.SampleSubmission && model.Color ==""Red""";
    //    bool result = Eval.Execute<bool>(expression, new { model });

    //    return Ok(result);
    //}
    #endregion

    [HttpPost]
    [Route("submit")]
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
                    { "placeholder", "Color Type" }
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
                    { "placeholder", "Color" }
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
                    { "placeholder", "e.g., p-12345" }
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
                    { "label", "Translucence percentage" }
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
                    { "label", "Sample Submission" }
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
                    { "rows", 3 }
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule("sampleSubmission"),
                    new MaxLengthRule(250)
                }
            },
            new Field
            {
                Key = "shippingDate",
                Type = "DateTime",
                UiType = "date",
                Properties = new Dictionary<string, object>()
                {
                    { "label", "Shipping Date" }
                },
                ValidationRules = new List<ValidationRule>
                {
                    new RequiredRule("sampleSubmission"),
                    new DateRangeRule(maxDate: new DateTime(2024,10,23))
                }
            }
        };

        _dbContext.RequestSchemas.Add(new RequestSchema
        {
            Name = "ColorMatch",
            Fields = fields
        });
        await _dbContext.SaveChangesAsync();

        return Ok();

    }

    [HttpPost]
    [Route("Validate")]
    public async Task<IActionResult> Validate([FromBody] JObject model)
    {
        var schema = await _dbContext.RequestSchemas.FirstAsync(x=>x.Name == "ColorMatch");

        if (model == null)
        {
            return BadRequest("Model is null.");
        }
        var validator = new DynamicValidator(model, schema.Fields);

        var result = validator.Validate(model);

        return Ok(schema);

    }

    public class DynamicValidator : AbstractValidator<JObject> {
        public DynamicValidator(JObject model, List<Field> fields)
        {
            foreach (var field in fields)
            {
                field.ValidationRules?.ForEach(f => f.ApplyRules(this, field, model));
            }
        }
    }
}
