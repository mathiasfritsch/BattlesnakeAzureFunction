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
            int minimumSpace = 4;

            var bigSpaceDirections = new List<Direction>();

            foreach (var possibleDirection in AllowedDirections)
            {
                var snake = gameState.You.Body.Distinct().ToList();

                var headAfterMove = gameState.You.Head.Move(possibleDirection);
                snake.Add(headAfterMove);

                int bestSize = 0;
                foreach (var directionForFloodFill in allDirections)
                {
                    var ffc = new FloodFillCalculator(gameState.Board.Width,
                          snake.ToArray(),
                          minimumSpace);

                    var floodFillPosition = headAfterMove.Move(directionForFloodFill);

                    if (floodFillPosition.X < 0 ||
                        floodFillPosition.X > gameState.Board.Width - 1||
                        floodFillPosition.Y < 0 ||
                        floodFillPosition.Y > gameState.Board.Height - 1 )
                            continue;

                    int size = ffc.FloodFill(floodFillPosition);
                    bestSize = size > bestSize ? size : bestSize;
                }
                if (bestSize >= minimumSpace)
                    bigSpaceDirections.Add(possibleDirection);
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