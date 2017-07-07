using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var _FolderName = "c:\\test\\test\\mark\\test.xml".GetLastFolderFromFullPath();

            Console.WriteLine(_FolderName);
            Console.ReadKey();
        }
    }
}
