using API;
using UnitTest;

namespace CallFsharpFromCsharp
{
    public class Class1
    {
        private int Sum()
        {
            return SomeMath.sum(1, 2);
        }

        private int Divide()
        {
            var option = SomeMath.divide(1, 2);
            return option.Value;
        }

        public void TestInterface()
        {
            var connection = new Say.Connection() as IConnection;
            connection.Message();
        }
    }
}