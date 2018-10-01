using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/phieugiaoviec")]
    public class PhieuGiaoViecController : Controller
    {
        private CamundaEngineClient _client;
        private IUnitOfWork _uow;

        public PhieuGiaoViecController(CamundaEngineClient client, IUnitOfWork uow)
        {
            this._client = client;
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Gets() 
        {
            try
            {
                var data = await this._uow.PhieuGiaoViecRepository.GetAllAsync();

                var result = new BaseResponse<IEnumerable<PhieuGiaoViec>>()
                {
                    Message = "Gets Ok",
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
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var data = await this._uow.PhieuGiaoViecRepository.GetAsync(id);

                var result = new BaseResponse<PhieuGiaoViec>()
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

        [HttpGet, Route("{processId}/task")]
        public async Task<IActionResult> GetTaskInfo(Guid processId)
        {
            try
            {
                var taskInfo = await _client.HumanTaskService.LoadTaskAsync(processId.ToString(), null);

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
        public async Task<IActionResult> Create([FromBody] PhieuGiaoViecRequest phieuGiaoViecRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(phieuGiaoViecRequest.PhieuGiaoViec.NoiDung) || string.IsNullOrEmpty(phieuGiaoViecRequest.PhieuGiaoViec.NhanVienThucHien))
                    {
                        return BadRequest("The variables are not null or empty");
                    }

                    var jPhieuGiaoViec = JsonConvert.SerializeObject(phieuGiaoViecRequest.PhieuGiaoViec);

                    await _client.HumanTaskService.CompleteTaskAsync(phieuGiaoViecRequest.ProcessInstanceId, phieuGiaoViecRequest.TaskId, new Dictionary<string, object> {
                        { "congViecPhongBanId", phieuGiaoViecRequest.CongViecPhongBanId.ToString() },
                        { "phieuGiaoViec", jPhieuGiaoViec }
                    }, "giaoViec");

                    return Ok("Create OK");
                }
                else
                {
                    return BadRequest(this.ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut, Route("hoanthanh")]
        public async Task<IActionResult> Update([FromBody] TrangThaiRequest trangThaiRequest)
        {
            try
            {
                await _client.HumanTaskService.CompleteTaskAsync(trangThaiRequest.ProcessInstanceId, trangThaiRequest.TaskId, null, "capNhapTrangThai");

                return Ok("Update OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}