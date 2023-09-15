using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataRepository
    {
        Task<List<EmployeeModels>> GetEmployee();
        Task<EmployeeModels> PostEmployee(EmployeeModels emplooyeModel);
        Task<int> DeleteEmployee(int id);
        Task UpdateEmployee(EmployeeModels emplooyeModel);
        Task SendMailEmployee(int id, string konu, string icerik);
        Task SendAllMail(string konu, string icerik);
    }
}
