using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public interface IBackGroundService<T>
    {
        ValueTask AddMail(T mailsender);
        ValueTask<T> SenMail(CancellationToken cancellationToken);
    }
}
