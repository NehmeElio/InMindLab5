using EnrollmentService.Application.Service;
using MediatR;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using UMS_Lab5.Application;
using UMS_Lab5.Application.Service;
using UMS_Lab5.Persistence;
using UMS_Lab5.Persistence.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UMSContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(AdminService).Assembly));

builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
//builder.Services.AddHostedService<RabbitMQConsumerService>();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Grade>();
modelBuilder.EntitySet<Grade>("Grades");
modelBuilder.EntityType<Course>();
modelBuilder.EntitySet<Course>("Courses");
modelBuilder.EntityType<Role>();
modelBuilder.EntitySet<Role>("Roles");
modelBuilder.EntityType<SessionTime>();
modelBuilder.EntitySet<SessionTime>("SessionTimes");
modelBuilder.EntityType<TeacherPerCourse>();
modelBuilder.EntitySet<TeacherPerCourse>("TeacherPerCourses");
modelBuilder.EntityType<TeacherPerCoursePerSessionTime>();
modelBuilder.EntitySet<TeacherPerCoursePerSessionTime>("TeacherPerCoursePerSessionTimes");
modelBuilder.EntityType<User>();
modelBuilder.EntitySet<User>("Users");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",modelBuilder.GetEdmModel()));
builder.Services.AddScoped<GradeService>();
builder.Services.AddMemoryCache();

builder.Services.AddHttpContextAccessor();
/*builder.Services.AddScoped<MultiTenantContext>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var branchId = httpContextAccessor.HttpContext?.Items["BranchId"].ToString();
    var schemaName = GetSchemaNameForBranch(branchId);

    var optionsBuilder = new DbContextOptionsBuilder<UMSContext>();
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

    return new MultiTenantContext(optionsBuilder.Options, "arts_branch");
});*/
builder.Services.AddScoped<MultiTenantContext>(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<MultiTenantContext>();
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    
    // Hard-code the schema name here
    var schemaName = "science_branch";

    return new MultiTenantContext(optionsBuilder.Options, schemaName);
});


string GetSchemaNameForBranch(string branchId)
{
    Console.WriteLine($"Received BranchId: {branchId}");
    // Implement schema name resolution logic based on branchId
    return branchId switch
    {
        "1" => "arts_branch",
        "2" => "science_branch",
        _ => throw new Exception("Unknown branch ID")
    };
}

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();