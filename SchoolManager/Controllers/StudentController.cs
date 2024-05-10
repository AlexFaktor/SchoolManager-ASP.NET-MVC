using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolRepository _repository;

        public StudentController(ISchoolRepository repository)
        {
            _repository = (SchoolRepository)repository;
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
            var groups = await _repository.GroupService.GetGroupsAsync();
            studentVM.Student.Group = groups.First(c => c.Id == studentVM.Student.GroupId);

            if (ModelState.IsValid)
            {
                _repository.StudentService.AddStudentRecord(studentVM.Student);
                _repository.DbSaveChangesAsync();
                return RedirectToAction("Index", "School");
            }

            return View(new SchoolCreateStudentVM(groups));
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
        public async Task<IActionResult> Delete(Guid id)
        {
            var students = await _repository.StudentService.GetStudentsAsync();


            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _repository.StudentService.DeleteStudent(id);
            _repository.DbSaveChanges();

            return RedirectToAction("Index", "School");
        }
    }
}
