using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CamundaClient.Dto;

namespace CamundaClient.Worker
{
    public class HumanTaskWorker : IDisposable
    {
        private Timer taskQueryTimer;
        private long pollingIntervalInMilliseconds = 50;
        private Action _action;

        public HumanTaskWorker(Action action)
        {
            this._action = action;
        }

        public void Start()
        {
            this.taskQueryTimer = new Timer(_ => DoPolling(), null, pollingIntervalInMilliseconds, Timeout.Infinite);
        }

        private void DoPolling()
        {
            this._action();
            // schedule next run
            taskQueryTimer.Change(TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(Timeout.Infinite));
        }

        public void Stop()
        {
            this.taskQueryTimer.Dispose();
        }

        public void Dispose()
        {
            if (this.taskQueryTimer != null)
            {
                this.taskQueryTimer.Dispose();
            }
        }
    }
}
