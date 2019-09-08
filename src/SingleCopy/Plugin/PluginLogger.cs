/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */

using System;
using System.Diagnostics;
using System.Reflection;
using NLog;

namespace SingleCopy.Plugin
{
    public static class PluginLogger
    {
        public static void Trace(string message) => GetCallersLogger().Trace(message);
        public static void Trace(string message, params object[] args) => GetCallersLogger().Trace(message, args);
        public static void Trace(Exception exception) => GetCallersLogger().Trace(exception);
        public static void Trace(Exception exception, string message) => GetCallersLogger().Trace(exception, message);
        public static void Trace(Exception exception, string message, params object[] args) => GetCallersLogger().Trace(exception, message, args);
   

        public static void Info(string message) => GetCallersLogger().Info(message);
        public static void Info(string message, params object[] args) => GetCallersLogger().Info(message, args);
        public static void Info(Exception exception) => GetCallersLogger().Info(exception);
        public static void Info(Exception exception, string message) => GetCallersLogger().Info(exception, message);
        public static void Info(Exception exception, string message, params object[] args) => GetCallersLogger().Info(exception, message, args);


        public static void Debug(string message) => GetCallersLogger().Debug(message);
        public static void Debug(string message, params object[] args) => GetCallersLogger().Debug(message, args);
        public static void Debug(Exception exception) => GetCallersLogger().Debug(exception);
        public static void Debug(Exception exception, string message) => GetCallersLogger().Debug(exception, message);
        public static void Debug(Exception exception, string message, params object[] args) => GetCallersLogger().Debug(exception, message, args);


        public static void Error(string message) => GetCallersLogger().Error(message);
        public static void Error(string message, params object[] args) => GetCallersLogger().Error(message, args);
        public static void Error(Exception exception) => GetCallersLogger().Error(exception);
        public static void Error(Exception exception, string message) => GetCallersLogger().Error(exception, message);
        public static void Error(Exception exception, string message, params object[] args) => GetCallersLogger().Error(exception, message,args);


        public static void Fatal(string message) => GetCallersLogger().Fatal(message);
        public static void Fatal(string message, params object[] args) => GetCallersLogger().Fatal( message, args);
        public static void Fatal(Exception exception) => GetCallersLogger().Fatal(exception);
        public static void Fatal(Exception exception, string message) => GetCallersLogger().Fatal(exception, message);
        public static void Fatal(Exception exception, string message, params object[] args) => GetCallersLogger().Fatal(exception, message, args);


        /* FIX ME - Needs to point to the plugins assembly */
        private static Logger GetCallersLogger() => LogManager.GetLogger((new StackTrace()).GetFrame(2).GetMethod().ReflectedType.FullName);
    }
}
