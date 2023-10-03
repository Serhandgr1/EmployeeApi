using DataAccessLayer.Models;
using EmailService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly IConfiguration _configuration;
        public DataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> DeleteEmployee(int id)
        {
           using ( var db = new DataContext(_configuration)) 
            {
                EmployeeModels employeeModels = new EmployeeModels();
                employeeModels.id = id;
                db.Emplooye.Remove(employeeModels);
               await db.SaveChangesAsync();
               return id;
            }


        }
        public async Task BgMailSender(BgMailModel bgMailModel) 
        {
            using (var db = new DataContext(_configuration))
            { 
               await db.SendMail.AddAsync(bgMailModel);
               await db.SaveChangesAsync();
            }

        }
        public async Task SendMailEmployee(int id, string konu, string icerik) 
        {
            using (var db = new DataContext(_configuration))
            {
              var emp= await db.Emplooye.FindAsync(id);
              var senmail = emp.email;
                MailSender mailSender = new MailSender();
                mailSender.SendMail(senmail, konu, icerik);
            }
        }
        public async Task SendAllMail(string konu, string icerik) 
        {
            using (var db = new DataContext(_configuration)) 
            {
                List<EmployeeModels> employeeModels = new List<EmployeeModels>();
                var data = await db.Emplooye.ToListAsync();
                foreach (EmployeeModels employee in data)
                {
                    string mail = employee.email;   
                    MailSender mailSender = new MailSender();
                    mailSender.SendMail(mail, konu, icerik);    
                }
            
            }

        }
        public async Task<List<EmployeeModels>> GetEmployee()
        {
            using (var db = new DataContext(_configuration)) 
            { 
                
               return await db.Emplooye.ToListAsync(); 
                
            }
                
        }

        public async Task<EmployeeModels> PostEmployee(EmployeeModels emplooyeModel)
        {
           using (var db = new DataContext(_configuration))
            {
             await db.Emplooye.AddAsync(emplooyeModel);
             await db.SaveChangesAsync();
            }
           return emplooyeModel;
        }

        public async Task UpdateEmployee(EmployeeModels emplooyeModel)
        {
           using(var db = new DataContext(_configuration)) 
            {
                    db.Emplooye.Update(emplooyeModel);
                    await db.SaveChangesAsync();
            }
        }
    }
}
