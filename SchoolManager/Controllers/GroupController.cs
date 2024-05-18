using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database.Services;
using SchoolManager.Models.ViewModels.GroupVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly ISchoolService<CourseService, GroupService, StudentService> _service;

        public GroupController(ISchoolService<CourseService, GroupService, StudentService> repository)
        {
            _service = repository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _service.Course.GetAllAsync();
            return View(new CreateGroupVM(courses));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupVM groupVM)
        {
            var courseId = groupVM.Group.CourseId ?? throw new Exception("CourseId cannot be null");

            groupVM.Group.Course = _service.Course.Get(courseId);

            if (ModelState.IsValid)
            {
                _service.Group.Add(groupVM.Group);
                return RedirectToAction("Index", "School");
            }
            return View(new CreateGroupVM(_service.Course.GetAll()));
        }

        // GET
        [HttpGet("EditGroup/{groupId}")]
        public IActionResult Edit(Guid groupId)
        {
            var group = _service.Group.Get(groupId);

            if (group == null)
                return NotFound();

            var vm = new EditGroupVM(group);
            return View(vm);
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id, EditGroupVM vm)
        {
            var recordForEdit = _service.Group.Get(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewGroup.Id = id;
            vm.NewGroup.CourseId = recordForEdit.CourseId;
            vm.NewGroup.Course = recordForEdit.Course;
            vm.NewGroup.Students = recordForEdit.Students;

            if (_service.Group.Update(vm.NewGroup))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var group = _service.Group.Get(id);
            var isGroupHasStudents = _service.Student.GetAll(group!.Id).Count > 0;

            if (group == null)
                return NotFound();

            if (isGroupHasStudents)
            {
                TempData["ErrorMessage"] = "Неможливо видалити групу, оскільки вона має студентів.";
                return RedirectToAction("Index", "School");
            }

            _service.Group.Delete(id);

            return RedirectToAction("Index", "School");
        }
    }
}
