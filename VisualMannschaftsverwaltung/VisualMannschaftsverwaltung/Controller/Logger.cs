using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class Logger
    {
        public static void log(string log)
        {
            DateTime dateTime = DateTime.Now;
            StackFrame frame = new StackFrame(1);

            string classname = frame.GetMethod().DeclaringType.Name;
            string methodname = frame.GetMethod().Name;
            string timestamp = dateTime.ToString("dd.MM.yyyy hh:mm:ss");

            Console.WriteLine($"{timestamp} [INFO | {classname} | {methodname}] {log}");
        }

        public static void error(string log)
        {
            DateTime dateTime = DateTime.Now;
            StackFrame frame = new StackFrame(1);

            string classname = frame.GetMethod().DeclaringType.Name;
            string methodname = frame.GetMethod().Name;
            string timestamp = dateTime.ToString("dd.MM.yyyy hh:mm:ss");

            Console.WriteLine($"{timestamp} [ERROR | {classname} | {methodname}] {log}");
        }
    }
}