using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestRazarPage.Entity;

namespace TestRazarPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly AppDbContext _dbContext;

        /// <summary>
        /// TempData特性 可跨页面传值
        /// </summary>
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public List<Customer> Customers { get; set; }
        public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            Customers = _dbContext.Customers.ToList();
        }

        public async Task<IActionResult> OnPostDelAsync(int id)
        {
            var customer =  await _dbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                Message = $"customer id={id} was not found";
                _logger.LogInformation(Message);
                return Page();
            }

            try
            {
                _dbContext.Customers.Remove(customer);

                await _dbContext.SaveChangesAsync();

                Message = $"customer id={customer.Id} was deleted";

                _logger.LogInformation(Message);

                return RedirectToPage("index");
            }
            catch (Exception)
            {
                Message = $" delete customer id={id} error";
                _logger.LogInformation(Message);
                return Page();
            }
           
        }

    }
}
