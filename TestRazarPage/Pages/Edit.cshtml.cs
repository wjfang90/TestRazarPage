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
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        private readonly AppDbContext _dbContext;

        /// <summary>
        /// TempData特性 可跨页面传值
        /// </summary>
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }
        public EditModel(ILogger<EditModel> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer =  await _dbContext.Customers.FindAsync(id);

            if (Customer == null)
            {
                Message = $"modify customer name={Customer.Name} not found";
                _logger.LogError(Message);
                return RedirectToPage("index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = $"modify customer name={Customer.Name} isvalid";
                _logger.LogError(Message);
                return Page();
            }

            try
            {

                _dbContext.Update(Customer);

                await _dbContext.SaveChangesAsync();

                Message = $"modify customer name={Customer.Name} success";

                _logger.LogInformation(Message);

                return RedirectToPage("index");
            }
            catch (Exception)
            {
                Message = $"modify customer name={Customer.Name} error";

                _logger.LogError(Message);
                return Page();
            }
        }
    }
}