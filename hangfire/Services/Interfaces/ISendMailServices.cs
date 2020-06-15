using System.Threading.Tasks;

namespace hangfire.Services
{
    public interface ISendMailServices
    {
        Task<bool> SendMailAsync(string mail, string message);
    }
}
