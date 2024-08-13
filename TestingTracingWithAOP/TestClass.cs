namespace TestingTracingWithAOP
{
    [Test]
    public class TestClass
    {
        public void Method01()
        {
            Console.WriteLine("entering Method01");
        }

        public void Method02()
        {
            Console.WriteLine("entering Method02");
            Console.WriteLine("throwing exception inside Method02");
            throw new Exception("exception inside Method02");
        }

        public int? Method03()
        {
            Console.WriteLine("entering Method03");
            Console.WriteLine("throwing exception inside Method03");
            throw new Exception("exception inside Method03");
        }

        public int Method04()
        {
            Console.WriteLine("entering Method04");
            Console.WriteLine("throwing exception inside Method04");
            return 1;
        }
    }
}
