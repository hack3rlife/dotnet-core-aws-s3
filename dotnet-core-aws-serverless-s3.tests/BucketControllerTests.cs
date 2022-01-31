using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Amazon.S3.Model;
using dotnet.core.aws.serverless.s3;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace dotnet_core_aws_serverless_s3.Tests
{
    public class BucketControllerTests
    {
        private LambdaEntryPoint LambdaFunction { get; set; }
        private TestLambdaContext TestLambdaContext { get; set; }
        public BucketControllerTests()
        {
            LambdaFunction = new LambdaEntryPoint();
            TestLambdaContext = new TestLambdaContext();
        }

        [Fact]
        public async Task BucketController_GetBuckets_Success()
        {
            // Arrange
            var payload = @"{
                              ""resource"": ""/{proxy+}"",
                              ""path"": ""/api/Bucket"",
                              ""httpMethod"": ""GET"",
                              ""headers"": null
                            }";

            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(payload);

            // Act
            var response = await LambdaFunction.FunctionHandlerAsync(request, TestLambdaContext);


            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Body);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            var buckets = JsonConvert.DeserializeObject<List<S3Bucket>>(response.Body);
            Assert.NotNull(buckets);
        }

        [Fact]
        public async Task BucketController_GetFilesByBucketName_Success()
        {
            // Arrange
            var payload = @"{
                              ""resource"": ""/{proxy+}"",
                              ""path"": ""/api/Bucket/Files"",
                              ""httpMethod"": ""GET"",
                              ""queryStringParameters"": {
                                ""bucketName"": ""aws-s3-webapi""
                              }
                            }";
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(payload);

            // Act
            var response = await LambdaFunction.FunctionHandlerAsync(request, TestLambdaContext);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Body);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);

            var bucket = JsonConvert.DeserializeObject<ListObjectsV2Response>(response.Body);
            Assert.NotNull(bucket);
            Assert.Equal("aws-s3-webapi", bucket.Name);
        }

        [Fact]
        public async Task BucketController_CreateBucket_Success()
        {
            // Arrange
            var payload = @"{
                              ""resource"": ""/{proxy+}"",
                              ""path"": ""/api/Bucket"",
                              ""httpMethod"": ""POST"",
                              ""body"": ""ewogICAgImJ1Y2tldE5hbWUiOiAidGVzdC1idWNrZXQtczMiLAogICAgInJlZ2lvbiI6ICJ1cy1lYXN0LTIiCiAgfQ=="",
                              ""isBase64Encoded"": true,
                              ""headers"": {
                                ""Content-Type"": ""application/json"",
                                ""Accept"": ""text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8""
                              }
                            }";
            var request = JsonConvert.DeserializeObject<APIGatewayProxyRequest>(payload);

            // Act
            var response = await LambdaFunction.FunctionHandlerAsync(request, TestLambdaContext);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Body);
            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);

        }
    }
}