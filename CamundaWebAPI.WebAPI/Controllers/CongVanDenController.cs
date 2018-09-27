using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [Route("api/congvanden")]
    public class CongVanDenController : Controller
    {
        private CamundaEngineClient _client;

        public CongVanDenController()
        {
            _client = new CamundaEngineClient();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CongVanDen congVanDen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var jCongVanDen = JsonConvert.SerializeObject(congVanDen);

                    var taskResponse = await _client.BpmnWorkflowService.StartProcessInstanceAsync("XuLyCongVanProcess", null, null);

                    return Ok(Json(new { Message = "", ProcessId = "" }));
                }
                else
                {
                    return BadRequest(this.ModelState);
                }
            }
            catch
            {
                return StatusCode(500, Json(new { Message = "" }));
            }
        }
    }
}