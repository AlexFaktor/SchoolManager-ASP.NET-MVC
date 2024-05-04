using System.Text.RegularExpressions;

namespace SchoolManager.Models.SchoolModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<GroupViewModel> Groups { get; set; }
    }
}
