using System.Threading.Tasks;

namespace eoTouchx.Services
{
    public interface ICrashService
    {
        Task ReportApplicationCrash();
    }
}
