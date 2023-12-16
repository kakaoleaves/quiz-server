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

            if (dto.Choices.Count != 4)
            {
                return BadRequest("question must have exactly 4 choices.");
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

        [HttpPut("{id}")]
        public async Task<ActionResult<Question>> PutQuestion(int id, PutQuestionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Choices.Count != 4)
            {
                return BadRequest("question must have exactly 4 choices.");
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

            var dbQuestion = await _context.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.QuestionId == id);

            if (dbQuestion is null)
            {
                return NotFound("question not found.");
            }

            dbQuestion.Content = dto.Content;

            foreach (var choice in dbQuestion.Choices)
            {
                var newChoice = dto.Choices.FirstOrDefault(c => c.ChoiceId == choice.ChoiceId);
                if (newChoice is null)
                {
                    return BadRequest("choice not found.");
                }

                choice.Content = newChoice.Content;
                choice.IsCorrect = newChoice.IsCorrect;
            }

            _context.Questions.Update(dbQuestion);

            // USER ANSEWR UPDATE
            var userAnswers = await _context.UserAnswers
                .Where(ua => ua.QuestionId == id)
                .ToListAsync();

            if (userAnswers.Any())
            {
                foreach (var userAnswer in userAnswers)
                {
                    var choice = dbQuestion.Choices.FirstOrDefault(c => c.ChoiceId == userAnswer.ChoiceId);
                    if (choice is null)
                    {
                        return BadRequest("choice not found.");
                    }

                    userAnswer.IsCorrect = choice.IsCorrect;
                }

                _context.UserAnswers.UpdateRange(userAnswers);
            }

            await _context.SaveChangesAsync();

            var dbQuestionCreator = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dbQuestion.CreatedBy);

            if (dbQuestionCreator is null)
            {
                   return BadRequest("question creator not found.");
            }

            var updatedDbQuestion = new GetQuestionDto
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
                    UserId = dbQuestionCreator.UserId,
                    Username = dbQuestionCreator.Username,
                }
            };

            return Ok(updatedDbQuestion);
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<QuestionAnswerDto>> GetQuestionsWithUserAnswer(int id)
        {
            var userAnswers = await _context.UserAnswers
                .Where(ua => ua.UserId == id)
                .ToListAsync();

            var dbQuestions = await _context.Questions
                .Include(q => q.Choices)
                .Include(q => q.Creator)
                .ToListAsync();

            var questionsWithAnswers = dbQuestions
                .Select(q => new QuestionAnswerDto
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
                    },
                    UserAnswer =
                        userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId) is not null
                            ? new UserAnswerDto
                            {
                                UserAnswerId = userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId)
                                    .UserAnswerId,
                                UserId = userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId).UserId,
                                ChoiceId = userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId).ChoiceId,
                                QuestionId = userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId)
                                    .QuestionId,
                                IsCorrect = userAnswers.FirstOrDefault(ua => ua.QuestionId == q.QuestionId).IsCorrect
                            }
                            : null
                }).ToList();


            return Ok(questionsWithAnswers);
        }

        [HttpGet("stats/{id}")]
        public async Task<ActionResult<User>> GetQuizStats(int id)
        {
            // Total number of attempts
            var totalAttempts = await _context.UserAnswers
                .CountAsync(ua => ua.UserId == id);

            // Total number of correct answers
            var totalCorrectAnswers = await _context.UserAnswers
                .CountAsync(ua => ua.UserId == id && ua.IsCorrect);

            var stats = new QuizStatsDto
            {
                TotalAttempts = totalAttempts,
                TotalCorrectAnswers = totalCorrectAnswers
            };

            return Ok(stats);
        }

        [HttpPost("answer")]
        public async Task<ActionResult<UserAnswer>> AnswerQuestion(PostAnswerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbQuestion = await _context.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.QuestionId == dto.QuestionId);

            if (dbQuestion is null)
            {
                return NotFound("question not found.");
            }

            var dbChoice = dbQuestion.Choices.FirstOrDefault(c => c.ChoiceId == dto.ChoiceId);

            if (dbChoice is null)
            {
                return NotFound("choice not found.");
            }

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);

            if (dbUser is null)
            {
                return NotFound("user not found.");
            }

            var dbUserAnswer = await _context.UserAnswers
                .FirstOrDefaultAsync(ua => ua.UserId == dto.UserId && ua.QuestionId == dto.QuestionId);

            if (dbUserAnswer is not null)
            {
                return BadRequest("user has already answered this question.");
            }

            var newDbUserAnswer = new UserAnswer
            {
                UserId = dto.UserId,
                ChoiceId = dto.ChoiceId,
                QuestionId = dto.QuestionId,
                IsCorrect = dbChoice.IsCorrect,
                DateAnswered = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.UserAnswers.Add(newDbUserAnswer);
            await _context.SaveChangesAsync();

            var dbUserAnswerDto = new UserAnswerDto
            {
                UserAnswerId = newDbUserAnswer.UserAnswerId,
                UserId = newDbUserAnswer.UserId,
                ChoiceId = newDbUserAnswer.ChoiceId,
                QuestionId = newDbUserAnswer.QuestionId,
                IsCorrect = newDbUserAnswer.IsCorrect
            };

            return Ok(dbUserAnswerDto);
        }
    }
}