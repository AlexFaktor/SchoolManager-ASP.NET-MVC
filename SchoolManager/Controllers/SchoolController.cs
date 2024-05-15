using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolService _repository;

        public SchoolController(ISchoolService repository)
        {
            _repository = (SchoolService)repository;
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
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _repository.CourseService.GetCoursesAsync(),
                Groups = _repository.GroupService.GetGroups(courseId),

                CourseId = courseId
            };

            return View(schoolIndexVM);
        }

        // GET: SchoolController Group/
        [HttpGet("Index/{courseId}/{groupId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId)
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _repository.CourseService.GetCoursesAsync(),
                Groups = _repository.GroupService.GetGroups(courseId),
                Students = _repository.StudentService.GetStudents(groupId),

                CourseId = courseId,
                GroupId = groupId
            };

            return View(schoolIndexVM);
        }

        // GET: SchoolController Student/
        [HttpGet("Index/{courseId}/{groupId}/{studentId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId, Guid studentId)
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _repository.CourseService.GetCoursesAsync(),
                Groups = _repository.GroupService.GetGroups(courseId),
                Students = _repository.StudentService.GetStudents(groupId),

                CourseId = courseId,
                GroupId = groupId,
                StudentId = studentId
            };

            return View(schoolIndexVM);
        }
    }
}
