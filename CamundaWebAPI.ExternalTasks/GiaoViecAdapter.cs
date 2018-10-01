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
using CamundaWebAPI.ViewModel.ViewModel;
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("giaoViec")]
    [ExternalTaskVariableRequirements("phieuGiaoViec", "congViecPhongBanId")]
    class GiaoViecAdapter : ExternalTaskAdapter
    {
        #region variables
        private const string Str_PhieuGiaoViec = "phieuGiaoViec";
        private const string Str_congViecPhongBanId = "congViecPhongBanId";
        #endregion

        protected override ResponseInformation ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var response = new ResponseInformation() {
                StatusResponse = ResponseInformation.Status.Successed
            };

            try
            {
                var pgv = ExternalTaskHelper.GetVariable<PhieuGiaoViecViewModel>(externalTask.Variables, Str_PhieuGiaoViec);
                var pbId = Convert.ToString(externalTask.Variables[Str_congViecPhongBanId].Value);

                var now = DateTime.Now;

                using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                {
                    var entityPhieuGiaoViec = new PhieuGiaoViec
                    {
                        PhieuGiaoViecId = Guid.NewGuid(),
                        NguoiGiaoId = pgv.NguoiGiaoId,
                        NoiDung = pgv.NoiDung,
                        NhanVienThucHien = pgv.NhanVienThucHien,
                        TrangThai = Constants.TrangThai.InProgress,
                        NgayTao = now,
                        DaXoa = false
                    };
                    uow.PhieuGiaoViecRepository.Add(entityPhieuGiaoViec);

                    var entityCvpb_pgv = new CongViecPhongBan_PhieuGiaoViec
                    {
                        Id = Guid.NewGuid(),
                        CongViecPhongBanId = new Guid(pbId),
                        PhieuGiaoViecId = entityPhieuGiaoViec.PhieuGiaoViecId,
                        Loai = 0,
                        NgayTao = now,
                        DaXoa = false
                    };
                    uow.CongViecPhongBanPhieuGiaoViecRepository.Add(entityCvpb_pgv);

                    var nhanvienIds = JsonConvert.DeserializeObject<string[]>(entityPhieuGiaoViec.NhanVienThucHien);
                    if (nhanvienIds != null && nhanvienIds.Length > 0)
                    {
                        foreach (var nhanvienId in nhanvienIds)
                        {
                            var entityCvcn = new CongViecCaNhan
                            {
                                CongViecCaNhanId = Guid.NewGuid(),
                                CaNhanId = new Guid(nhanvienId),
                                PhieuGiaoViecId = entityPhieuGiaoViec.PhieuGiaoViecId,
                                NoiDungThucHien = "",
                                TrangThai = Constants.TrangThai.InProgress,
                                NgayTao = now,
                                DaXoa = false
                            };

                            uow.CongViecCaNhanRepository.Add(entityCvcn);
                        }
                    }

                    uow.Commit();
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
