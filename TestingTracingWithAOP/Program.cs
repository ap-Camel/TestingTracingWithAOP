namespace TestingTracingWithAOP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var testClass = new TestClass();
            var asyncTestclass = new AsyncTestClass();

            try
            {
                //testClass.Method01();
                //testClass.Method02();
                //testClass.Method04();
                testClass.Method03();
                

                //await asyncTestclass.AsyncMethod01();
                //await asyncTestclass.AsyncMethod02();
                //await asyncTestclass.AsyncMethod03();
                //await asyncTestclass.AsyncMethod04();
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception message: " + ex.Message.ToString());
            }            
        }
    }
}
