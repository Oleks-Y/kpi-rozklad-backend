using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rozklad.Parser.Entities
{
    public record GroupTimetable
    {
        public GroupTimetableWrapper[] InteropScheduleList { get; set; }
    }

    public record GroupTimetableWrapper
    {
        [JsonPropertyName("Код")]
        public Guid Code { get; set; }
        
        [JsonPropertyName("ТипСеместру")]
        public string SemesterType { get; set; }
        
        [JsonPropertyName("Факультет")]
        public string Faculty { get; set; }
        
        [JsonPropertyName("Курс")]
        public string Course { get; set; }
        
        [JsonPropertyName("ScheduleItems")]
        public IEnumerable<ScheduleItem> ScheduleItems { get; set; }
    }

    public record ScheduleItem
    {
        [JsonPropertyName("Код")]
        public Guid Code { get; set; }
        
        [JsonPropertyName("ДисциплінаНазва")]
        public string SubjectName { get; set; }
        
        [JsonPropertyName("ДисциплінаСкороченаНазва")]
        public string SubjectShortName { get; set; }
        
        [JsonPropertyName("ДисциплінаКодРНП")]
        public string SubjectCodeRNP { get; set; }
        
        [JsonPropertyName("День")]
        public string Day { get; set; }
        
        [JsonPropertyName("Пара")]
        public int Lesson { get; set; }
        
        [JsonPropertyName("Викладач")]
        public string Lecturer { get; set; }
        
        [JsonPropertyName("ВикладачКод")]
        public Guid LecturerId { get; set; }
        
        [JsonPropertyName("Аудиторія")]
        public string Auditory { get; set; }
        
        [JsonPropertyName("ТижденьПочатку")]
        public string WeekStart { get; set; }
        
        [JsonPropertyName("ТижденьКінця")]
        public string WeekEnd { get; set; }
        
        [JsonPropertyName("ТипЗаняття")]
        public string LessonType { get; set; }
        
        [JsonPropertyName("Група")]
        public string Group { get; set; }
    }
}