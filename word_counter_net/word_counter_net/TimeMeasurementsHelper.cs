using System.Diagnostics;

namespace word_counter_net;

public static class TimeMeasurementsHelper
{
    public static (T result, long minTime) MeasureExecutionTime<T>(Func<T> action, int iterations)
    {
        long minTime = long.MaxValue;
        T finalResult = default;

        for (int i = 0; i < iterations; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            T result = action();
            stopwatch.Stop();

            long elapsed = stopwatch.ElapsedMilliseconds;
            if (elapsed < minTime)
            {
                minTime = elapsed;
                finalResult = result;
            }
        }

        return (finalResult, minTime);
    }

    public static (T result, TimeSpan minTime) MeasureExecutionTimeTimeSpan<T>(Func<T> action, int iterations)
    {
        var minTime = TimeSpan.MaxValue;
        T finalResult = default;

        for (int i = 0; i < iterations; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            T result = action();
            stopwatch.Stop();

            var elapsed = stopwatch.Elapsed;
            if (elapsed < minTime)
            {
                minTime = elapsed;
                finalResult = result;
            }
        }

        return (finalResult, minTime);
    }
}
