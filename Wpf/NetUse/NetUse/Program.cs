using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetUse
{
    class Program
    {
        static void Main(string[] args)
        {
            var resourcePath = @"\\deamb999000sto.ww002.siemens.net\FS004021567$\TestResults\SI";

            var netUse = new NetUse();

            var resultCode = netUse.MapWithDriveLetter("G", resourcePath);
            PrintMessage(resultCode, "Map with drive letter");

            resultCode = netUse.UnMapDrive("G");
            PrintMessage(resultCode, "Unmap from drive letter");

            resultCode = netUse.MapWithoutDriveLetter(resourcePath);
            PrintMessage(resultCode, "Map without drive letter");

            resultCode = netUse.UnMapResource(resourcePath);
            PrintMessage(resultCode, "Unmap without drive letter");
        }

        private static void PrintMessage(uint resultCode, string infoMessage)
        {
            Console.WriteLine($"Action: {infoMessage.PadRight(50)}; Result Code: {resultCode}");
        }
    }
}
