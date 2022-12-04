using BattlesnakeAzureFunction.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattlesnakeAzureFunction
{
    public class SnakeProcessor
    {
        private List<Direction> allowedDirections;
        private readonly GameState gameState;

        public SnakeProcessor(GameState gameState)
        {
            this.gameState = gameState;
            this.allowedDirections = new List<Direction> { Direction.left,
                Direction.right,
                Direction.up,
                Direction.down };
        }

        public SnakeProcessor StayOnboard()
        {
            var nonOffboardDirections = new List<Direction>();

            foreach (var possibleDirection in allowedDirections)
            {
                if (gameState.Board.MoveStayOnBoad(possibleDirection, gameState.You.Head)) nonOffboardDirections.Add(possibleDirection);
            }
            allowedDirections = nonOffboardDirections;
            return this;
        }

        public SnakeProcessor DontTouchYourself()
        {
            var nonSelfDirections = new List<Direction>();

            if (allowedDirections.Any())
            {
                foreach (var possibleDirection in allowedDirections)
                {
                    if (gameState.You.MoveDontTouchSelf(possibleDirection)) nonSelfDirections.Add(possibleDirection);
                }
            }
            allowedDirections = nonSelfDirections;
            return this;
        }
        public Direction GetMove()
        {
            if (allowedDirections.Any())
            {
                return allowedDirections[new Random().Next(allowedDirections.Count())];
            }

            return Direction.left;
        }
    }
}