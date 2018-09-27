using System;
using System.Collections.Generic;
using CamundaClient.Dto;
using CamundaClient.Worker;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Core.Helpers;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

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

        protected override void ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var responseCode = Constants.ResponseCode.Created;
            try
            {
                var a = "{SoCongVan: 'aaaa'; TrichYeu: 'bbbb'}";
                var cvd = ExternalTaskHelper.GetVariable<CongVanDen>(externalTask.Variables, CongVanDen);
                var now = DateTime.Now;

                using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                {
                    cvd.NgayTao = now;
                    cvd.TrangThai = Constants.TrangThai.InProgress;
                    cvd.CongVanDenId = Guid.NewGuid();

                    uow.CongVanDenRepository.Add(cvd);
                    uow.Commit();
                }
            }
            catch (Exception)
            {
                responseCode = Constants.ResponseCode.Failed;
            }

            resultVariables.Add(ResponseCode, responseCode);
        }
    }
}
