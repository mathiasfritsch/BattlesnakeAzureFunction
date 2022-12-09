using BattlesnakeAzureFunction.Model;
using System.Collections.Generic;

namespace BattlesnakeAzureFunction
{
    public class FloodFillCalculator
    {
        private enum FloodFillState
        { notVisited, line, filled }

        private FloodFillState[,] board;
        private int filledCounter = 0;
        private int size = 0;

        public FloodFillCalculator(int size, Coord[] snake)
        {
      
            this.board = new FloodFillState[size, size];
            this.size = size;

            foreach (var snakeItem in snake)
            {
                this.board[snakeItem.X, snakeItem.Y] = FloodFillState.line;
            }
        }

        public int FloodFill(Coord position)
        {
            var allDirections = new List<Direction> { Direction.left,
                Direction.right,
                Direction.up,
                Direction.down };

      
            if (this.board[position.X, position.Y] == FloodFillState.line ||
                this.board[position.X, position.Y] == FloodFillState.filled
                )
            {
                return filledCounter;
            }

            if (this.board[position.X, position.Y] == FloodFillState.notVisited)
            {
                this.board[position.X, position.Y] = FloodFillState.filled;
                this.filledCounter++;
        
                foreach (var direction in allDirections)
                {
                    var newPosition = position.Move(direction);
                    if (newPosition.X < 0 || newPosition.X >= size
                        || newPosition.Y < 0 || newPosition.Y >= size) continue;

                    FloodFill(position.Move(direction));
                }
            }

            return filledCounter;
        }
    }
}