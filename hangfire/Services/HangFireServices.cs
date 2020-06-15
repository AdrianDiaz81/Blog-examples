using hangfire.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace hangfire.Services
{
    public class HangFireServices : IHangFireServices
    {
        private readonly IConfiguration configuration;
        private readonly ISendMailServices sendMailServices;

        public HangFireServices(IConfiguration configuration,ISendMailServices sendMailServices )
        {
            this.configuration = configuration;
            this.sendMailServices = sendMailServices;
        }
        public async Task ScheduleJob()
        {
            var email = this.configuration.GetSection("sendmail").Value;
            var message = "Hola q ase";
            await this.sendMailServices.SendMailAsync(email, message);            
        }
    }
}
