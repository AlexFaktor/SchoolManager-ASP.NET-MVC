using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database.Services;
using SchoolManager.Models.ViewModels.StudentVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class StudentController : Controller
    {
        private readonly ISchoolService<CourseService, GroupService, StudentService> _service;

        public StudentController(ISchoolService<CourseService, GroupService, StudentService> repository)
        {
            _service = repository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var groups = await _service.Group.GetAllAsync();
            return View(new CreateStudentVM(groups));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentVM studentVM)
        {
            var groupId = studentVM.Student.GroupId ?? throw new Exception("GroupId cannot be null");
            studentVM.Student.Group = _service.Group.Get(groupId);

            if (ModelState.IsValid)
            {
                _service.Student.Add(studentVM.Student);
                return RedirectToAction("Index", "School");
            }

            return View(new CreateStudentVM(await _service.Group.GetAllAsync()));
        }

        // GET
        [HttpGet("EditStudent/{studentId}")]
        public IActionResult Edit(Guid studentId)
        {
            var student = _service.Student.Get(studentId);

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
            var recordForEdit = _service.Student.Get(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewStudent.Id = id;
            vm.NewStudent.GroupId = recordForEdit.GroupId;
            vm.NewStudent.Group = recordForEdit.Group;

            if (_service.Student.Update(vm.NewStudent))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var student = _service.Student.Get(id);

            if (student == null)
                return NotFound();

            _service.Student.Delete(id);

            return RedirectToAction("Index", "School");
        }
    }
}
