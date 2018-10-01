using System;
using System.Collections.Generic;
using CamundaClient.Dto;
using CamundaClient.ViewModel;
using CamundaClient.Worker;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.Request;
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("xoaCongVan")]
    [ExternalTaskVariableRequirements("congVanDen")]
    class DeleteCongVanAdapter : ExternalTaskAdapter
    {
        #region variables
        private const string CongVanDen = "congVanDen";
        private const string Str_XoaCongVan = "xoaCongVan";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var response = new ResponseInformation() {
                StatusResponse = ResponseInformation.Status.Successed
            };

            try
            {
                var cvd = ExternalTaskHelper.GetVariable<CongVanDenRequest>(externalTask.Variables, CongVanDen);
                if (cvd != null)
                {
                    using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                    {
                        var entity = uow.CongVanDenRepository.Get(cvd.CongVanDenId.Value);
                        if (entity == null)
                        {
                            throw new Exception("The CongVanDen is null");
                        }

                        entity.DaXoa = true;
                        uow.CongVanDenRepository.Update(entity);
                        uow.Commit();
                    }
                }
                else
                {
                    response.StatusResponse = ResponseInformation.Status.Failed;
                }
            }
            catch (Exception ex)
            {
                response.StatusResponse = ResponseInformation.Status.Failed;
                response.Message = ex.ToString();
            }

            resultVariables.Add(Str_XoaCongVan, true);
            response.Variables = resultVariables;

            return response;
        }
    }
}
