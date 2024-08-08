using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS_Lab5.Persistence.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Persistence;

namespace UMS_Lab5.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesOdataController : ODataController
    {
       private readonly UMSContext _context;
       //private readonly MultiTenantContext _context;

        public CoursesOdataController(UMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<Course> Get()
        {
            return _context.Courses;
        }
    }


}