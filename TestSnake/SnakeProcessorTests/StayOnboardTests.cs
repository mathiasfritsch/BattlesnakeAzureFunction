using BattlesnakeAzureFunction;
using BattlesnakeAzureFunction.Model;

namespace TestSnake.SnakeProcessorTests
{
    [TestClass]
    public class StayOnboardTests
    {
        [TestMethod]
        public void SnakeStaysOnBoard()
        {
            var gameState = new GameState();
            gameState.Board = new Board
            {
                Width = 11,
                Height = 11,
            };
            gameState.You = new Snake
            {
                Body = new List<Coord>
                {
                    new Coord(0,1),
                    new Coord(1,1),
                    new Coord(1,0),
                }
            };

            var sp = new SnakeProcessor(gameState);
            sp.StayOnboard();
            Assert.AreEqual(3, sp.AllowedDirections.Count);

            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.up));
            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.down));
            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.right));
        }
    }
}