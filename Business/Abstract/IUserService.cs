using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<IList<User>>> GetAll();
        Task<IResult> Add(User user);
        Task<IDataResult<User>> GetByEmail(string email);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
    }
}
