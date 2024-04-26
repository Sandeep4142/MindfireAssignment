using CourseService.Models;

namespace CourseService
{
    public interface ICourseDA
    {
        bool AddCourse(Course course);
        bool DeleteCourse(int id);
        List<Course> GetAllCourse();
        Course GetCourseByID(int id);
        bool UpdateCourseCoordinator(int id, string name);
    }
}