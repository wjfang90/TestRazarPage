using System.ComponentModel.DataAnnotations;

namespace TestRazarPage.Entity
{
    /// <summary>
    /// 客户实体
    /// </summary>
    public class Customer
    {
        
        public int Id { get; set; }

        [Required,MaxLength(30)]
        public string Name { get; set; }
        [Range(1,130)]
        public int Age { get; set; }
    }
}