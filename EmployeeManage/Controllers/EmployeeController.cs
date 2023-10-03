using BuisenessLayer;
using DataAccessLayer.Models;
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
        private readonly IBackGroundService<BgMailModel> _backGroundService;
        private readonly IIsContunieMailService _isContunieMailService;
        public EmployeeController(IConfiguration configuration, IBackGroundService<BgMailModel> backGroundService , IIsContunieMailService isContunieMailService)
        {
            _isContunieMailService = isContunieMailService;
            _buiseness = new BuisenessCodes(configuration);
            _configuration = configuration;
            _backGroundService = backGroundService;
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
        [HttpPost("try-mail-service")]
        public async ValueTask TryMailService(bool item) 
        {
            await _isContunieMailService.IsContinueAdd(item);
        }
        [HttpPost("send-mail-emp")]
        public async ValueTask SendMailEmployee(int id , string konu , string icerik)
        {
    //     await _buiseness.SendMailEmployee(id , konu , icerik);
            BgMailModel bgMailModel = new BgMailModel();    
            bgMailModel.UserId = id;
            bgMailModel.Icerik = icerik;
            bgMailModel.Konu = konu;
         await _backGroundService.AddMail(bgMailModel);

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
