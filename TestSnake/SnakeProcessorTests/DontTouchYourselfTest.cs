using BattlesnakeAzureFunction.Model;
using BattlesnakeAzureFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSnake.SnakeProcessorTests
{
    [TestClass]
    public class DontTouchYourselfTest
    {
        [TestMethod]
        public void DontTouchYourselDoesNotTouch()
        {
            GameState gameState = new GameState();
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

            SnakeProcessor sp = new SnakeProcessor(gameState);
            sp.DontTouchYourself();
            Assert.AreEqual(3, sp.AllowedDirections.Count);

            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.up));
            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.down));
            Assert.IsTrue(sp.AllowedDirections.Contains(Direction.left));

        }
    }
}
