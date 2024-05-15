using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.SchoolModels;
using SchoolManager.Models.ViewModels.CourseVM;
using SchoolManager.Models.ViewModels.GroupVM;
using SchoolManager.Models.ViewModels.StudentVM;
using SchoolManager.Resources.Interface;
using System.Text.RegularExpressions;

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
            return View(new CreateStudentVM(groups));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentVM studentVM)
        {
            var groupId = studentVM.Student.GroupId ?? throw new Exception("GroupId cannot be null");
            studentVM.Student.Group = _repository.GroupService.GetGroup(groupId);

            if (ModelState.IsValid)
            {
                _repository.StudentService.AddStudentRecord(studentVM.Student);
                return RedirectToAction("Index", "School");
            }

            return View(new CreateStudentVM(await _repository.GroupService.GetGroupsAsync()));
        }

        // GET
        [HttpGet("EditStudent/{studentId}")]
        public IActionResult Edit(Guid studentId)
        {
            var student = _repository.StudentService.GetStudent(studentId);

            if (student == null)
                return NotFound();

            var vm = new EditStudentVM(student);
            return View(vm);
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id, EditStudentVM vm)
        {
            var recordForEdit = _repository.StudentService.GetStudent(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewStudent.Id = id;
            vm.NewStudent.GroupId = recordForEdit.GroupId;
            vm.NewStudent.Group = recordForEdit.Group;

            if (_repository.StudentService.UpdateStudent(vm.NewStudent))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
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
