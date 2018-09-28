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
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("luuChiDao")]
    [ExternalTaskVariableRequirements("chiDao")]
    class SaveChiDaoAdapter : ExternalTaskAdapter
    {
        private const string REQ_ChiDao = "chiDao";

        #region responses
        private const string ResponseCode = "ResponseCode";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var responseStatus = ResponseInformation.Status.Successed;
            try
            {
                var chiDao = ExternalTaskHelper.GetVariable<ChiDao>(externalTask.Variables, REQ_ChiDao);
                if (chiDao != null)
                {
                    var now = DateTime.Now;

                    using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                    {
                        chiDao.NgayTao = now;
                        chiDao.DaXoa = false;
                        uow.ChiDaoRepository.Add(chiDao);

                        var cvpbs = JsonConvert.DeserializeObject<CongViecPhongBan[]>(chiDao.PhongBanThucHien);
                        if (cvpbs == null || cvpbs.Length > 0)
                        {
                            foreach(var cvpb in cvpbs)
                            {
                                cvpb.CongViecPhongBanId = Guid.NewGuid();
                                cvpb.ChiDaoId = chiDao.ChiDaoId;
                                cvpb.TrangThai = Constants.TrangThai.InProgress;
                                cvpb.NgayTao = now;
                                cvpb.DaXoa = false;
                                uow.CongViecPhongBanRepository.Add(cvpb);
                            }
                        }

                        uow.Commit();
                    }
                }
                else
                {
                    responseStatus = ResponseInformation.Status.Failed;
                }
            }
            catch (Exception ex)
            {
                responseStatus = ResponseInformation.Status.Failed;
            }

            return new ResponseInformation
            {
                StatusResponse = responseStatus,
                Variables = resultVariables
            };
        }
    }
}
