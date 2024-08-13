namespace TestingTracingWithAOP
{    
    public class AsyncTestClass
    {
        [Test]
        public async Task AsyncMethod01()
        {
            Console.WriteLine("entering AsyncMethod01");
            await Task.Delay(500);
        }

        [Test]
        public async Task AsyncMethod02()
        {
            Console.WriteLine("entering AsyncMethod02");
            await Task.Delay(500);
            Console.WriteLine("throwing inside AsyncMethod02");
            throw new Exception("exception inside AsyncMethod02");
        }

        [Test]
        public async Task<int> AsyncMethod03()
        {
            Console.WriteLine("entering AsyncMethod03");
            await Task.Delay(500);
            Console.WriteLine("throwing inside AsyncMethod03");
            throw new Exception("exception inside AsyncMethod03");
        }

        [Test]
        public async Task<int> AsyncMethod04()
        {
            Console.WriteLine("entering AsyncMethod04");
            await Task.Delay(500);
            Console.WriteLine("throwing inside AsyncMethod04");
            return await Task.FromResult(0);
        }


    }
}
