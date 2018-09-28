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
        #region variables
        private const string REQ_ChiDao = "chiDao";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var response = new ResponseInformation
            {
                StatusResponse = ResponseInformation.Status.Successed
            };

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
