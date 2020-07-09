using Microsoft.Extensions.Primitives;

namespace Company.Function
{
    public interface ITelemetry
    {
        void Event(Event telemetryEvent);
        void Error(Error error);
        void Dependency(Dependency dependency);
        void SetUserContext(string userId);
        void SetOperationContext(string executionId);
    }
}