using CamundaClient.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamundaClient.Worker
{
    public abstract class ExternalTaskAdapter
    {
        public IDictionary<string, List<Action<IDictionary<string, object>>>> Events { get; set; }
        public IDictionary<string, List<TaskCompletionSource<object>>> CompletionSources { get; set; }

        public ExternalTaskAdapter()
        {
            Events = new Dictionary<string, List<Action<IDictionary<string, object>>>>();
            CompletionSources = new Dictionary<string, List<TaskCompletionSource<object>>>();
        }

        protected abstract void ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables);
        
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            this.ExecuteTask(externalTask, ref resultVariables);

            var data = resultVariables.ToDictionary(entry => entry.Key, entry => entry.Value);
            if (Events.ContainsKey(externalTask.ProcessInstanceId))
            {
                var list = Events[externalTask.ProcessInstanceId];
                list.ForEach(x => x(data));
            }

            if (CompletionSources.ContainsKey(externalTask.ProcessInstanceId))
            {
                var list = CompletionSources[externalTask.ProcessInstanceId];
                list.ForEach(x => x.SetResult(data));
                CompletionSources.Remove(externalTask.ProcessInstanceId);
            }
        }
    }

}
