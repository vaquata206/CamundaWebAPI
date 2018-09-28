﻿using System;
using System.Collections.Generic;
using CamundaClient.Dto;
using CamundaClient.ViewModel;
using CamundaClient.Worker;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Core.Helpers;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.Request;
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("taoCongVan")]
    [ExternalTaskVariableRequirements("congVanDen")]
    class CreateCongVanAdapter : ExternalTaskAdapter
    {
        #region requests
        private const string CongVanDen = "congVanDen";
        #endregion

        #region responses
        private const string ResponseCode = "ResponseCode";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var status = ResponseInformation.Status.Successed;
            try
            {
                var cvd = ExternalTaskHelper.GetVariable<CongVanDenRequest>(externalTask.Variables, CongVanDen);
                if (cvd != null)
                {
                    cvd.CongVanDenId = Guid.NewGuid();

                    var now = DateTime.Now;

                    using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                    {
                        var entity = new CongVanDen
                        {
                            CongVanDenId = cvd.CongVanDenId.Value,
                            SoCongVan = cvd.SoCongVan,
                            TrichYeu = cvd.TrichYeu,
                            DaXoa = false,
                            TrangThai = Constants.TrangThai.InProgress,
                            NgayTao = now
                        };

                        uow.CongVanDenRepository.Add(entity);
                        uow.Commit();
                    }

                    var jCongVanDen = JsonConvert.SerializeObject(cvd);
                    resultVariables.Add(CongVanDen, jCongVanDen);
                }
                else
                {
                    status = ResponseInformation.Status.Failed;
                }
            }
            catch (Exception ex)
            {
                status = ResponseInformation.Status.Failed;
            }

            return new ResponseInformation {
                 StatusResponse = status,
                 Variables = resultVariables
            };
        }
    }
}
