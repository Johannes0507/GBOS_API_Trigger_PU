using DrLightCutterAPITrigger.Helper;
using DrLightCutterAPITrigger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register LoginService and ForwardService as Singleton services
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<ForwardService>();
builder.Services.AddSingleton<ForwardScheduleService>();

// Configure the application to run as a Windows Service
builder.Host.UseWindowsService();

var app = builder.Build();

#region Token Refresh Service
using (var scope = app.Services.CreateScope())
{
    var loginService = scope.ServiceProvider.GetRequiredService<LoginService>();
    try
    {
        // Perform login
        await loginService.LoginAsync();
        Console.WriteLine("Login successful and the token refresh timer has started...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occurred during login process: {ex.Message}");
    }
}
#endregion

#region Set up forwarding schedule
using (var scope = app.Services.CreateScope())
{
    var forwardScheduleService = scope.ServiceProvider.GetRequiredService<ForwardScheduleService>();
    var forwardSchedule = new ForwardSchedule { Interval = 10, IntervalUnit = TimeUnit.Minutes, IsAutoForwardEnabled = true };
    forwardScheduleService.SetForwardSchedule(forwardSchedule);
}
#endregion

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
