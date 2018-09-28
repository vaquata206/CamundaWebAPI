using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.ViewModel.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [Route("api/congvanden")]
    public class CongVanDenController : Controller
    {
        private CamundaEngineClient _camundaClient;

        public CongVanDenController(CamundaEngineClient camundaClient)
        {
            this._camundaClient = camundaClient;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CongVanDenRequest congVanDen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(congVanDen.SoCongVan) || string.IsNullOrEmpty(congVanDen.TrichYeu))
                    {
                        return BadRequest(Json(new { Message = "The variables are not null or empty" }));
                    }

                    var jCongVanDen = JsonConvert.SerializeObject(congVanDen);

                    var taskResponse = await _camundaClient.BpmnWorkflowService.StartProcessInstanceAsync("XuLyCongVanProcess", new Dictionary<string, object>() {
                        { "congVanDen", jCongVanDen }
                    }, "taoCongVan");

                    return Ok(Json(new { ProcessId = taskResponse.ProcessInstanceId, StatusCode = (int)taskResponse.Content.StatusResponse, Message = taskResponse.Content.Message }));
                }
                else
                {
                    return BadRequest(this.ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Json(new { Message = ex.ToString() }));
            }
        }
    }
}