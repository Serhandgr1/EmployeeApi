using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public interface IIsContunieMailService
    {
        ValueTask<BackGroundServiceControllerModel> IsContinueRead(CancellationToken cancellationToken);
        ValueTask IsContinueAdd(BackGroundServiceControllerModel item);
        ValueTask IsWorkingAdd(bool item);
        ValueTask<bool> IsWorkingRead(CancellationToken cancellationToken);
    }
}
