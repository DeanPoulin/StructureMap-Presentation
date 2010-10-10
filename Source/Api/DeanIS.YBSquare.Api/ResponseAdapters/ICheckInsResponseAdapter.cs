using System.Collections.Generic;
using DeanIS.YBSquare.DataAccess;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public interface ICheckInsResponseAdapter
    {
        CheckInsResponse Create(IEnumerable<UserCheckin> userCheckIns);
    }
}