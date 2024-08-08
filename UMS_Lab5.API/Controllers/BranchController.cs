using Microsoft.AspNetCore.Mvc;

namespace UMS_Lab5.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BranchController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("change")]
    public IActionResult ChangeBranch([FromBody] int branchId)
    {        
        var context = _httpContextAccessor.HttpContext;
        context.Items["BranchId"] = branchId.ToString();
        Console.WriteLine(context.Items["BranchId"]);

        return Ok("Branch changed successfully.");
    }
}