using System;

namespace Rozklad.Models
{
    public record ScheduledLesson
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string SubjectString { get; set; }

        public string Day { get; set; }

        public int Number { get; set; }

        public string LecturerName { get; set; }
        public string LecturerId { get; set; }
        public string Room { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }
        public string LessonType { get; set; }

        public Guid GroupId { get;set;}
    }
}