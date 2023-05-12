using System.ComponentModel;
using System.Globalization;
using Cronos;
using NCrontab;
using NodaTime;

namespace WebApplication2.Jobs;

public class ContentParserJob : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IClock _clock;

    public ContentParserJob(IConfiguration configuration, IClock clock)
    {
        _configuration = configuration;
        _clock = clock;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = _clock.GetCurrentInstant().ToDateTimeUtc();
            var next = CronExpression.Parse(_configuration["Jobs:ContentParserJob:Cron"]).GetNextOccurrence(now);
            if (next is null)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                continue;
            }

            var delay = next.Value - now;
            await Task.Delay(delay.Add(TimeSpan.FromSeconds(1)), stoppingToken);
            await Process();
        }
        while (!stoppingToken.IsCancellationRequested);

        await Task.CompletedTask;
    }
    
    private async Task Process()
    {
        Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
        await Task.CompletedTask;
    }
}