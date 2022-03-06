namespace LibraryManager.Domain.Dtos.Teacher
{
    public class GetTeacherDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Reference { get; set; }
        public string Role { get; set; }
    }
}
