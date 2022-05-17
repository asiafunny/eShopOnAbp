using System;
using System.Threading.Tasks;
using Polly;
using Serilog;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EShop.Shared.Hosting.Microservice.DbMigrations;

public abstract class PendingMigrationsCheckerBase : ITransientDependency
{
    public async Task TryAsync(Func<Task> check, int retryCount = 3)
    {
        var retryPolicy = Policy.Handle<Exception>()
                                .WaitAndRetry(retryCount,
                                              attemptCount => TimeSpan.FromSeconds(attemptCount * RandomHelper.GetRandom(2000, 5000)),
                                              (ex, sleepDuration, attemptNumber, _) =>
                                              {
                                                  Log.Warning($"{ex.GetType().Name} has been thrown. Retrying in {sleepDuration}. {attemptNumber} / {retryCount}. Exception:\n{ex.Message}");
                                              });
        await retryPolicy.Execute(async () =>
                                  {
                                      await check();
                                  });
    }
}
