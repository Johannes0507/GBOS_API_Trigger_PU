using DrLightCutterAPITrigger.Helper;
using DrLightCutterAPITrigger.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���U LoginService �M ForwardService �@�� Singleton �A��
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<ForwardService>();
builder.Services.AddSingleton<ForwardScheduleService>();

// �]�w���ε{���b Windows Service ���B��
builder.Host.UseWindowsService();

var app = builder.Build();

#region ��s�O�P�� Service
using (var scope = app.Services.CreateScope())
{
    var loginService = scope.ServiceProvider.GetRequiredService<LoginService>();
    try
    {
        // ����n�J
        await loginService.LoginAsync();
        Console.WriteLine("�n�J���\�ö}�l��s�O�P���p�ɾ�...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"�n�J�L�{���X�{���~: {ex.Message}");
    }
}
#endregion

#region �]�m��o�Ƶ{
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
