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
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.CSAT,
                5,
                null,
                null,
                null
            );

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
            CreateSurveyAnswerInput input = new(
                question,
                QuestionType.OpenText,
                5,
                "Answer",
                null,
                null
            );

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
                "Comment",
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(SurveyAnswerErrors.MissingNumericValue(QuestionType.NPS), result.Error);
        }

        [Fact]
        public void Create_ShouldReturnNumericValueOutOfRange_WhenNPSIsOutOfRAnge()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.NPS };
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.NPS,
                11,
                null,
                null,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(
                result.Error,
                SurveyAnswerErrors.NumericValueOutOfRange(QuestionType.NPS, min: 0, max: 10)
            );
        }

        [Fact]
        public void Create_ShouldReturnNumericValueOutOfRange_WhenCSATIsOutOfRAnge()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.CSAT };
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.CSAT,
                6,
                null,
                null,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(
                result.Error,
                SurveyAnswerErrors.NumericValueOutOfRange(QuestionType.CSAT, min: 1, max: 5)
            );
        }

        [Fact]
        public void Create_ShouldSucceed_WhenCSATIsValid()
        {
            // Arrange
            var question = new SurveyQuestion { Id = Guid.NewGuid(), Type = QuestionType.CSAT };
            var input = new CreateSurveyAnswerInput(
                question,
                QuestionType.CSAT,
                5,
                null,
                null,
                null
            );

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
                null,
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
                null,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedText, result.Value.TextValue);
            Assert.Equal(QuestionType.OpenText, result.Value.Type);
        }

        [Theory]
        [InlineData(QuestionType.SingleChoice)]
        [InlineData(QuestionType.Attendant)]
        public void Create_ShouldReturnError_WhenChosenOptionIsNotInOptionsList(QuestionType type)
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
                Type = type,
                Options = validOptions,
            };

            var expectedText = "C";
            var input = new CreateSurveyAnswerInput(question, type, null, expectedText, null, null);

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(result.Error, SurveyAnswerErrors.InvalidOptionForQuestion(question.Id));
        }

        [Theory]
        [InlineData(QuestionType.SingleChoice)]
        [InlineData(QuestionType.Attendant)]
        public void Create_ShouldSucceed_WhenChosenOptionIsInOptionsList(QuestionType type)
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
                Type = type,
                Options = validOptions,
            };
            var expectedOption = "A";
            var input = new CreateSurveyAnswerInput(
                question,
                type,
                null,
                expectedOption,
                null,
                null
            );

            // Act
            var result = SurveyAnswer.Create(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedOption, result.Value.TextValue);
            Assert.Equal(type, result.Value.Type);
        }
    }
}
