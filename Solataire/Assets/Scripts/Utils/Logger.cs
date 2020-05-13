using System.Diagnostics;
using UnityEngine;

public class Logger
{
    //Singleton Init
    private static readonly Logger instance = new Logger();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Logger()
    {
    }

    private Logger()
    {
        Initialize();
    }

    public static Logger Instance
    {
        get
        {
            return instance;
        }
    }
    //

    private static ILogger m_Logger;

    [Conditional("DEV_BUILD"), Conditional("UNITY_EDITOR")]
    public void Initialize()
    {
        m_Logger = UnityEngine.Debug.unityLogger;
    }

    [Conditional("DEV_BUILD"), Conditional("UNITY_EDITOR")]
    public void PrintLog(string tag, string message)
    {
        Log(LogType.Log, tag, message);
    }

    [Conditional("DEV_BUILD"), Conditional("UNITY_EDITOR")]
    public void PrintError(string tag, string message)
    {
        Log(LogType.Error, tag, message);
    }

    [Conditional("DEV_BUILD"), Conditional("UNITY_EDITOR")]
    public void PrintExc(string tag, string message)
    {
        Log(LogType.Exception, tag, message);
    }

    [Conditional("DEV_BUILD"), Conditional("UNITY_EDITOR")]
    private void Log(LogType type, string tag, string message)
    {
        m_Logger.Log(type, tag, message);
    }
}
