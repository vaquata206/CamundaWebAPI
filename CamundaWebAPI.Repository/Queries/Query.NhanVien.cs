using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetDsCongViecCaNhanByCaNhanId = @"SELECT cvcn.CongViecCaNhanId as CongViecCaNhanId
								,cvcn.CaNhanId as CaNhanId
								,cvcn.PhieuGiaoViecId as PhieuGiaoViecId
								,pgv.NoiDung as NoiDung
								,pgv.TrangThai as TrangThai
								,pgv.NgayTao as NgayTao
								,pgv.NgaySua as NgaySua
								,pgv.DaXoa as DaXoa
								,pgv.ProcessInstanceId as ProcessInstanceId
							FROM [CongViecCaNhan] as cvcn
							inner join [PhieuGiaoViec] as pgv on cvcn.PhieuGiaoViecId = pgv.PhieuGiaoViecId
							where cvcn.CaNhanId = @CaNhanId
							ORDER BY pgv.NgayTao DESC";
    }
}
