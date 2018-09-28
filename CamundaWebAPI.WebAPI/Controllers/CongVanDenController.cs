using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.ViewModel.Request;
using CamundaWebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [Route("api/congvanden")]
    public class CongVanDenController : Controller
    {
        private CamundaEngineClient _client;
        private IUnitOfWork _uow;

        public CongVanDenController(CamundaEngineClient client, IUnitOfWork uow)
        {
            this._client = client;
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string congVanId)
        {
            try
            {
                if (string.IsNullOrEmpty(congVanId))
                {
                    return BadRequest("The variables are not null or empty");
                }

                var congVan = await this._uow.CongVanDenRepository.GetAsync<CongVanDen>(congVanId);

                var result = new BaseResponse<CongVanDen>()
                {
                    Message = "Get cong van",
                    Code = 200,
                    Result = congVan
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
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

                    var taskResponse = await _client.BpmnWorkflowService.StartProcessInstanceAsync("XuLyCongVanProcess", new Dictionary<string, object>() {
                        { "congVanDen", jCongVanDen }
                    }, "taoCongVan");

                    var variable = (Dictionary<String, object>) taskResponse.Variables;
                    
                    return Ok(Json(new { ProcessId = taskResponse.ProcessInstanceId, Message = variable["ResponseCode"].ToString() }));
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