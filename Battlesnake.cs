using BattlesnakeAzureFunction.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            GameState gameState = JsonConvert.DeserializeObject<GameState>(content);

            var allDirections = new List<Direction> { Direction.left,
                Direction.right,
                Direction.up,
                Direction.down };

            var directionToTake = Direction.left;

            var nonOffboardDirections = new List<Direction>();

            foreach (var possibleDirection in allDirections)
            {
                if (gameState.Board.MoveStayOnBoad(possibleDirection, gameState.You.Head)) nonOffboardDirections.Add(possibleDirection);
            }

            var nonSelfDirections = new List<Direction>();

            if (nonOffboardDirections.Any())
            {
                foreach (var possibleDirection in nonOffboardDirections)
                {
                    if (gameState.You.MoveDontTouchSelf(possibleDirection)) nonSelfDirections.Add(possibleDirection);
                }
            }

            if (nonSelfDirections.Any())
            {
                directionToTake = nonSelfDirections[new Random().Next(nonOffboardDirections.Count())];
            }

            log.LogInformation($"Game:{gameState.Game.ID} - {gameState.Turn}  Head:{gameState.You.Head}  - Move: {directionToTake}");
            
            return new OkObjectResult(
                 new
                 {
                     move = directionToTake.ToString(),
                     shout = "moving " + directionToTake.ToString()
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