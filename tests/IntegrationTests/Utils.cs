using System.Net.Mime;
using System.Text;

using Newtonsoft.Json;

namespace VirtualBookstore.IntegrationTests;

public static class Utils
{
    public static StringContent CreateRequestAsStringContent<TModel>(TModel model) =>
        new(JsonConvert.SerializeObject(model),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

    public static async Task ExecuteConcurrently(uint process, Func<CancellationToken, Task> execution)
    {
        IEnumerable<int> processes = Enumerable.Range(1, (int)process);
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
        await Parallel.ForEachAsync(processes,
            parallelOptions,
            async (_, cancellationToken) =>
            {
                await execution(cancellationToken);
            });
    }
}
