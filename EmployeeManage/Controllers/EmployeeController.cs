using BuisenessLayer;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IBuiseness _buiseness;
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _buiseness = new BuisenessCodes(configuration);
            _configuration = configuration;

        }
        [HttpGet("get-all-emp")]
        public async Task<List<FakeEmplooyeModel>> GetEmployee() 
        {
            return await _buiseness.GetEmployee();   
        }
        [HttpPost("post-emp")]
        public async Task<FakeEmplooyeModel>PostEmployee(FakeEmplooyeModel emplooyeModel)
        {
            return await _buiseness.PostEmployee(emplooyeModel);

        }
        [HttpPost("send-mail-emp")]
        public async Task SendMailEmployee(int id , string konu , string icerik)
        {
           await _buiseness.SendMailEmployee(id , konu , icerik);

        }
        [HttpPost("send-mail-all")]
        public async Task SendAllMail(string konu, string icerik)
        {
            await _buiseness.SendAllMail(konu, icerik);

        }
        [HttpPut("update-emp")]
        public async Task<FakeEmplooyeModel> UpdateEmployee(FakeEmplooyeModel emplooyeModel) 
        {
            return await _buiseness.UpdateEmployee(emplooyeModel);
        }
        [HttpDelete("delete-emp")]
        public async Task<int> DeleteEmployee(int id)
        {
            return await _buiseness.DeleteEmployee(id);

        }
    }
}
