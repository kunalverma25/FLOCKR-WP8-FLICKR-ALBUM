using System.Diagnostics;
using System.Windows;
using System;
using Microsoft.Phone.Scheduler;
using AroundMe.Core;

namespace AroundMe.Scheduler
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            await LockScreenHelpers.SetRandomImageFromLocalStorage();

            // From:
            // http://msdn.microsoft.com/en-US/library/windowsphone/develop/microsoft.phone.scheduler.scheduledactionservice.launchfortest(v=vs.105).aspx

            // "Use this method during application development to test your background agent
            // implementation. Background agents are launched by the operating system 
            // according to the type of agent and the current state of the system. This 
            // scheduling logic is described in Background agents for Windows Phone. You 
            // can use this method to launch an agent more frequently from your foreground 
            // application or from the agent itself in order to test the agent execution. 
            // This method should only be used during development. You should remove calls 
            // to this method from your production application."

            //ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));

            NotifyComplete();
        }
    }
}