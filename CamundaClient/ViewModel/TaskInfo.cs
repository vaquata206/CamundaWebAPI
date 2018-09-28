using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.ViewModel
{
    public class TaskInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assignee")]
        public string Assignee { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("due")]
        public string Due { get; set; }

        [JsonProperty("followUp")]
        public string FollowUp { get; set; }

        [JsonProperty("delegationState")]
        public string DelegationState { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("executionId")]
        public string ExecutionId { get; set; }

        [JsonProperty("parentTaskId")]
        public string ParentTaskId { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("processDefinitionId")]
        public string ProcessDefinitionId { get; set; }

        [JsonProperty("processInstanceId")]
        public string ProcessInstanceId { get; set; }

        [JsonProperty("caseExecutionId")]
        public string CaseExecutionId { get; set; }

        [JsonProperty("caseDefinitionId")]
        public string CaseDefinitionId { get; set; }

        [JsonProperty("caseInstanceId")]
        public string CaseInstanceId { get; set; }

        [JsonProperty("taskDefinitionKey")]
        public string TaskDefinitionKey { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        [JsonProperty("formKey")]
        public string FormKey { get; set; }

        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
    }
}
