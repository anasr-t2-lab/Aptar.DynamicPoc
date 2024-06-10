using Aptar.DynamicPoc.Data;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services.DynamicValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using NCalcAsync;
using Newtonsoft.Json.Linq;
using Volo.Abp.AspNetCore.Mvc;

namespace Aptar.DynamicPoc.Controllers;

public class AHomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}

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
    public IActionResult Submit([FromBody] JObject model)
    {

        FormSchmea mmm = new();

        Field colorType = new()
        {
            Key = "colorType",
            Type = "radio",
            Properties = new()
            {
                { "placeholder" , "Color Type" },
                { "label" , "Color Type" }
            },
            Validators = new()
            {
                new Validator
                {
                    Type = "required",
                    Message = "Color Type is required"
                }
            },
            Options = new()
            {
                new Option
                {
                    Label = "RGB",
                    Value = "RGB"
                },
                new Option
                {
                    Label = "HEX",
                    Value = "HEX"
                },
                new Option
                {
                    Label = "CMYK",
                    Value = "CMYK"
                }
            }
        };

        mmm.Fields.Add(colorType);

        mmm.AddField("color", "input", new() { { "placeholder", "Color" }, { "label", "Color" } }, new List<Validator>() { new Validator { Type = "required", Message = "color is required" } });
        mmm.AddField("partNumber", "input", new() { { "placeholder", "Part Number" }, { "label", "Part Number" } }, new List<Validator>() { new Validator { Type = "required", Message = "Part Number is required" }, new Validator { Type = "pattern", Message = "pattern", Parameters = new Dictionary<string, object>() { { "pattern", "^p-\\d+$" } } } });
        mmm.AddField("translucencePercentage", "input", new() { { "placeholder", "Part Number" }, { "label", "Part Number" } }, new List<Validator>() { new Validator { Type = "required", Message = "Part Number is required" }, new Validator { Type = "pattern", Message = "pattern", Parameters = new Dictionary<string, object>() { { "pattern", "^p-\\d+$" } } } });


        mmm.AddField("name", "text", new() { { "placeholder", "name" } },
            new List<Validator>
            {
                new Validator{ Type = "required", Message = "Name is required" },
                new Validator{ Type = "minLength", Message = "Name must be at least 3 characters long", Parameters = new Dictionary<string, object>{ { "length", 3 } } },
                new Validator{ Type = "maxLength", Message = "Name must be at most 50 characters long", Parameters = new Dictionary<string, object>{ { "length", 50 } } }
            });

        mmm.AddField("Desc", "text", new() { { "placeholder", "Desc" } },
            new List<Validator>
            {
                new Validator{ Type = "maxLength", Message = "Name must be at most 50 characters long", Parameters = new Dictionary<string, object>{ { "length", 50 } } }
            });
        if (model == null)
        {
            return BadRequest("Model is null.");
        }

        // Process the JObject model
        string name = model["name"]?.ToString();
        int age = model["age"]?.ToObject<int>() ?? 0;
        DateTime date = model["date"]?.ToObject<DateTime>() ?? DateTime.MinValue;

        return Ok($"Name: {name}, Age: {age}, Date: {date}");
    }
}
