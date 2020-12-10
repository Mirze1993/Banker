using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroORM.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroORM.Logging.Tests
{
    [TestClass()]
    public class LogWriteFileTests
    {
        [TestMethod()]
        public async Task WriteFileTest()
        {
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
        }
    }
}