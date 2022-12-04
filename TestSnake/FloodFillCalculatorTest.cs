using BattlesnakeAzureFunction;
using BattlesnakeAzureFunction.Model;

namespace TestSnake
{
    [TestClass]
    public class FloodFillCalculatorTest
    {
        [TestMethod]
        public void FloodFillSingleSpace()
        {

            Coord[] snake = new Coord[3];
            snake[0] = new Coord(0, 1);
            snake[1] = new Coord(1, 1);
            snake[2] = new Coord(1, 0);

            var ffc = new FloodFillCalculator(5, snake,4);

            Assert.AreEqual(1,ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillTwoSpace()
        {

            Coord[] snake = new Coord[4];
            snake[0] = new Coord(1, 0);
            snake[1] = new Coord(1, 1);
            snake[2] = new Coord(1, 2);
            snake[3] = new Coord(0, 2);

            var ffc = new FloodFillCalculator(5, snake, 4);

            Assert.AreEqual(2, ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillAllSpace()
        {

            Coord[] snake = new Coord[0];
  
            var ffc = new FloodFillCalculator(3, snake, 100);

            Assert.AreEqual(9, ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillLimit()
        {

            Coord[] snake = new Coord[0];

            var ffc = new FloodFillCalculator(3, snake, 3);

            Assert.AreEqual(3, ffc.FloodFill(new Coord(0, 0)));
        }
    }
}