using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class ChiDaoRepository : RepositoryBase, IChiDaoRepository
    {
        public ChiDaoRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
