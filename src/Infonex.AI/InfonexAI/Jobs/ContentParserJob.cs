using System.ComponentModel;
using System.Globalization;
using NCrontab;
using NodaTime;

namespace WebApplication2.Jobs;

public class ContentParserJob : BackgroundService
{
    private readonly IClock _clock;
    private CrontabSchedule _schedule;
    private DateTime _nextRun;

    public ContentParserJob(IConfiguration configuration, IClock clock)
    {
        _clock = clock;
        _schedule = CrontabSchedule.Parse(configuration["Jobs:ContentParserJob:Cron"],new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // do
        // {
        //     var now = DateTime.Now;
        //     if (now <= _nextRun) continue;
        //     await Process();
        //     _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        // }
        // while (!stoppingToken.IsCancellationRequested);

        await Task.CompletedTask;
    }
    
    private async Task Process()
    {
        Console.WriteLine("hello world" + DateTime.Now.ToString("F"));
        await Task.CompletedTask;
    }
}