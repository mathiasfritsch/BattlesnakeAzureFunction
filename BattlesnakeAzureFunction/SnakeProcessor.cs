using BattlesnakeAzureFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattlesnakeAzureFunction
{
    public class SnakeProcessor
    {
        public List<Direction> AllowedDirections;
        private readonly GameState gameState;

        public readonly List<Direction> allDirections = new List<Direction> {
                Direction.left,
                Direction.right,
                Direction.up,
                Direction.down};

        public SnakeProcessor(GameState gameState)
        {
            this.gameState = gameState;
            this.AllowedDirections = new List<Direction> {
                Direction.left,
                Direction.right,
                Direction.up,
                Direction.down };
        }

        public SnakeProcessor StayOnboard()
        {
            var nonOffboardDirections = new List<Direction>();

            foreach (var possibleDirection in AllowedDirections)
            {
                if (gameState.Board.MoveStayOnBoad(possibleDirection, gameState.You.Head))
                    nonOffboardDirections.Add(possibleDirection);
            }
            AllowedDirections = nonOffboardDirections;
            return this;
        }

        public SnakeProcessor DontTouchYourself()
        {
            var nonSelfDirections = new List<Direction>();

            if (AllowedDirections.Any())
            {
                foreach (var possibleDirection in AllowedDirections)
                {
                    if (gameState.You.MoveDontTouchSelf(possibleDirection)) nonSelfDirections.Add(possibleDirection);
                }
            }
            AllowedDirections = nonSelfDirections;
            return this;
        }

        public SnakeProcessor DontGoToTightSpace()
        {
            var bigSpaceDirections = new List<Direction>();

            if (AllowedDirections.Count <= 1 ) return this;
            Dictionary<Direction, int> spacesByDirections = new Dictionary<Direction, int>();

            var snakeBody = gameState.You.Body.Distinct().ToList();
            var ffcBeforeMove = new FloodFillCalculator(
                gameState.Board.Width,
                snakeBody.Where(p => p != gameState.You.Head).ToArray());

            foreach (var possibleDirection in AllowedDirections)
            {
                spacesByDirections.Add(possibleDirection, 0);

                var snake = gameState.You.Body.Distinct().ToList();

                var headAfterMove = gameState.You.Head.Move(possibleDirection);
                var ffc = new FloodFillCalculator(gameState.Board.Width, snake.ToArray());
                spacesByDirections[possibleDirection] = ffc.FloodFill(headAfterMove);
            }

            if(spacesByDirections.Any( s => s.Value >= snakeBody.Count))
            {
                foreach (var spacesByDirection in spacesByDirections.Where(s => s.Value >= snakeBody.Count))
                {
                    bigSpaceDirections.Add(spacesByDirection.Key);
                }
            }
            else
            {
                bigSpaceDirections.Add(spacesByDirections.OrderByDescending(s => s.Value).First().Key); 
            }

            this.AllowedDirections = bigSpaceDirections;
            return this;
        }

        public Direction GetMove()
        {
            if (AllowedDirections.Any())
            {
                return AllowedDirections[new Random().Next(AllowedDirections.Count())];
            }

            return Direction.left;
        }
    }
}