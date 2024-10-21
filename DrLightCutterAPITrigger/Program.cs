using DrLightCutterAPITrigger.Helper;
using DrLightCutterAPITrigger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 註冊 LoginService 和 ForwardService 作為 Singleton 服務
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<ForwardService>();
builder.Services.AddSingleton<ForwardScheduleService>();

// 設定應用程式在 Windows Service 中運行
builder.Host.UseWindowsService();

var app = builder.Build();

#region 刷新令牌的 Service
using (var scope = app.Services.CreateScope())
{
    var loginService = scope.ServiceProvider.GetRequiredService<LoginService>();
    try
    {
        // 執行登入
        await loginService.LoginAsync();
        Console.WriteLine("登入成功並開始刷新令牌的計時器...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"登入過程中出現錯誤: {ex.Message}");
    }
}
#endregion

#region 設置轉發排程
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
