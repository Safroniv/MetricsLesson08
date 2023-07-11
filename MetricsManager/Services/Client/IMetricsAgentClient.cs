using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
    }
}
