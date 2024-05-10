using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolRepository _repository;

        public SchoolController(ISchoolRepository repository)
        {
            _repository = (SchoolRepository)repository;
        }

        // GET: SchoolController
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _repository.CourseService.GetCoursesAsync()
            };

            return View(schoolIndexVM);
        }

        // GET: SchoolController Course/
        [HttpGet("Index/{courseId}")]
        public async Task<IActionResult> Index(Guid courseId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            var course = await _repository.CourseService.GetCoursesAsync();
            var groups = await _repository.GroupService.GetGroupsAsync();

            schoolIndexVM.Courses = course;
            schoolIndexVM.Groups = groups.Where(g => g.CourseId == courseId).ToList();

            schoolIndexVM.CourseId = courseId;

            return View(schoolIndexVM);
        }

        // GET: SchoolController Group/
        [HttpGet("Index/{courseId}/{groupId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            var courses = await _repository.CourseService.GetCoursesAsync();
            var groups = await _repository.GroupService.GetGroupsAsync();
            var students = await _repository.StudentService.GetStudentsAsync();

            schoolIndexVM.Courses = courses;
            schoolIndexVM.Groups = groups.Where(g => g.CourseId == courseId).ToList();
            schoolIndexVM.Students = students.Where(s => s.GroupId == groupId).ToList();

            schoolIndexVM.CourseId = courseId;
            schoolIndexVM.GroupId = groupId;

            return View(schoolIndexVM);
        }

        // GET: SchoolController Student/
        [HttpGet("Index/{courseId}/{groupId}/{studentId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId, Guid studentId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            var courses = await _repository.CourseService.GetCoursesAsync();
            var groups = await _repository.GroupService.GetGroupsAsync();
            var students = await _repository.StudentService.GetStudentsAsync();

            schoolIndexVM.Courses = courses;
            schoolIndexVM.Groups = groups.Where(g => g.CourseId == courseId).ToList();
            schoolIndexVM.Students = students.Where(s => s.GroupId == groupId).ToList();

            schoolIndexVM.CourseId = courseId;
            schoolIndexVM.GroupId = groupId;
            schoolIndexVM.StudentId = studentId;

            return View(schoolIndexVM);
        }
    }
}
