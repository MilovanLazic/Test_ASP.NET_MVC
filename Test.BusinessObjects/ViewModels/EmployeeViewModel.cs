using System.ComponentModel.DataAnnotations;

namespace Test.BusinessObjects.ViewModels
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        [Display(Name = "Total time worked (hh:mm:ss)")]
        public TimeSpan? TotalTimeWorked { get; set; }
    }
}
