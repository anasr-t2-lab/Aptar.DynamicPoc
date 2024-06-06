using Aptar.DynamicPoc.Data;
using Aptar.DynamicPoc.Entities;
using Aptar.DynamicPoc.Services;
using Aptar.DynamicPoc.Services.DynamicValidation;
using Aptar.DynamicPoc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        VendorValidation vendorValidation = await _dbContext.VendorValidations.FirstOrDefaultAsync(x => x.Id == id);

        if (vendorValidation is null)
            return NotFound();

        return Ok(vendorValidation);
    }

    [HttpPost]
    public async Task<IActionResult> ValidateForm(Request request)
    {
        //var request = new Request
        //{
        //    Subject = "Sample Request",
        //    Description = "This is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample descriptionThis is a sample description.",
        //    SamplesCount = 25,
        //    Material = (Material)3,
        //    Email = "sample",
        //    PhoneNumber = "hasjsakd",
        //    RequestDate = DateTime.Now - TimeSpan.FromDays(200)
        //};


        //        string formlyTemplate = @"
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
        var vendorValidation = (await _dbContext.VendorValidations.FirstOrDefaultAsync(x => x.Id == 1));

        JArray formlyValidations = JArray.Parse(vendorValidation.GetFormSchemaAsString());
        var validator = new DynamicValidator(formlyValidations);
        
        var formData = JObject.Parse(JsonConvert.SerializeObject(request));

        var result = validator.Validate(formData);

        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors);
            return BadRequest(errors);
        }
        return Ok();
    }


    [HttpPost("x")]
    public async Task<IActionResult> ValidateForm2([FromBody]ColorRequest request)
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
        var vendorValidation = (await _dbContext.VendorValidations.FirstOrDefaultAsync(x => x.Id == 2));


        JArray formlyValidations = JArray.Parse(vendorValidation.GetFormSchemaAsString());
        var validator = new DynamicRequestValidator(formlyValidations);

        var formData = JObject.Parse(JsonConvert.SerializeObject(request));

        var result = validator.Validate(formData);

        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors);
            return BadRequest(errors);
        }
        return Ok();
    }
}
