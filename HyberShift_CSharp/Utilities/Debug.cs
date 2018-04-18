using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class using for debug
// Created: TM Quan 19/4

namespace HyberShift_CSharp.Utilities
{
    public static class Debug
    {
        //private static StringBuilder sb = new StringBuilder();

        public static void Log(string content)
        {
            File.AppendAllText("log.txt", DateTime.Now.ToString() + ": " + content.ToString() + Environment.NewLine);
        }
    }
}
