using System;
using System.Collections.Generic;
using CamundaClient.Dto;
using CamundaClient.ViewModel;
using CamundaClient.Worker;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Core.Helpers;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.ViewModel;
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("luuChiDao")]
    [ExternalTaskVariableRequirements("chiDao")]
    class SaveChiDaoAdapter : ExternalTaskAdapter
    {
        #region variables
        private const string Str_ChiDao = "chiDao";
        private const string Str_XoaCongVan = "xoaCongVan";
        #endregion

        protected override Dictionary<string, object> ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            try
            {
                var chiDao = ExternalTaskHelper.GetVariable<ChiDaoViewModel>(externalTask.Variables, Str_ChiDao);
                if (chiDao != null)
                {
                    var now = DateTime.Now;
                    chiDao.ChiDaoId = Guid.NewGuid();

                    using (var uow = new UnitOfWork(ConfigSettings.ConnectionString))
                    {
                        var chiDaoEntity = new ChiDao
                        {
                            ChiDaoId = chiDao.ChiDaoId.Value,
                            CongVanDenId = chiDao.CongVanDenId,
                            NguoiChiDaoId = chiDao.NguoiChiDaoId,
                            NgayTao = now,
                            NoiDung = chiDao.NoiDung,
                            PhongBanThucHien = chiDao.PhongBanThucHien,
                            DaXoa = false
                        };

                        uow.ChiDaoRepository.Add(chiDaoEntity);

                        var PhongBanIds = JsonConvert.DeserializeObject<string[]>(chiDaoEntity.PhongBanThucHien);

                        if (PhongBanIds == null || PhongBanIds.Length > 0)
                        {
                            foreach(var phongBanId in PhongBanIds)
                            {
                                var cvpbEntity = new CongViecPhongBan
                                {
                                    CongViecPhongBanId = Guid.NewGuid(),
                                    ChiDaoId = chiDaoEntity.ChiDaoId,
                                    TrangThai = Constants.TrangThai.InProgress,
                                    PhongBanId = new Guid(phongBanId),
                                    NgayTao = now,
                                    DaXoa = false
                                };
                                
                                uow.CongViecPhongBanRepository.Add(cvpbEntity);
                            }
                        }

                        uow.Commit();
                    }
                }
                else
                {
                    throw new Exception("The ChiDao is null");
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
