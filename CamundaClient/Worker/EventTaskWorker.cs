using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CamundaClient.Service;

namespace CamundaClient.Worker
{
    public class EventTaskWorker : IDisposable
    {
        private Timer taskQueryTimer;
        private long pollingIntervalInMilliseconds = 2000;
        private ProcessInstanceService _processInstanceService;
        private IList<ExternalTaskWorker> _workers;

        public EventTaskWorker(ProcessInstanceService processInstanceService, IList<ExternalTaskWorker> workers)
        {
            this._processInstanceService = processInstanceService;
            this._workers = workers;
        }

        public void Start()
        {
            this.taskQueryTimer = new Timer(_ => CleanProcessInstances(), null, pollingIntervalInMilliseconds, Timeout.Infinite);
        }

        // Remove all events of processes that are completed.
        private void CleanProcessInstances()
        {
            var processInstances = _processInstanceService.GetInstances();
            foreach (var worker in _workers)
            {
                var taskAdapter = worker.taskWorkerInfo.TaskAdapter;
                if (taskAdapter == null || taskAdapter.Events == null)
                {
                    continue;
                }

                // Select processInstanceIds from 'taskAdapter.Events' that don't exist in 'processInstances'
                var list = from evt in taskAdapter.Events
                           join ins in processInstances on evt.Key equals ins.Id into te
                           from ins in te.DefaultIfEmpty()
                           where ins == null
                           select evt.Key;

                // Remove all events of processes that are completed.
                foreach(var id in list.ToList())
                {
                    taskAdapter.Events.Remove(id);
                }
            }

            taskQueryTimer.Change(TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds), TimeSpan.FromMilliseconds(Timeout.Infinite));
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
