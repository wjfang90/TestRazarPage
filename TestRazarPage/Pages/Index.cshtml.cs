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

        /// <summary>
        /// 查询条件
        /// 出于安全原因，必须选择绑定 GET 请求数据以对模型属性进行分页
        /// </summary>
        [BindProperty(SupportsGet =true)]
        public string SearchText { get; set; }

        public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            if(string.IsNullOrEmpty(SearchText))
            {
                Customers = _dbContext.Customers.ToList();
            }
            else
            {
                Customers = _dbContext.Customers.Where(t=>t.Name.Contains(SearchText.Trim())).ToList();
            }
        }

        /// <summary>
        /// 注意，asp-page-handler 属性值中省略了页处理程序方法名称的 On<Verb> 前缀。 如果方法是异步的，也省略 Async 后缀。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetDelAsync(int id)
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

                return Page();
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
