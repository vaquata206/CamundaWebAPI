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
    [ExternalTaskTopic("capNhapTrangThai")]
    class CompletingAdapter : ExternalTaskAdapter
    {
        #region variables
        private const string Str_CongVanDen = "congVanDen";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var response = new ResponseInformation
            {
                StatusResponse = ResponseInformation.Status.Successed
            };

            try
            {
                var congVanDen = ExternalTaskHelper.GetVariable<CongVanDenRequest>(externalTask.Variables, Str_CongVanDen);
                if (congVanDen != null)
                {
                    using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                    {
                        var entityCvd = uow.CongVanDenRepository.Get(congVanDen.CongVanDenId);

                        entityCvd.TrangThai = Constants.TrangThai.Done;
                        uow.CongVanDenRepository.Update(entityCvd);

                        var entityChiDao = uow.ChiDaoRepository.GetChiDaoByCongVanDenIdAsync(entityCvd.CongVanDenId).Result;
                        var listCvpb = uow.CongViecPhongBanRepository.GetCongViecPhongBanByChiDaoIdAsync(entityChiDao.ChiDaoId).Result;
                        foreach(var cvpb in listCvpb)
                        {
                            cvpb.TrangThai = Constants.TrangThai.Done;
                            uow.CongViecPhongBanRepository.Update(cvpb);

                            var phieuGiaoViec = uow.PhieuGiaoViecRepository.GetByCongViecPhongBan(cvpb.CongViecPhongBanId).Result;
                            phieuGiaoViec.TrangThai = Constants.TrangThai.Done;
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
