using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroTech.Models;
using MicroTech.NewFolder;
namespace MicroTech.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
      
        private readonly AccountService _accountService;

        public AccountController(TestDevContext context, AccountService accountService)
        {
            
            _accountService = accountService;
        }

        [HttpGet("TopLevelAccounts")]
        public IActionResult GetTopLevelAccounts()
        {
            var result1 = _accountService.GetTotalAccounts();

           var result=result1.Where(a=>a.AccParent==null).Select(a => new
                {
                    TopLevelAccount = a.AccNumber,
                    TotalBalance =  _accountService.GetTotalBalanceOfTopLevelAccounts(a),
                    Branches=_accountService.CalculateBranches(a,a.AccNumber)
           })
                .ToList();

            return Ok(result);
        }

        

       

    }

}
