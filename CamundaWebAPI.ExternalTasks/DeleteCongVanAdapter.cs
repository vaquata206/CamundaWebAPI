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

        protected override Dictionary<string, object> ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
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
                    throw new Exception("The CongVanDen is null");
                }

                resultVariables.Add("xoaCongVan", true);
                return resultVariables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
