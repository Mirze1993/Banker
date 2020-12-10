using MicroORM.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            LogWriteFile writeFile = new LogWriteFile();
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");
            await writeFile.WriteFile("birinci");
            await writeFile.WriteFile("ikinci");
            await writeFile.WriteFile("ucuncu");
            await writeFile.WriteFile("dorduncu");

            //Console.WriteLine(Assembly.GetEntryAssembly().Location);
            //Console.WriteLine(new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath);
            //Console.WriteLine(Assembly.GetEntryAssembly().Location);
            //Console.WriteLine(Environment.GetCommandLineArgs()[0]);
            //Console.WriteLine(Process.GetCurrentProcess().MainModule.FileName);
            //Console.WriteLine(Environment.CurrentDirectory);
            //string ProjectDirPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
            //Console.WriteLine(ProjectDirPath);


            Console.ReadLine();

        }
    }
}
