using WebApi.Shared.Core.Enums;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Errors;

namespace WebApiTest.Surveys.Domain.Entities
{
    public class SurveyAnswerTests
    {
        [Fact]
        public void Create_ShouldReturnAnswerTypeMismatch_WhenInputTypeDoesNotMatchQuestionType()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.NPS };
            var input = new CreateSurveyAnswerInput(question, QuestionType.CSAT, 5, null, null);

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(SurveyAnswerErrors.AnswerTypeMismatch, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnInvalidAnswerInput_WhenBothNumericAndTextValuesArePresent()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.OpenText };
            CreateSurveyAnswerInput input = new(question, QuestionType.OpenText, 5, "Answer", null);

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(SurveyAnswerErrors.InvalidAnswerInput, result.Error);
        }

        [Fact]
        public void Create_ShouldReturnMissingNumericValue_WhenNPSLacksValue()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.NPS };
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.NPS,
                null,
                "Answer",
                "Comment"
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(SurveyAnswerErrors.MissingNumericValue(QuestionType.NPS), result.Error);
        }

        [Fact]
        public void Create_ShouldSucceed_WhenCSATIsValid()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.CSAT };
            var input = new CreateSurveyAnswerInput(question, QuestionType.CSAT, 5, null, null);

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(question.Id, result.Value.QuestionId);
            Assert.Equal(5, result.Value.NumericValue);
            Assert.Equal(QuestionType.CSAT, result.Value.Type);
        }

        [Fact]
        public void Create_ShouldReturnMissingTextValue_WhenOpenTextIsEmpty()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.OpenText };
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.OpenText,
                null,
                string.Empty,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(SurveyAnswerErrors.MissingTextValue(QuestionType.OpenText), result.Error);
        }

        [Fact]
        public void Create_ShouldSucceed_WhenOpenTextIsValid()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.OpenText };

            var expectedText = "Detailed free-form comment.";
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.OpenText,
                null,
                expectedText,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedText, result.Value.TextValue);
            Assert.Equal(QuestionType.OpenText, result.Value.Type);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenChosenOptionIsNotInOptionsList()
        {
            // Arrange
            List<SurveyQuestionOption> validOptions =
            [
                new("A", "Default", null),
                new("B", "Default", null),
            ];
            var question = new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Type = QuestionType.SingleChoice,
                Options = validOptions,
            };

            var expectedText = "C";
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.SingleChoice,
                null,
                expectedText,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, SurveyAnswerErrors.InvalidOptionForQuestion(question.Id));
        }

        [Fact]
        public void Create_ShouldSucceed_WhenChosenOptionIsInOptionsList()
        {
            // Arrange
            List<SurveyQuestionOption> validOptions =
            [
                new("A", "Default", null),
                new("B", "Default", null),
            ];
            var question = new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Type = QuestionType.SingleChoice,
                Options = validOptions,
            };
            var expectedOption = "A";
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.SingleChoice,
                null,
                expectedOption,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedOption, result.Value.TextValue);
            Assert.Equal(QuestionType.SingleChoice, result.Value.Type);
        }
    }
}
