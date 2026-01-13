using MonsterFighter;
namespace MonsterFighterTests
{
    public class MosterTest
    {
        [Fact]
        public void MonsterStatsTestPass()
        {
            //Arrange
            float health = 22.2f;
            float attack = 5f;
            float armor = 10f;
            float speed = 2;
            var inputValues = new StringReader(health.ToString() + "\n" + attack.ToString() + "\n" + armor.ToString() + "\n" + speed.ToString());
            Console.SetIn(inputValues);
            //Act
            Monster trollo = new Troll("Torollo");
            float resultHealth = trollo.HealthPoints;
            float resultAttack = trollo.AttackPower;
            float resultArmor = trollo.DefencePower;
            float resultSpeed = trollo.Speed;
            //Assert
            Assert.Equal(health, resultHealth);
            Assert.Equal(attack, resultAttack);
            Assert.Equal(armor, resultArmor);
            Assert.Equal(speed, resultSpeed);
        }
    }
}
