import * as ApplicationInsights from 'applicationinsights';
import TelemetryClient = require('applicationinsights/out/Library/TelemetryClient');

export const ConfigureAppInsights = (instrumationKey: string) => {
    const appInsights = ApplicationInsights.setup(instrumationKey)
        .setAutoDependencyCorrelation(true)
        .setAutoCollectRequests(true)
        .setAutoCollectPerformance(true, true)
        .setAutoCollectExceptions(true)
        .setAutoCollectDependencies(true)
        .setAutoCollectConsole(true)
        .setUseDiskRetryCaching(true)
        .setSendLiveMetrics(true)
        .setDistributedTracingMode(
            ApplicationInsights.DistributedTracingModes.AI
        )
        .start();
    return ApplicationInsights.defaultClient;
};
