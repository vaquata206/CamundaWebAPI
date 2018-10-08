using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.ViewModel.Request;
using CamundaWebAPI.ViewModel.Response;
using CamundaWebAPI.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/congvanden")]
    public class CongVanDenController : Controller
    {
        private CamundaEngineClient _camundaClient;
        private IUnitOfWork _uow;

        public CongVanDenController(CamundaEngineClient camundaClient, IUnitOfWork uow)
        {
            this._camundaClient = camundaClient;
            this._uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<CongVanDen>>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var data = await this._uow.CongVanDenRepository.GetDsCongVanDenAsync();
                
                var result = new BaseResponse<IEnumerable<CongVanDen>>()
                {
                    Message = "Gets OK",
                    Code = 200,
                    Result = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CongVanDen>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetCongVanDen(Guid id)
        {
            try
            {
                var data = await this._uow.CongVanDenRepository.GetAsync(id);

                var result = new BaseResponse<CongVanDen>()
                {
                    Message = "Get OK",
                    Code = 200,
                    Result = data
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet, Route("process/{processId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetTaskInfo(Guid processId)
        {
            try
            {
                var taskInfo = await _camundaClient.HumanTaskService.LoadTaskAsync(processId.ToString(), "Người tạo xóa công văn");

                var result = new BaseResponse<string>()
                {
                    Message = "Get OK",
                    Code = 200,
                    Result = taskInfo.Id
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(string), 500)]
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

                    var taskResponse = await _camundaClient.BpmnWorkflowService.StartProcessInstanceAsync("XuLyCongVanWithExceptionProcess", new Dictionary<string, object>() {
                        { "congVanDen", jCongVanDen }
                    }, "taoCongVan");

                    var result = new BaseResponse<string>()
                    {
                        Message = taskResponse.Content.Message,
                        Code = (int)taskResponse.Content.StatusResponse,
                        Result = taskResponse.ProcessInstanceId
                    };

                    return Ok(result);
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

        [HttpDelete, Route("task/{taskId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            try
            {
                await _camundaClient.HumanTaskService.CompleteTaskAsync(taskId, null);

                return Ok("Delete OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}