namespace PracticalTask5_RestApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public double GPA { get; set; }
    }
}
