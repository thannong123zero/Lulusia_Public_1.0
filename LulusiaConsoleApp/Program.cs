using Python.Runtime;

namespace LulusiaConsoleApp
{
    internal class Program
    {
        public static void Main()
        {
            // Set the Python runtime DLL path (adjust based on your Python version)
            Runtime.PythonDLL = @"C:\Users\nguye\AppData\Local\Programs\Python\Python310\python310.dll";

            // Initialize the Python runtime
            PythonEngine.Initialize();

            using (Py.GIL()) // Acquire the Python Global Interpreter Lock (GIL)
            {
                // Execute Python code directly
                PythonEngine.Exec("print('Hello from Python!')");
            }

            PythonEngine.Shutdown();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
