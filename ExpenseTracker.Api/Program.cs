using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Contracts;

var builder = WebApplication.CreateBuilder(args);

/********************
 * SERVICE CONTAINER:
 ********************/

//Controllers service:  
builder.Services.AddControllers(options =>
{
   options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

//API endppoint metadata service:
builder.Services.AddEndpointsApiExplorer();

//Swagger sercice for API documentation:
builder.Services.AddSwaggerGen();

//Croess Orgin Resource Shearing (COURS) Service:
builder.Services.AddCors(x => x.AddDefaultPolicy(builder => builder.AllowAnyOrigin()));

//Data access sercice:
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

/************************
 * HTTP REQUEST PIPELINE:
 ************************/
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Default rout:
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();