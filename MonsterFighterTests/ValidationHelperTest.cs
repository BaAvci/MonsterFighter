using MonsterFighter;
namespace MonsterFighterTests
{
    public class ValidationHelperTest
    {
        // Es existieren keine fails oder grau zonen tests. Der user würde in einer endlosschleife sitzten und nicht rauskommen.


        [Fact]
        public void ValidNumberTestPass()
        {
            //Arrange
            float expectedResult = 50.532f;
            string outputMsg = "Unzulässige Zahl";

            var input = new StringReader(expectedResult.ToString());
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);
            // Act
            float result = ValidationHelper.ValueInputCheck(outputMsg);
            //Assert

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ValidNumberTestRetryPass()
        {
            //Arrange
            float expectedResult = 50.532f;
            string failInput = "Guten tag könnte ich 5 kaffes bekommen?\n";
            string outputMsg = "Unzulässige Zahl";

            var input = new StringReader(failInput + expectedResult.ToString());
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);
            // Act
            float result = ValidationHelper.ValueInputCheck(outputMsg);
            string stringResult = output.ToString().Trim();
            //Assert

            Assert.Equal(expectedResult, result);
            Assert.Equal(outputMsg, stringResult);
        }

        [Fact]
        public void YesNoTestPass()
        {
            //Arrange
            int expectedResultInput = 1;

            var input = new StringReader(expectedResultInput.ToString());
            Console.SetIn(input);

            // Act
            bool result = ValidationHelper.YesNoCheck();
            //Assert

            Assert.True(result);
        }

        [Fact]
        public void YesNoTestRetryPass()
        {
            //Arrange
            int expectedResult = 1;
            int missInput = 5;
            string outputMsg = "Bitte geben Sie entweder 1 oder 2 ein!";

            var input = new StringReader(missInput.ToString() + "\n" + expectedResult.ToString());
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            bool result = ValidationHelper.YesNoCheck();
            string stringResult = output.ToString().Trim();
            //Assert

            Assert.True(result);
            Assert.Equal(outputMsg, stringResult);
        }

        [Fact(Skip = "ReadKey can not be mocked. Method has to be rewriten!")]
        public void CharInputCheckTestPass()
        {
            //Arrange
            List<char> inputChars =
            [
                'a', 'b', 'c',
            ];
            char expectedResult = 'a';

            var input = new StringReader(expectedResult.ToString());
            Console.SetIn(input);

            // Act
            char result = ValidationHelper.CharInputCheck(inputChars);
            //Assert

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void BetweenOneAndTwoPass()
        {

            //Arrange
            int expectedResult = 1;
            var input = new StringReader(expectedResult.ToString());
            Console.SetIn(input);

            //Act
            int result = ValidationHelper.CheckValueBetween(1, 2);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void BetweenOneAndTwoRetryPass()
        {

            //Arrange
            int expectedResult = 1;
            int failedInput = 5;
            int minValue = 1;
            int maxValue = 2;
            string expectedStringOutput = $"Bitte geben Sie eine ganze Zahl ein die zwischen {minValue} und {maxValue} liegt.\r\n";
            var input = new StringReader(failedInput.ToString() + "\n" + expectedResult.ToString());
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);
            //Act
            int result = ValidationHelper.CheckValueBetween(minValue, maxValue);
            var stringResult = output.ToString();
            //Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedStringOutput, stringResult);

        }

        [Fact]
        public void BetweenOneAndTwoTenRetryPass()
        {

            //Arrange
            int expectedResult = 1;
            int failedInput = 5;
            int minValue = 1;
            int maxValue = 2;

            string msgOutput = $"Bitte geben Sie eine ganze Zahl ein die zwischen {minValue} und {maxValue} liegt.\r\n";
            string stringFailInput = failedInput.ToString() + "\n";
            string inputString = "";
            string expectedStringOutput = "";

            for (int i = 0; i < 10; i++)
            {
                expectedStringOutput += msgOutput;
                inputString += stringFailInput;
            }

            var input = new StringReader(inputString + expectedResult.ToString());
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            int result = ValidationHelper.CheckValueBetween(minValue, maxValue);
            var stringResult = output.ToString();

            //Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedStringOutput, stringResult);
        }
    }
}