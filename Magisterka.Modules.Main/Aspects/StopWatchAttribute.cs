//// // -----------------------------------------------------------------------
//// //  <copyright file="StopWatchAttribute.cs" company="DevCore .NET">
//// //      Copyright DevCore.NET All rights reserved.
//// //  </copyright>
//// //  <author>Paweł Kondzior</author>
//// // -----------------------------------------------------------------------
//namespace Magisterka.Modules.Main.Aspects
//{
//    using System;
//    using PostSharp.Aspects;
//    using log4net;
//    using System.Diagnostics;

//    [Serializable]
//    public sealed class StopWatchAttribute : OnMethodBoundaryAspect
//    {
//        private static readonly ILog log = LogManager.GetLogger(typeof(TraceAttribute).Name);

//        public StopWatchAttribute()
//        {
//        }

//        public override void OnEntry(MethodExecutionArgs args)
//        {
//            args.MethodExecutionTag = Stopwatch.StartNew();
//        }

//        public override void OnExit(MethodExecutionArgs args)
//        {
//            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
//            sw.Stop();


//            var className = args.Method.ReflectedType.Name;
//            var methodName = args.Method.Name;

//            log.Info(String.Format("Klasa {0} Metoda: {1} czas ms: {2}", 
//                className, methodName, sw.ElapsedMilliseconds));
            
//        }

//    }

//}