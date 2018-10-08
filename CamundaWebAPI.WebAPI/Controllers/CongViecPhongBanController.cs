using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.ViewModel.Response;
using CamundaWebAPI.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/congviecphongban")]
    public class CongViecPhongBanController : Controller
    {
        private IUnitOfWork _uow;

        public CongViecPhongBanController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet, Route("phongban/{phongBanId}")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<CongViecPhongBanResponse>>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Gets(Guid phongBanId)
        {
            try
            {
                var data = await this._uow.CongViecPhongBanRepository.GetDsCongViecPhongBanByPhongBanIdAsync(phongBanId);

                var result = new BaseResponse<IEnumerable<CongViecPhongBanResponse>>()
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

        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(BaseResponse<CongViecPhongBanResponse>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var data = await this._uow.CongViecPhongBanRepository.GetCongViecPhongBanByIdAsync(id);

                var result = new BaseResponse<CongViecPhongBanResponse>()
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
    }
}