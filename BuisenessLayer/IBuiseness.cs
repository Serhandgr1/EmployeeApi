using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public interface IBuiseness
    {
        Task<List<FakeEmplooyeModel>> GetEmployee();
        Task<FakeEmplooyeModel> PostEmployee(FakeEmplooyeModel emplooyeModel);
       Task<int> DeleteEmployee(int id);
        Task<FakeEmplooyeModel> UpdateEmployee(FakeEmplooyeModel emplooyeModel);
        Task SendMailEmployee(int id, string konu, string icerik);
        Task SendAllMail(string konu, string icerik);
        Task BgMailService(BgMailModel bgMailModel);
    }
}
