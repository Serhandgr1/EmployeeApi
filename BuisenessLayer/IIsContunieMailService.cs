using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public interface IIsContunieMailService
    {
        ValueTask<bool> IsContinueRead(CancellationToken cancellationToken);
        ValueTask IsContinueAdd(bool item);
    }
}
