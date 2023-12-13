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
            // add choices to each question
            var dbQuestionsWithChoices = await _context.Questions
                                                .Include(q => q.Choices)
                                                .ToListAsync();

            return Ok(dbQuestionsWithChoices);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var dbQuestion = await _context.Questions.FindAsync(id);
            if (dbQuestion is null)
            {
                return NotFound("question not found.");
            }

            _context.Questions.Remove(dbQuestion);
            await _context.SaveChangesAsync();

            return Ok(dbQuestion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Choice>>> GetChoicesByQuestionId(int questionId)
        {
            var dbChoices = await _context.Choices.Where(c => c.QuestionId == questionId).ToListAsync();

            return Ok(dbChoices);
        }
    }
}
