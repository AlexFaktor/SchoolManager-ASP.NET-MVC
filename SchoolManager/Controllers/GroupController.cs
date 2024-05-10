using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly SchoolRepository _repository;

        public GroupController(ISchoolRepository repository)
        {
            _repository = (SchoolRepository)repository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _repository.CourseService.GetCoursesAsync();
            return View(new SchoolCreateGroupVM(courses));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolCreateGroupVM groupVM)
        {
            var courses = await _repository.CourseService.GetCoursesAsync();

            groupVM.Group.Course = courses.First(c => c.Id == groupVM.Group.CourseId);

            if (ModelState.IsValid)
            {
                _repository.GroupService.AddGroupRecord(groupVM.Group);
                _repository.DbSaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new SchoolCreateGroupVM(courses));
        }

        // GET
        [HttpGet("EditGroup/{groupId}")]
        public IActionResult Edit(Guid groupId)
        {
            return NotFound();
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GroupRecord newGroup)
        {
            return NotFound();
        }

        // DELETE
        public async Task<IActionResult> Delete(Guid id)
        {
            var groups = await _repository.GroupService.GetGroupsAsync();

            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var group = groups.FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            if (group.Students.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити групу, оскільки вона має студентів.";
                return RedirectToAction("Index", "School");
            }

            _repository.GroupService.DeleteGroup(id);
            _repository.DbSaveChanges();

            return RedirectToAction("Index", "School");
        }
    }
}
