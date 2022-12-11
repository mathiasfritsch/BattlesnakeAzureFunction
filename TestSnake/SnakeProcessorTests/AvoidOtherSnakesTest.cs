using BattlesnakeAzureFunction;
using BattlesnakeAzureFunction.Model;

namespace TestSnake.SnakeProcessorTests
{
    [TestClass]
    public class AvoidOtherSnakesTest
    {
        [TestMethod]
        public void DontTouchOtherSnakeTest()
        {
            var gameState = new GameState();
            gameState.Board = new Board
            {
                Width = 5,
                Height = 5,
            };
            gameState.You = new Snake
            {
                Body = new List<Coord>
                {
                    new Coord(1,2),
                    new Coord(2,2),
                    new Coord(3,2),
                    new Coord(4,2)
                }
            };

            var otherSnake = new Snake
            {
                Body = new List<Coord>
                {
                    new Coord(0,1),
                    new Coord(0,2),
                    new Coord(0,3)
                }
            };

            gameState.Board.Snakes.Add(gameState.You);
            gameState.Board.Snakes.Add(otherSnake);


            var sp = new SnakeProcessor(gameState);
            sp.AvoidOtherSnakes();
            Assert.AreEqual(3, sp.AllowedDirections.Count);

          }
    }
}