using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new NUnit.Framework.Internal.TestExecutionContext().EstablishExecutionEnvironment();
            Assembly assembly = Assembly.LoadFrom("C:\\Users\\Анна\\Documents\\Visual Studio 2015\\Projects\\C_Homework_1\\C_Homework_1\\bin\\Debug\\C_Homework_1.exe");
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(TestFixtureAttribute), false))
                {
                    ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                    object obj = constructorInfo.Invoke(Type.EmptyTypes);
                    MethodInfo[] methodInfo = type.GetMethods();
                    foreach (MethodInfo method in methodInfo)
                    {
                        try
                        {
                            method.Invoke(obj, new object[] { });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
