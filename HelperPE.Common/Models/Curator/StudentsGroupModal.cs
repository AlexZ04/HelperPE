namespace HelperPE.Common.Models.Curator
{
    public class StudentsGroupModal
    {
        public required FacultyDTO Faculty { get; set; }
        public required string Group { get; set; }
        public List<StudentProfileShortModel> Students { get; set; } = 
            new List<StudentProfileShortModel>();
    }
}
