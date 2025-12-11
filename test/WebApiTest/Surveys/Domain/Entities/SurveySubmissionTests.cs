using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Errors;

namespace WebApiTest.Surveys.Domain.Entities
{
    public class SurveySubmissionTestes
    {
        [Fact]
        public void Create_ShouldReturnError_WhenAnswersListIsEmpty()
        {
            // Arrange
            var input = new CreateSurveySubmissionInput(Guid.NewGuid(), []);

            // Act
            var result = SurveySubmission.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, SurveySubmissionErrors.CreateWithEmptyAnswers);
        }

        [Fact]
        public void Create_ShouldReturnSuccessWithNewSubmission_WhenAnswersAreProvided()
        {
            var surveyId = Guid.NewGuid();
            List<SurveyAnswer> sampleAnswers =
            [
                new()
                {
                    QuestionId = Guid.NewGuid(),
                    Type = QuestionType.CSAT,
                    NumericValue = 5,
                },
            ];

            var input = new CreateSurveySubmissionInput(surveyId, sampleAnswers);

            // Act
            var result = SurveySubmission.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(surveyId, result.Value.SurveyId);
            Assert.Equal(sampleAnswers, result.Value.Answers);
        }
    }
}
