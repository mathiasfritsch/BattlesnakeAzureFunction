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

            var ffc = new FloodFillCalculator(5, snake);

            Assert.AreEqual(1, ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillTwoSpace()
        {
            Coord[] snake = new Coord[4];
            snake[0] = new Coord(1, 0);
            snake[1] = new Coord(1, 1);
            snake[2] = new Coord(1, 2);
            snake[3] = new Coord(0, 2);

            var ffc = new FloodFillCalculator(5, snake);

            Assert.AreEqual(2, ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillAllSpace()
        {
            Coord[] snake = new Coord[0];

            var ffc = new FloodFillCalculator(3, snake);

            Assert.AreEqual(9, ffc.FloodFill(new Coord(0, 0)));
        }

        [TestMethod]
        public void FloodFillAllSpace5x5Down()
        {
            Coord[] snake = new Coord[7];
            snake[0] = new Coord(4, 0);
            snake[1] = new Coord(3, 0);
            snake[2] = new Coord(3, 1);
            snake[3] = new Coord(2, 1);
            snake[4] = new Coord(2, 2);
            snake[5] = new Coord(1, 2);
            snake[6] = new Coord(0, 2);

            var ffc = new FloodFillCalculator(5, snake);

            Assert.AreEqual(5, ffc.FloodFill(new Coord(0, 1)));
        }

        [TestMethod]
        public void FloodFillAllSpace5x5Up()
        {
            Coord[] snake = new Coord[7];
            snake[0] = new Coord(4, 0);
            snake[1] = new Coord(3, 0);
            snake[2] = new Coord(3, 1);
            snake[3] = new Coord(2, 1);
            snake[4] = new Coord(2, 2);
            snake[5] = new Coord(1, 2);
            snake[6] = new Coord(0, 2);

            var ffc = new FloodFillCalculator(5, snake);

            Assert.AreEqual(13, ffc.FloodFill(new Coord(0, 3)));
        }
    }
}