using System;
using System.Collections.Generic;
using CamundaClient.Dto;
using CamundaClient.Worker;
using CamundaWebAPI.Core.Common;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.ExternalTasks
{
    [ExternalTaskTopic("calculate")]
    [ExternalTaskVariableRequirements("x", "y")]
    class CalculationAdapter : ExternalTaskAdapter
    {
        protected override void ExecuteTask(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            long result = 0;
            try
            {
                //long x = Convert.ToInt64(externalTask.Variables["x"].Value);
                //long y = Convert.ToInt64(externalTask.Variables["y"].Value);
                //result = x + y;
                using (IUnitOfWork uow = new UnitOfWork(ConfigSettings.ConnectionString))
                {
                    var nhanVien = new   NhanVien
                    {
                        TenNhanVien = "aa",
                        NhanVienId = Guid.NewGuid()
                    };

                    uow.NhanVienRepository.Add(nhanVien);
                    uow.Commit();
                }
            }
            catch (Exception){
            }
            resultVariables.Add("result", result);
        }
    }
}
