using System.Text;
using Microsoft.EntityFrameworkCore;
using MicroTech.Models;

namespace MicroTech.NewFolder
{
    public class AccountService
    {
        private readonly TestDevContext _context;

        public AccountService(TestDevContext context)
        {
            _context = context;
        }

       
        public List<Account> GetTotalAccounts() 
        {
        
            return _context.Accounts.Include(c => c.childAccounts).ToList();
        }
        public decimal GetTotalBalanceOfTopLevelAccounts(Account account) 
        {
            decimal total = account.Balance ?? 0;

            
            foreach (var child in account.childAccounts)
            {
                total += GetTotalBalanceOfTopLevelAccounts(child);
            }

            return total;
        }


       

        public  List<AccountBranch> CalculateBranches(Account account, string currentBranch)
        {
            var branches = new List<AccountBranch>(); 

            if (account.childAccounts.Count == 0)
            {
                branches.Add(new AccountBranch
                {
                    Branch = currentBranch,
                    Balance = account.Balance ?? 0
                });
            }
            else
            {
                
                foreach (var child in account.childAccounts)
                {
                    var branch = $"{currentBranch} {child.AccNumber}";
                    var childBranches = CalculateBranches(child, branch);

                    branches.AddRange(childBranches);
                }
            }

            return branches;
        }


    }

    public class AccountBranch
    {
        public string Branch { get; set; }
        public decimal Balance { get; set; }
    }
}
