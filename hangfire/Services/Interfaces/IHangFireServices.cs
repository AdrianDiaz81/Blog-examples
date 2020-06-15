using System.Threading.Tasks;

namespace hangfire.Services.Interfaces
{
    public interface IHangFireServices
    {
        Task ScheduleJob();
    }
}
