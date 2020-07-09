using Microsoft.ApplicationInsights;

namespace Company.Function
{
    public class ApplicationInsightTelemetry : ITelemetry
    {
        public TelemetryClient ApplicationInsightsClient { get; private set; }

        public void Dependency(Dependency dependency)
        {
            throw new System.NotImplementedException();
        }

        public void Error(Error error)
        {
            throw new System.NotImplementedException();
        }

        public void Event(Event telemetryEvent)
        {
            throw new System.NotImplementedException();
        }

        public static ITelemetry Create(TelemetryClient client)
        {
            return new ApplicationInsightTelemetry
            {
                ApplicationInsightsClient = client
            };
        }

        public void SetUserContext(string userId)
        {
            ApplicationInsightsClient.Context.User.Id = userId;
        }

        public void SetOperationContext(string executionId)
        {
            ApplicationInsightsClient.Context.Operation.Id = executionId;
        }
    }
}