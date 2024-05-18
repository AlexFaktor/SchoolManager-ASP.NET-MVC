using SchoolManager.Database.Entity;
using SchoolManager.Database.Services;

namespace SchoolManager.Resources.Interface
{
    public interface ISchoolService<TCourseService, TGroupService, TStudentService>
        where TCourseService : IEntityService<CourseRecord>
        where TGroupService : IEntityServiceExtended<GroupRecord>
        where TStudentService : IEntityServiceExtended<StudentRecord>
    {
        TCourseService Course { get; }
        TGroupService Group { get; }
        TStudentService Student { get; }
    }
}
