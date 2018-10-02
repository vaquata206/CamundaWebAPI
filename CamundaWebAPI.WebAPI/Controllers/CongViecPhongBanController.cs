using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [Route("api/congviecphongban")]
    public class CongViecPhongBanController : Controller
    {
        private IUnitOfWork _uow;

        public CongViecPhongBanController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet, Route("{phongBanId}")]
        public async Task<IActionResult> Get(Guid phongBanId)
        {
            try
            {
                var data = await this._uow.CongViecPhongBanRepository.GetCongViecPhongBanByPhongBanIdAsync(phongBanId);

                var result = new BaseResponse<IEnumerable<CongViecPhongBan>>()
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