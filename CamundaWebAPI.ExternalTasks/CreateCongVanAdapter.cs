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

        protected override Dictionary<string, object> ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            try
            {
                //throw new Exception("test");
                
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
                            NgayTao = now,
                            ProcessInstanceId = new Guid(externalTask.ProcessInstanceId)
                        };

                        uow.CongVanDenRepository.Add(entity);
                        uow.Commit();
                    }

                    var jCongVanDen = JsonConvert.SerializeObject(cvd);
                    resultVariables.Add(CongVanDen, jCongVanDen);
                }
                else
                {
                    throw new Exception("The CongVanDen is Null");
                }

                return resultVariables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
