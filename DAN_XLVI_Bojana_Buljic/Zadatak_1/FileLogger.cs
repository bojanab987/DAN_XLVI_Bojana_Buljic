using System;
using System.IO;
using System.Threading;

namespace Zadatak_1
{
    /// <summary>
    /// Class for logging actions into file
    /// </summary>
    class FileLogger
    {
        // File where to save the message
        private readonly string filePath  = @"..\..\Log.txt";        
        // The lock object protecting the file
        private readonly object locker = new object();

        /// <summary>
        ///Method to Print Message that will be saved in the log file
        /// </summary>
        /// <param name="type">The type of message</param>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <returns></returns>
        public string PrintMessage(string type, string firstName, string lastName)
        {
            return type + " manager: " + firstName + " " + lastName;
        }

        /// <summary>
        /// Save the log message in the file
        /// </summary>
        /// <param name="message">message that will be saved</param>
        public void LogFile(string message)
        {
            lock (locker)
            {
                try
                {
                    Thread.Sleep(2500);
                    StreamWriter streamWriter = new StreamWriter(filePath, append: true);
                    string logMessage = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "] " + message;
                    streamWriter.WriteLine(logMessage.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                catch (FileNotFoundException)
                {
                    File.Create(filePath);
                }
            }
        }
    }
}
