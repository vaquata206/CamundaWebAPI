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
    [Route("api/nhanvien")]
    public class NhanVienController : Controller
    {
        private IUnitOfWork _uow;

        public NhanVienController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                var data = await this._uow.NhanVienRepository.GetAllAsync();

                var result = new BaseResponse<IEnumerable<NhanVien>>()
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