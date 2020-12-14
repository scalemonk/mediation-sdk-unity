using System;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsLogger
    {
        private const string logTag = " SM-ADS | 🤝 "; //this emoji represents unity 🙃

        public static void LogWithFormat(string format, params object[] args)
        {
            DoLog(LogType.Log, format, args);
        }

        private static void DoLog(LogType level, string format, params object[] args)
        {
            Debug.LogFormat(level, LogOption.NoStacktrace, null, getEmojiForLevel(level) + logTag + format, args);
        }

        private static string getEmojiForLevel(LogType level)
        {
            switch (level)
            {
                case LogType.Error:
                    return "❌";
                case LogType.Assert:
                    return "💡";
                case LogType.Warning:
                    return "⚠️";
                case LogType.Log:
                    return "🐞";
                case LogType.Exception:
                    return "💥";
                default:
                    return "⁉️";
            }
        }

        public static void LogWarning(string log, params object[] args)
        {
            DoLog(LogType.Warning, log, args);
        }
        
        public static void LogError(string log, params object[] args)
        {
            DoLog(LogType.Error, log, args);
        }
    }
}