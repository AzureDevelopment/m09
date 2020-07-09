import { AzureFunction, Context, HttpRequest } from '@azure/functions';
import { ConfigureAppInsights } from '../telemetry';
import * as stringify from 'json-stringify-safe';

const httpTrigger: AzureFunction = async function (
    context: Context,
    req: HttpRequest
): Promise<void> {
    const telemetry = ConfigureAppInsights(process.env.ai_instrumationKey);
    telemetry.trackEvent({
        name: 'Starting some important operation',
        contextObjects: context,
        properties: {
            req: stringify(req),
        },
        time: new Date(),
    });

    const startOfDependencyCall = new Date();
    const result = await fetch('https://google.pl');
    telemetry.trackDependency({
        dependencyTypeName: 'HTTP',
        name: 'Fetching something important',
        duration: new Date().valueOf() - startOfDependencyCall.valueOf(),
        data: Buffer.from(result.body).toString('utf-8'),
        resultCode: result.status,
        success: result.status == 200,
    });

    context.res = {
        body: 2 / 0,
    };
};

export default httpTrigger;
