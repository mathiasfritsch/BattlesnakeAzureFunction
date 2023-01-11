using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BattlesnakeAzureFunction.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BattlesnakeAzureFunction
{
    public static class Battlesnake
    {
        [FunctionName("Echo")]
        public static async Task<IActionResult> Echo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "echo")] HttpRequest request)
        {



            var blobStorageConnection = Environment.GetEnvironmentVariable("BlobStorageFromKeyVault", EnvironmentVariableTarget.Process);
            

            return new OkObjectResult($"OK3 {blobStorageConnection.Substring(0,10)}");
        }

        [FunctionName("Blob")]
        public static async Task<IActionResult> Blob(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blob")] HttpRequest request)
        {
            var blobStorageConnection = Environment.GetEnvironmentVariable("BlobStorageFromKeyVault", EnvironmentVariableTarget.Process);

            BlobContainerClient client = new BlobContainerClient(blobStorageConnection, "testcontainer");

            BlobClient blobClient = client.GetBlobClient("someblob.txt");
            string retValue = "";

            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                byte[] result = new byte[download.ContentLength];
                await download.Content.ReadAsync(result, 0, (int)download.ContentLength);

                retValue = Encoding.UTF8.GetString(result);
            }
            return new OkObjectResult($"OK3 {retValue} ");
        }


        [FunctionName("Get")]
        public static async Task<IActionResult> GetBattlesnake(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "battlesnake/")] HttpRequest request)
        {
            return new OkObjectResult(
                new
                {
                    apiversion = "1",
                    author = "happyspider",
                    color = "#112288",
                    head = "silly",
                    tail = "default",
                    version = "1"
                });
        }

        [FunctionName("Move")]
        public static async Task<IActionResult> Move(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "battlesnake/move")] HttpRequest req, ILogger log)
        {
            log.LogInformation($"Starting Move");
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var gameState = JsonConvert.DeserializeObject<GameState>(content);

            var sp = new SnakeProcessor(gameState);

            var move = sp.StayOnboard()
                .DontTouchYourself()
                .DontGoToTightSpace()
                .AvoidOtherSnakes()
                .GetMove();

            //log.LogInformation($"Game:{gameState.Game.ID} - {gameState.Turn}  Head:{gameState.You.Head}  - Move: {directionToTake}");

            return new OkObjectResult(
                 new
                 {
                     move = move.ToString(),
                     shout = "moving " + move.ToString()
                 });
        }

        [FunctionName("End")]
        public static async Task<IActionResult> End(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "battlesnake/end")] HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            GameState gameState = JsonConvert.DeserializeObject<GameState>(content);

            return new OkObjectResult("ok");
        }

        [FunctionName("Start")]
        public static async Task<IActionResult> Start(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "battlesnake/start")] HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            GameState gameState = JsonConvert.DeserializeObject<GameState>(content);

            return new OkObjectResult("ok");
        }
    }
}
