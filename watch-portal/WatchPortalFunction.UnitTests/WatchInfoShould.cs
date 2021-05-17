using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace WatchPortalFunction.UnitTests
{
    public class WatchInfoShould
    {
        private readonly ILogger _nullLogger;

        public WatchInfoShould()
        {
            _nullLogger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
        }

        [Fact]
        public async Task ReturnOkWhenGivenAModel()
        {
            var queryStringValue = "abc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new Dictionary<string, StringValues>
                    {
                        {"model", queryStringValue}
                    }
                )
            };
            var response = await WatchInfo.Run(request, _nullLogger);

            // Check that the response is an "OK" response
            var result = Assert.IsAssignableFrom<OkObjectResult>(response);

            // Check that the contents of the response are the expected contents
            dynamic watchinfo = new {Manufacturer = "abc", CaseType = "Solid", Bezel = "Titanium", Dial = "Roman", CaseFinish = "Silver", Jewels = 15};
            var watchInfo = $"Watch Details: {watchinfo.Manufacturer}, {watchinfo.CaseType}, {watchinfo.Bezel}, {watchinfo.Dial}, {watchinfo.CaseFinish}, {watchinfo.Jewels}";
            Assert.Equal(watchInfo, result.Value);
        }

        [Fact]
        public async Task ReturnBadRequestWhenNoParametersProvided()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var response = await WatchInfo.Run(request, _nullLogger);

            var result = Assert.IsAssignableFrom<BadRequestObjectResult>(response);
            Assert.Equal("Please provide a watch model in the query string", result.Value);
        }

        [Fact]
        public async Task ReturnBadRequestWhenNoModelProvided()
        {
            var queryStringValue = "abc";
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Query = new QueryCollection
                (
                    new Dictionary<string, StringValues>()
                    {
                        { "not-model", queryStringValue }
                    }
                )
            };

            var response = await WatchInfo.Run(request, _nullLogger);

            var result = Assert.IsAssignableFrom<BadRequestObjectResult>(response);
            Assert.Equal("Please provide a watch model in the query string", result.Value);
        }
    }
}