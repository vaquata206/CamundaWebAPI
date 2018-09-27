using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Reposirory;

namespace CamundaWebAPI.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private IChiDaoRepository _chiDaoRepository;
        private ICongVanDenRepository _congVanDenRepository;
        private ICongViecPhongBanRepository _congViecPhongBanRepository;
        private ICongViecPhongBanPhieuGiaoViecRepository _congViecPhongBanPhieuGiaoViecRepository;
        private INhanVienRepository _nhanVienRepository;
        private IPhieuGiaoViecRepository _phieuGiaoViecRepository;
        private IPhongBanRepository _phongBanRepository;

        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IChiDaoRepository ChiDaoRepository {
            get { return _chiDaoRepository ?? new ChiDaoRepository(_transaction); }
        }

        public ICongVanDenRepository CongVanDenRepository {
            get { return _congVanDenRepository ?? new CongVanDenRepository(_transaction); }
        }

        public ICongViecPhongBanRepository CongViecPhongBanRepository
        {
            get { return _congViecPhongBanRepository ?? new CongViecPhongBanRepository(_transaction); }
        }

        public ICongViecPhongBanPhieuGiaoViecRepository CongViecPhongBanPhieuGiaoViecRepository
        {
            get { return _congViecPhongBanPhieuGiaoViecRepository ?? new CongViecPhongBanPhieuGiaoViecRepository(_transaction); }
        }

        public INhanVienRepository NhanVienRepository
        {
            get { return _nhanVienRepository ?? new NhanVienRepository(_transaction); }
        }

        public IPhieuGiaoViecRepository PhieuGiaoViecRepository
        {
            get { return _phieuGiaoViecRepository ?? new PhieuGiaoViecRepository(_transaction); }
        }

        public IPhongBanRepository PhongBanRepository
        {
            get { return _phongBanRepository ?? new PhongBanRepository(_transaction); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw ex;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepository();
            }
        }

        public void resetRepository()
        {
            _chiDaoRepository = null;
            _congVanDenRepository = null;
            _congViecPhongBanRepository = null;
            _congViecPhongBanPhieuGiaoViecRepository = null;
            _nhanVienRepository = null;
            _phieuGiaoViecRepository = null;
            _phongBanRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _connection = null;
                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
