using System;
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
        #region variables
        private const string CongVanDen = "congVanDen";
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
                    response.StatusResponse = ResponseInformation.Status.Failed;
                }
            }
            catch (Exception ex)
            {
                response.StatusResponse = ResponseInformation.Status.Failed;
                response.Message = ex.ToString();
            }

            response.Variables = resultVariables;

            return response;
        }
    }
}
