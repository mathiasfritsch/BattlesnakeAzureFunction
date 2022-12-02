using BattlesnakeAzureFunction.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
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
            return new OkObjectResult("ok");
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
                    color = "#115566",
                    head = "default",
                    tail = "default",
                    version = "1"
                });
        }

        [FunctionName("Move")]
        public static async Task<IActionResult> Move(
       [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "battlesnake/move")] HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            GameState gameState = JsonConvert.DeserializeObject<GameState>(content);
            Direction[] directions = new Direction[] { Direction.left, Direction.right, Direction.up, Direction.down };
            Random rnd = new Random();

            var head = gameState.You.Head;
            var directionToTake = Direction.left;

            if (gameState.Board.OnBoard(head.AboveMe()))
            {
                directionToTake = Direction.up;
            }
            else if (gameState.Board.OnBoard(head.BelowMe()))
            {
                directionToTake = Direction.down;
            }
            else if (gameState.Board.OnBoard(head.LeftOfMe()))
            {
                directionToTake = Direction.left;
            }
            else if (gameState.Board.OnBoard(head.RightOfMe()))
            {
                directionToTake = Direction.right;
            }

            return new OkObjectResult(
                 new
                 {
                     move = directionToTake.ToString(),
                     shout = "down" + directionToTake.ToString()
                 }
                );
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