using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CamundaWebAPI.WebAPI.Controllers
{
    [Route("api/chidao")]
    public class ChiDaoController : Controller
    {
        private CamundaEngineClient _client;
        private IUnitOfWork _uow;
        
        public ChiDaoController(CamundaEngineClient client, IUnitOfWork uow)
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
                    return BadRequest(Json(new { Message = "The variables are not null or empty" }));
                }

                var congVan = this._uow.CongVanDenRepository.GetAsync<CongVanDen>(congVanId);

                return Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string taskId, [FromBody] ChiDaoRequest chiDao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(chiDao.NoiDung) || string.IsNullOrEmpty(chiDao.PhongBanThucHien))
                    {
                        return BadRequest(Json(new { Message = "The variables are not null or empty" }));
                    }

                    var jChiDao = JsonConvert.SerializeObject(chiDao);

                    await _client.BpmnWorkflowService.CompleteTask(taskId, new Dictionary<string, object> {
                        { "variables", jChiDao}
                    });

                    return Ok(Json(new { Message = "Completed chi dao user task!" }));
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
    }
}