using AspectInjector.Broker;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestingTracingWithAOP
{
    [Aspect(Scope.Global)]
    [Injection(typeof(TestAttribute))]
    public class TestAttribute : Attribute
    {
        private static MethodInfo _taskTHandler = typeof(TestAttribute).GetMethod(nameof(TestAttribute.WrapAsync), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo _taskHandler = typeof(TestAttribute).GetMethod(nameof(TestAttribute.WrapTaskAsync), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo _normalMethodHandler = typeof(TestAttribute).GetMethod(nameof(TestAttribute.WrapSync), BindingFlags.NonPublic | BindingFlags.Static);

        [Advice(Kind.Around, Targets = Target.Method)]
        public object Around(
        [Argument(Source.Name)] string name,
        [Argument(Source.Arguments)] object[] args,
        [Argument(Source.Target)] Func<object[], object> target,
        [Argument(Source.ReturnType)] Type retType)
        {
            Console.WriteLine("entering Around for method name: {0}", name);
            if (typeof(Task).IsAssignableFrom(retType) && retType.IsGenericType)
            {
                var syncResultType = retType.IsConstructedGenericType ? retType.GenericTypeArguments[0] : typeof(object);
                return _taskTHandler.MakeGenericMethod(syncResultType).Invoke(this, new object[] { target, args, name });
            }
            else if (typeof(Task).IsAssignableFrom(retType) && retType.IsGenericType == false)
            {
                return _taskHandler.Invoke(this, new object[] { target, args, name });
            }
            else if (retType.FullName == "System.Void")
            {
                return WrapVoid(target, args, name);
            }
            else
            {
                return _normalMethodHandler.MakeGenericMethod(retType).Invoke(this, new object[] { target, args, name });
            }
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void TraceAfter([Argument(Source.Name)] string name)
        {
            Console.WriteLine("entering TraceAfter for method name: {0}", name);
        }

        private static T WrapSync<T>(Func<object[], object> target, object[] args, string name)
        {
            try
            {
                return (T)target(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("entering WrapSync method's catch block for method name: {0}", name);
                ExceptionDispatchInfo.Capture(e).Throw();
                return default(T);
            }
        }

        private static async Task WrapTaskAsync(Func<object[], object> target, object[] args, string name)
        {
            try
            {
                await (Task)target(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("entering WrapSync method's catch block for method name: {0}", name);
                ExceptionDispatchInfo.Capture(e).Throw();
                return default(T);
            }
        }

        private static async Task<T> WrapAsync<T>(Func<object[], object> target, object[] args, string name)
        {
            try
            {
                return await (Task<T>)target(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("entering WrapSync method's catch block for method name: {0}", name);
                ExceptionDispatchInfo.Capture(e).Throw();
                return default;
            }
        }

        private object WrapVoid(Func<object[], object> target, object[] args, string name)
        {
            try
            {
                return target(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("entering WrapSync method's catch block for method name: {0}", name);
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
    }
}
