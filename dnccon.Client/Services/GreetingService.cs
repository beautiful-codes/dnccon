using dnccon.Client.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace dnccon.Client.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> logger;

        public GreetingService(ILogger<GreetingService> logger)
        {
            this.logger = logger;
        }

        public void Run()
        {
            for(int i = 0; i < 10; i++)
            {
                logger.LogInformation($"Greeting of time {i} with your helping !!");
            }
        }

            
    }
}
