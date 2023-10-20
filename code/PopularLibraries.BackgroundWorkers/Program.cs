using Hangfire;
using Hangfire.MemoryStorage;
using PopularLibraries.BackgroundWorkers.UseCases;
using PopularLibraries.BackgroundWorkers.Workers;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISampleUseCase, SampleUseCase>();
builder.Services.AddScoped<SendPostCreatedNotification>();
builder.Services.AddHostedService<NativeWorker>();
builder.Services.AddQuartz(config =>
    {
        JobKey key = new JobKey("QuartzExampleJob");
        config.AddJob<QuartzExampleJob>(jobConfig => jobConfig.WithIdentity(key));
        config.AddTrigger(opts => opts
            .ForJob(key)
            .WithIdentity("QuartzExampleJob-trigger")
            //Cada 10 minutos
            // .WithCronSchedule("10 * * * * ?")); 
            //una vez al dia a las 10
            .WithDailyTimeIntervalSchedule(daily =>
                daily.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 00))
                ));
    })
    .AddQuartzHostedService(config => config.WaitForJobsToComplete = true);


builder.Services.AddHangfire(config => config.UseMemoryStorage());

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