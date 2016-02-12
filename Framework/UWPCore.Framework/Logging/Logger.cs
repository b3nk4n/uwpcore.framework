using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace UWPCore.Framework.Logging
{
    /// <summary>
    /// A simple logger implementation.
    /// </summary>
    /// <remarks>
    /// Based on <see cref="https://code.msdn.microsoft.com/windowsapps/A-logging-solution-for-c407d880/sourcecode?fileId=120398&pathId=1998894176"/>.
    /// </remarks>
    public static class Logger
    {
        /// <summary>
        /// Thread synchronization object.
        /// </summary>
        private static object sync = new object();

        /// <summary> 
        /// max number of line logged by the system 
        /// </summary> 
        public static int MaxSize { get; set; } = 300;

        /// <summary>
        /// Gets or sets the log data buffer.
        /// </summary>
        private static List<string> Buffer { get; set; }

        /// <summary>
        /// Writes a log line.
        /// </summary>
        /// <param name="e">The exception.</param>
        public static void WriteLine(Exception e)
        {
            WriteLine("EXCEPTION {0} {1}\n STACK TRACE {2}", e.Message, e.InnerException != null ? " HAS INNER EXCEPTION" : "", e.StackTrace);
            if (e.InnerException != null)
            {
                WriteLine(e.InnerException);
            }
        }

        /// <summary>
        /// Writes a log line.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <param name="exceptionDescription">The exeption description.</param>
        public static void WriteLine(Exception e, string exceptionDescription)
        {
            WriteLine("EXCEPTION <{0}> {1} {2}\n STACK TRACE {3}", exceptionDescription, e.Message, e.InnerException != null ? " HAS INNER EXCEPTION" : "", e.StackTrace);
            if (e.InnerException != null)
            {
                WriteLine(e.InnerException);
            }
        }

        /// <summary>
        /// Writes a log line.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The args.</param>
        public static void WriteLine(string format, params object[] args)
        {
            string s = string.Format(format, args);
            WriteLine(s);
        }

        /// <summary>
        /// Writes a log line.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public static void WriteLine(string line)
        {
#if DEBUG
            StringBuilder sb = new StringBuilder("[");
            sb.Append(DateTime.Now.ToString("yyyyMMddhhmss"));
            sb.Append("-TID:");
            sb.Append(Environment.CurrentManagedThreadId);
            sb.Append("] ");
            sb.Append(line);

            lock (sync)
            {
                if (Buffer == null)
                {
                    Buffer = new List<string>();
                }

                Buffer.Add(sb.ToString());

                while (Buffer.Count() > MaxSize)
                {
                    Buffer.RemoveAt(0);
                }
            }

            Debug.WriteLine(sb);
#endif
        }

        /// <summary>
        /// Loads log data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public static void Load(StreamReader stream)
        {
            lock (sync)
            {
                Buffer = new List<string>();

                while (!stream.EndOfStream)
                {
                    Buffer.Add(stream.ReadLine());
                }
            }
        }

        /// <summary>
        /// Saves log data to a stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        public static void Save(StreamWriter stream)
        {
            if (Buffer != null)
            {
                lock (sync)
                {
                    foreach (string s in Buffer)
                    {
                        stream.WriteLine(s);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the stored log as a string.
        /// </summary>
        /// <returns>The stored log.</returns>
        public static string GetStoredLog()
        {
            StringBuilder sb = new StringBuilder();

            lock (sync)
            {
                if (Buffer != null)
                {
                    foreach (string s in Buffer)
                    {
                        sb.AppendLine(s);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
