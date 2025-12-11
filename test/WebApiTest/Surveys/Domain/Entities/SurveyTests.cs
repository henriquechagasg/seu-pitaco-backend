using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Errors;

namespace WebApiTest.Surveys.Domain.Entities
{
    public class SurveyTests
    {
        [Fact]
        public void UpdateQuestions_ShouldReturnForbiddenAction_WhenSurveyHasSubmissions()
        {
            // Arrange
            var survey = new SurveyBuilder().WithAnySubmission().Build();

            // Act
            var question = new SurveyQuestion();
            var result = survey.UpdateQuestions([question]);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(
                result.Error,
                SurveyErrors.ActionForbidden("A pesquisa contém respostas.")
            );
        }

        [Fact]
        public void UpdateQuestions_ShouldReturnEmptyOptionsError_WhenSingleChoiceQuestionHasNoOptions()
        {
            // Arrange
            var survey = new SurveyBuilder().Build();

            var invalidQuestionId = Guid.NewGuid();
            var invalidQuestion = new SurveyQuestion
            {
                Id = invalidQuestionId,
                Type = QuestionType.SingleChoice,
                Options = null,
            };
            var newQuestions = new List<SurveyQuestion> { invalidQuestion };

            // Act
            var result = survey.UpdateQuestions(newQuestions);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, SurveyErrors.EmptyOptions(invalidQuestionId.ToString()));
            Assert.NotEqual(newQuestions, survey.Questions);
        }

        [Fact]
        public void UpdateQuestions_ShouldUpdateQuestionsAndSetOrder_WhenNoSubmissionsAndValidQuestions()
        {
            // Assert
            var survey = new SurveyBuilder().Build();

            var question1 = new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Type = QuestionType.CSAT,
                Options = null,
            };
            var question2 = new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Type = QuestionType.SingleChoice,
                Options =
                [
                    new SurveyQuestionOption("Option A", "Default", string.Empty),
                    new SurveyQuestionOption("Option B", "Default", string.Empty),
                ],
            };
            var newQuestions = new List<SurveyQuestion> { question1, question2 };

            // Act
            var result = survey.UpdateQuestions(newQuestions);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newQuestions, survey.Questions);
            Assert.Equal(0, question1.Order);
            Assert.Equal(1, question2.Order);
            Assert.Equal(newQuestions, result.Value);
        }
    }
}
