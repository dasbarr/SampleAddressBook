using System.Diagnostics;

namespace SampleAddressBook;

internal class SimpleLogger
{
    //-------------------------------------------------------------
    // Public class methods
    //-------------------------------------------------------------

    /// <summary>
    /// Adds regular message to the log.
    /// </summary>
    /// <param name="message">Message text.</param>
    public static void Log(string message)
    {
        InternalLog("[Log] " + message);
    }

    /// <summary>
    /// Adds warning message to the log.
    /// </summary>
    /// <param name="message">Warning text.</param>
    public static void Warning(string message)
    {
        InternalLog("[Warning] " + message);
    }

    /// <summary>
    /// Adds error message to the log.
    /// </summary>
    /// <param name="message">Error text.</param>
    public static void Error(string message)
    {
        InternalLog("[Error] " + message);
    }

    //-------------------------------------------------------------
    // Private class methods
    //-------------------------------------------------------------

    /// <summary>
    /// Internal method for log message processing.
    /// </summary>
    /// <param name="message">Log message.</param>
    private static void InternalLog(string message)
    {
#if DEBUG
        Debug.WriteLine(message);
#endif
    }
}
