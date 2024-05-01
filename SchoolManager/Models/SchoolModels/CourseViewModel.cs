using System.Text.RegularExpressions;

namespace SchoolManager.Models.SchoolModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<GroupViewModel> Groups { get; set; }
    }
}
