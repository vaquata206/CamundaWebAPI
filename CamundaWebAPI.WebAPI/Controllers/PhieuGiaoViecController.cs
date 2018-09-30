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
    [Route("phieugiaoviec")]
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

        [HttpGet]
        public async Task<IActionResult> Gets(Guid id)
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

        [HttpPost]
        public async Task<IActionResult> Create(Guid processInstanceId, Guid taskId, [FromBody] PhieuGiaoViecRequest phieuGiaoViec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(phieuGiaoViec.NoiDung) || string.IsNullOrEmpty(phieuGiaoViec.NhanVienThucHien))
                    {
                        return BadRequest("The variables are not null or empty");
                    }

                    var jPhieuGiaoViec = JsonConvert.SerializeObject(phieuGiaoViec);

                    await _client.HumanTaskService.CompleteAsync(processInstanceId, taskId, new Dictionary<string, object> {
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

        [HttpPut]
        public async Task<IActionResult> Update(Guid processInstanceId, Guid taskId, [FromBody] int trangThai)
        {
            try
            {
                await _client.HumanTaskService.CompleteAsync(processInstanceId, taskId, new Dictionary<string, object> {
                        { "thucHienPhieuGiaoViec", trangThai }
                    }, "capNhapTrangThai");

                return Ok("Update OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}