using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI_DotNet8.Data;
using QuizAPI_DotNet8.Entities;
using QuizAPI_DotNet8.Models;

namespace QuizAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly DataContext _context;
        public QuestionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetQuestions()
        {
            var dbQuestions = await _context.Questions
                .Select(q => new GetQuestionDto
                {
                    QuestionId = q.QuestionId,
                    Content = q.Content,
                    Choices = q.Choices.Select(c => new ChoiceDto
                    {
                        ChoiceId = c.ChoiceId,
                        Content = c.Content,
                        IsCorrect = c.IsCorrect
                    }).ToList(),
                    DateCreated = q.DateCreated,
                    Creator = new UserSummaryDto
                    {
                        UserId = q.Creator.UserId,
                        Username = q.Creator.Username,
                    }
                })
                .ToListAsync();

            return Ok(dbQuestions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var dbQuestion = await _context.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (dbQuestion is null)
            {
                return NotFound("question not found.");
            }

            return Ok(dbQuestion);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Choices.Count < 2)
            {
                return BadRequest("question must have at least 2 choices.");
            }

            if (dto.Choices.Count(c => c.IsCorrect) != 1)
            {
                return BadRequest("question must have exactly 1 correct choice.");
            }

            if (dto.Choices.Any(c => string.IsNullOrWhiteSpace(c.Content)))
            {
                return BadRequest("choice content cannot be empty.");
            }

            if (dto.Choices.Any(c => c.Content.Length > 100))
            {
                return BadRequest("choice content cannot be longer than 100 characters.");
            }

            if (string.IsNullOrWhiteSpace(dto.Content))
            {
                return BadRequest("question content cannot be empty.");
            }

            if (dto.Content.Length > 100)
            {
                return BadRequest("question content cannot be longer than 100 characters.");
            }

            // not admin
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.CreatedBy);
            if (user is null)
            {
                return BadRequest("user not found.");
            } else if (user.IsAdmin is false)
            {
                return Unauthorized("only admins can create questions.");
            }

            var dbQuestion = new Question
            {
                Content = dto.Content,
                CreatedBy = dto.CreatedBy,
                DateCreated = DateOnly.FromDateTime(DateTime.Now),
                Choices = new List<Choice>()
            };

            foreach (var choice in dto.Choices)
            {
                dbQuestion.Choices.Add(new Choice
                {
                    Content = choice.Content,
                    IsCorrect = choice.IsCorrect
                });
            }

            _context.Questions.Add(dbQuestion);
            await _context.SaveChangesAsync();

            var questionDto = new GetQuestionDto
            {
                QuestionId = dbQuestion.QuestionId,
                Content = dbQuestion.Content,
                Choices = dbQuestion.Choices.Select(c => new ChoiceDto
                {
                    ChoiceId = c.ChoiceId,
                    Content = c.Content,
                    IsCorrect = c.IsCorrect
                }).ToList(),
                DateCreated = dbQuestion.DateCreated,
                Creator = new UserSummaryDto
                {
                    UserId = dbQuestion.Creator.UserId,
                    Username = dbQuestion.Creator.Username,
                }
            };

            return CreatedAtAction(nameof(GetQuestion), new { id = dbQuestion.QuestionId }, questionDto);
        }
    }
}