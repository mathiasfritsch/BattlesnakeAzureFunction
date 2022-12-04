using BattlesnakeAzureFunction;
using BattlesnakeAzureFunction.Model;

namespace TestSnake.SnakeProcessorTests
{
    [TestClass]
    public class DontGoToTightSpaceTest
    {
        [TestMethod]
        public void DontGoToTightSpacePrefersBigSpace()
        {
            var gameState = new GameState();

            gameState.Board = new Board
            {
                Width = 4,
                Height = 4,
            };
            gameState.You = new Snake
            {
                Body = new List<Coord>
                {
                    new Coord(0,2),
                    new Coord(1,2),
                    new Coord(2,2),
                    new Coord(2,1),
                    new Coord(2,0)
                }
            };

            var sp = new SnakeProcessor(gameState);
            sp.DontTouchYourself()
                .StayOnboard()
                .DontGoToTightSpace();

            Assert.AreEqual(Direction.up, sp.AllowedDirections.Single());
        }
    }
}