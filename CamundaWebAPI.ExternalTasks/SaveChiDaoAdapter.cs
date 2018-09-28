using System;
using System.Collections.Generic;
using CamundaClient.Dto;
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
        #region requests
        private const string REQ_ChiDao = "chiDao";
        #endregion

        #region responses
        private const string ResponseCode = "ResponseCode";
        #endregion

        protected override void ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var responseCode = Constants.ResponseCode.Created;
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
                    responseCode = Constants.ResponseCode.Failed;
                }
            }
            catch (Exception ex)
            {
                responseCode = Constants.ResponseCode.Failed;
            }

            resultVariables.Add(ResponseCode, responseCode);
        }
    }
}
