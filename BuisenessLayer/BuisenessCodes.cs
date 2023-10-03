using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisenessLayer
{
    public class BuisenessCodes : IBuiseness
    {
        private IDataRepository _repository;
        private readonly IConfiguration _configuration;
        public BuisenessCodes(IConfiguration configuration)
        {
            _configuration = configuration; 
            _repository = new DataRepository(_configuration);
        }

        public async Task<int> DeleteEmployee(int id)
        {
           return await _repository.DeleteEmployee(id);
        }

        public async Task<List<FakeEmplooyeModel>> GetEmployee()
        {
            var data = await _repository.GetEmployee();
            List<FakeEmplooyeModel> fakemodel = new List<FakeEmplooyeModel>();
            foreach (EmployeeModels employee in data)
            {
                FakeEmplooyeModel fakemploye = new FakeEmplooyeModel();
                fakemploye.phone = employee.phone;
                fakemploye.adress = employee.adress;
                fakemploye.id = employee.id;
                fakemploye.name = employee.name;
                fakemploye.email = employee.email;
                fakemodel.Add(fakemploye);
            }

            return fakemodel;

        }
        public async Task BgMailService(BgMailModel bgMailModel) 
        {
          await _repository.BgMailSender(bgMailModel);
        }
        public async Task SendMailEmployee(int id, string konu, string icerik) {
            
              await  _repository.SendMailEmployee(id, konu, icerik);
        }
        public async Task SendAllMail(string konu, string icerik)
        {

           await _repository.SendAllMail(konu, icerik);

        }
        public async Task<FakeEmplooyeModel> PostEmployee(FakeEmplooyeModel emplooyeModel)
        {
            EmployeeModels employee = new EmployeeModels();
            employee.email=emplooyeModel.email;
            employee.adress=emplooyeModel.adress;
            employee.phone=emplooyeModel.phone;
            employee.name=emplooyeModel.name;
           await  _repository.PostEmployee(employee);
            return emplooyeModel;
        }

        public async Task<FakeEmplooyeModel> UpdateEmployee(FakeEmplooyeModel emplooyeModel)
        {
           EmployeeModels employeeModels = new EmployeeModels();
            employeeModels.id=emplooyeModel.id;
            employeeModels.name=emplooyeModel.name; 
            employeeModels.phone=emplooyeModel.phone;   
            employeeModels.adress=emplooyeModel.adress;
            employeeModels.email=emplooyeModel.email;   
          await  _repository.UpdateEmployee(employeeModels);
            return emplooyeModel;
        }
    }
}
