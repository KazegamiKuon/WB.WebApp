using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WB.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext
builder.Services.AddDbContext<InterviewContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InterviewContext") ?? throw new InvalidOperationException("Connection string 'InterviewContext' not found.")));
//Add swagger
builder.Services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Test Interview", Version = "v1" }));

// Add services to the container.
builder.Services.AddControllersWithViews();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7254",
                                              "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Interview"));
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
