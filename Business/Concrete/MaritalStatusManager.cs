using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MaritalStatusManager : IMaritalStatusService
    {
        private IMaritalStatusDal _maritalStatusDal;

        public MaritalStatusManager(IMaritalStatusDal maritalStatusDal)
        {
            _maritalStatusDal = maritalStatusDal;
        }

        public async Task<IDataResult<IList<MaritalStatus>>> GetAll()
        {
            return new SuccessDataResult<IList<MaritalStatus>>(await _maritalStatusDal.GetListAsync());
        }
    }
}
