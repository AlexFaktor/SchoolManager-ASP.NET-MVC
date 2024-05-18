using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Database.Services;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ISchoolService<CourseService, GroupService, StudentService> _service;

        public SchoolController(ISchoolService<CourseService, GroupService, StudentService> repository)
        {
            _service = repository;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _service.Course.GetAllAsync()
            };

            return View(schoolIndexVM);
        }

        [HttpGet("Index/{courseId}")]
        public async Task<IActionResult> Index(Guid courseId)
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _service.Course.GetAllAsync(),
                Groups = _service.Group.GetAll(courseId),

                CourseId = courseId
            };

            return View(schoolIndexVM);
        }

        [HttpGet("Index/{courseId}/{groupId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId)
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _service.Course.GetAllAsync(),
                Groups = _service.Group.GetAll(courseId),
                Students = _service.Student.GetAll(groupId),

                CourseId = courseId,
                GroupId = groupId
            };

            return View(schoolIndexVM);
        }

        [HttpGet("Index/{courseId}/{groupId}/{studentId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId, Guid studentId)
        {
            var schoolIndexVM = new SchoolIndexVM
            {
                Courses = await _service.Course.GetAllAsync(),
                Groups = _service.Group.GetAll(courseId),
                Students = _service.Student.GetAll(groupId),

                CourseId = courseId,
                GroupId = groupId,
                StudentId = studentId
            };

            return View(schoolIndexVM);
        }
    }
}
