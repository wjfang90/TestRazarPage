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
    public class CreateModel : PageModel
    {

        private readonly ILogger<CreateModel> _logger;

        private readonly AppDbContext _dbContext;

        /// <summary>
        /// TempData特性 可跨页面传值
        /// </summary>
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }
        public CreateModel(ILogger<CreateModel> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = $"add customer name={Customer.Name} error";
                _logger.LogError(Message);
                return Page();
            }

            try
            {
                _dbContext.Customers.Add(Customer);

                await _dbContext.SaveChangesAsync();

                Message = $"add customer name={Customer.Name} success";

                _logger.LogInformation(Message);
                
                return RedirectToPage("index");
            }
            catch (Exception)
            {
                Message = $"add customer name={Customer.Name} error";

                _logger.LogError(Message);
                return Page();
            }
        }
    }
}