using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Concurrency.Collections.Tests
{
    public class ConcurrentDictionaryTests
    {
        private readonly ITestOutputHelper _output;

        public ConcurrentDictionaryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Should_iterate_while_adding()
        {
            var sut = new ConcurrentDictionary<int, int>();

            var addDelay = 10;
            var iterateDelay = 11;

            var numberOfItems = 10;

            var addTask = Task.Run(async () =>
            {
                for (var i = 0; i < numberOfItems; i++)
                {
                    sut.TryAdd(i, i);

                    await Task.Delay(addDelay);
                }
            });

            var actualNumberOfItems = 0;

            var iterateTask = Task.Run(async () =>
            {
                await Task.Delay(addDelay);

                foreach (var kvp in sut)
                {
                    //_output.WriteLine($"[{kvp.Key}] = {kvp.Value}");

                    Interlocked.Increment(ref actualNumberOfItems);

                    await Task.Delay(iterateDelay);
                }
            });

            await Task.WhenAll(addTask, iterateTask);

            Assert.Equal(numberOfItems, actualNumberOfItems);
        }
    }
}
