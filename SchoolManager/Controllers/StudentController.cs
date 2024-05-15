using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.SchoolModels;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolService _repository;

        public StudentController(ISchoolService repository)
        {
            _repository = (SchoolService)repository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var groups = await _repository.GroupService.GetGroupsAsync();
            return View(new SchoolCreateStudentVM(groups));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolCreateStudentVM studentVM)
        {
            var groupId = studentVM.Student.GroupId ?? throw new Exception("GroupId cannot be null");
            studentVM.Student.Group = _repository.GroupService.GetGroup(groupId);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "School");
            }

            return View(new SchoolCreateStudentVM(await _repository.GroupService.GetGroupsAsync()));
        }

        // GET
        [HttpGet("EditStudent/{studentId}")]
        public IActionResult Edit(Guid studentId)
        {
            return NotFound();
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentRecord newStudent)
        {
            return NotFound();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var student = _repository.StudentService.GetStudent(id);

            if (student == null)
                return NotFound();

            _repository.StudentService.DeleteStudent(id);

            return RedirectToAction("Index", "School");
        }
    }
}
