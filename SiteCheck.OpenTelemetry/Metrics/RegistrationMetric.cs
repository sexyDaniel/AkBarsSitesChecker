using SiteCheck.OpenTelemetry.Metrics.Constants;
using System.Diagnostics.Metrics;

namespace SiteCheck.OpenTelemetry.Metrics;

public static class RegistrationMetric
{
    private readonly static Meter _meter = new(MeterNames.RegistrationMeter, "1.0.0");

    public static readonly Counter<int> RegistrationCounter = _meter.CreateCounter<int>("Registration.Count");
}
