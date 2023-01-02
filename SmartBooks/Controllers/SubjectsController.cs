using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBooks.Domain;
using SmartBooks.Domain.Entities;
using SmartBooks.Dtos;

namespace SmartBooks.Controllers;

public class SubjectController : BaseController<SubjectController>
{
    private readonly string[] Subjects = new[]
        { "Matematika", "Slovencina", "Geografia", "Fyzika", "Chemia", "Anglicky jazyk", "Dejepis" };
    
    private readonly SmartBooksContext _sbContext;
    private readonly IMapper _mapper;

    public SubjectController(SmartBooksContext sbContext, IMapper mapper)
    {
        _sbContext = sbContext;
        _mapper = mapper;

        if (!_sbContext.Subjects.Any())
        {
            var subjects = GenerateSubjects();
            _sbContext.AddRange(subjects);
            _sbContext.SaveChanges();
        }
    }


    [HttpGet("All")]
    public async Task<ActionResult<IList<SubjectDto>>> GetSubjects()
    {
        var data = await _sbContext.Subjects.ToListAsync();
        var result = _mapper.Map<List<SubjectDto>>(data);
        
        return result;
    }
    
    [HttpGet("{subjectId}")]
    public async Task<ActionResult<IList<LectureDto>>> GetSubjectLectures(long subjectId, string filter)
    {
        var dataQuery = _sbContext.Lectures.Where(l => l.Subject != null && l.Subject.Id == subjectId);
        if (!string.IsNullOrEmpty(filter))
        {
            dataQuery = dataQuery.Where(l => l.Name.Contains(filter));
        }
        var data = await dataQuery.ToListAsync();
        var result = _mapper.Map<List<LectureDto>>(data);
        
        return result;
    }
    
    
    

    private IList<Subject> GenerateSubjects()
    {
        
        var fakeLectures = new Faker<Lecture>()
            .RuleFor(l => l.Id, f => f.UniqueIndex)
            .RuleFor(l => l.Name, f => $"{f.Hacker.Adjective()} {f.Hacker.Noun()}")
            .RuleFor(l => l.TotalTasks, f=> f.Random.Int(6, 12))
            .RuleFor(l => l.FinishedTasks, (f, l) => f.Random.Int(0, l.TotalTasks));

        var fakeSubjects = new Faker<Subject>()
            .RuleFor(s => s.Id, f => f.UniqueIndex + 1)
            .RuleFor(s => s.Name, f => f.PickRandom<string>(Subjects))
            .RuleFor(s => s.Lectures, (f, s) =>
            {
                var lectures = fakeLectures.Generate(f.Random.Int(3, 12));
                foreach (var lecture in lectures)
                {
                    lecture.SubjectId = s.Id;
                    lecture.Subject = s;
                }
                return lectures;
            });

        var subjects = fakeSubjects.Generate(4);

        return subjects;
    }
    
}