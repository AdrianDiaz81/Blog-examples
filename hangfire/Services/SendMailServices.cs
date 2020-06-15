using System.Threading.Tasks;

namespace hangfire.Services
{
    public class SendMailServices : ISendMailServices
    {        
        
        public async  Task<bool> SendMailAsync(string mail, string message)
        {
            await Task.Delay(3000);
            return true;
        }
    }
}
