﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.Request;
using CamundaWebAPI.ViewModel.Response;
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
        public async Task<IActionResult> GetTaskInfo(Guid processId)
        {
            try
            {
                var taskInfo = await _client.HumanTaskService.LoadTask(processId.ToString(), "Chỉ đạo");

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
        public async Task<IActionResult> Create(Guid processInstanceId, Guid taskId, [FromBody] ChiDaoRequest chiDao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(chiDao.NoiDung) || string.IsNullOrEmpty(chiDao.PhongBanThucHien))
                    {
                        return BadRequest("The variables are not null or empty");
                    }

                    var jChiDao = JsonConvert.SerializeObject(chiDao);
                    
                    await _client.HumanTaskService.CompleteAsync(processInstanceId, taskId, new Dictionary<string, object> {
                        { "chiDao", jChiDao }
                    }, "luuChiDao");

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
    }
}