namespace SmartBooks.Dtos;

public record LectureDto(long Id, string Name, int TotalTasks, int FinishedTasks, decimal? Score);