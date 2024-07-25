using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using UMS_Lab5.Application;
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
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(AdminService).Assembly));

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