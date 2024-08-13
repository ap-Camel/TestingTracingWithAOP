using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTracingWithAOP
{
    public interface ITracingContext : IDisposable
    {
        ActivitySource ActivitySource { get; }
    }

    internal class TracingContext : ITracingContext
    {
        internal const string ActivitySourceName = "CIR-Trace";
        private List<Activity> _activities;

        public TracingContext()
        {
            string version = typeof(TracingContext).Assembly.GetName().Version?.ToString();
            ActivitySource = new ActivitySource(ActivitySourceName, version);
            _activities = new List<Activity>();
        }

        public ActivitySource ActivitySource { get; }

        public void Dispose()
        {
            this.ActivitySource.Dispose();
        }
    }
}
