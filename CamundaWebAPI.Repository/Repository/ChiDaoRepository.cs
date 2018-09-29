using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class ChiDaoRepository : RepositoryBase<ChiDao>, IChiDaoRepository
    {
        public ChiDaoRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
