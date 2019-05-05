//// // -----------------------------------------------------------------------
//// //  <copyright file="TraceAttribute.cs" company="DevCore .NET">
//// //      Copyright DevCore.NET All rights reserved.
//// //  </copyright>
//// //  <author>Paweł Kondzior</author>
//// // -----------------------------------------------------------------------

//namespace Magisterka.Modules.Main.Aspects
//{
//    using System;
//    using PostSharp.Aspects;
//    using log4net;
//    using Magisterka.Infrastructure.Shared.Interfaces;

//    [Serializable]
//    public sealed class TraceAttribute : OnMethodBoundaryAspect
//    {
//        public TraceAttribute()
//        {
            
//        }

//        private static readonly ILog log = LogManager.GetLogger(typeof(TraceAttribute).Name);

//        public override void OnEntry(MethodExecutionArgs args)
//        {
//            var className = args.Method.ReflectedType.Name;
//            var methodName = args.Method.Name;

//            log.Info(string.Format("Class {0} Method {1}", className, methodName));

//            var objs = args.Arguments;
//            var parm = args.Method.GetParameters();

//            int index = 0;
//            foreach (var argument in objs)
//            {
//                var argType = argument.GetType();

//                if (argType.IsPrimitive || argType == typeof(string))
//                {
//                    log.Info(string.Format("  parm: {0} wartosc: {1}", parm[index].Name, argument));
//                }
//                else if (argument is ILogItem)
//                {
//                    log.Info(string.Format("  parm: {0} ", parm[index].Name));
//                    (argument as ILogItem).LogItem(log);
//                }

//                index++;
//            }

//        }

//        public override void OnExit(MethodExecutionArgs args)
//        {
//            log.Info("OnExit");
//        } 

//    }
//}