namespace WebApi.Surveys.Domain.Entities
{
    public class SurveyBuilder
    {
        private readonly Survey _survey;

        public SurveyBuilder()
        {
            _survey = new Survey
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public SurveyBuilder WithAnySubmission()
        {
            var submission = new SurveySubmission { };
            _survey.Submissions = [submission];
            return this;
        }

        public Survey Build()
        {
            return _survey;
        }
    }
}
