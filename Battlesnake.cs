using BattlesnakeAzureFunction.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace BattlesnakeAzureFunction
{
    public static class Battlesnake
    {
        [FunctionName("Echo")]
        public static async Task<IActionResult> Echo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "echo")] HttpRequest request)
        {
            return new OkObjectResult("OK2");
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