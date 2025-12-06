using WebApi.Companies.Application.Queries;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Infra.Database;

namespace WebApiTest.Companies.Application.Queries
{
    public class CompanyReportQueryTests
    {
        private readonly Random random = Random.Shared;
        private readonly InMemorySurveysRepository _surveysRepository;
        private readonly CompanyReportQuery _query;

        public CompanyReportQueryTests()
        {
            _surveysRepository = new InMemorySurveysRepository();
            _query = new CompanyReportQuery(_surveysRepository);
        }

        [Fact]
        public async void Handle_ShouldReturnSurveysCountCorrectly()
        {
            var expectedCount = 10;
            var surveys = Enumerable
                .Range(1, expectedCount)
                .Select(_ => new SurveyBuilder().WithPromoterScore().Build())
                .ToList();
            foreach (var survey in surveys)
            {
                await _surveysRepository.Save(survey);
            }

            var result = await _query.Handle();

            Assert.NotNull(result);
            Assert.Equal(result.Count, expectedCount);
        }

        [Fact]
        public async void Handle_ShouldReturnNpsScoreCorrectly_WhenScoreIsPromoter()
        {
            var surveys = Enumerable
                .Range(1, 10)
                .Select(_ => new SurveyBuilder().WithPromoterScore().Build())
                .ToList();
            foreach (var survey in surveys)
            {
                await _surveysRepository.Save(survey);
            }

            var result = await _query.Handle();

            Assert.NotNull(result);
            Assert.True(result.NpsSummary.Score > 0);
        }

        [Fact]
        public async void Handle_ShouldReturnNpsScoreCorrectly_WhenScoreIsDetractor()
        {
            var expectedCount = 10;
            var surveys = Enumerable
                .Range(1, expectedCount)
                .Select(_ => new SurveyBuilder().WithDetractorScore().Build())
                .ToList();
            foreach (var survey in surveys)
            {
                await _surveysRepository.Save(survey);
            }

            var result = await _query.Handle();

            Assert.NotNull(result);
            Assert.True(result.NpsSummary.Score < 0);
        }

        [Fact]
        public async void Handle_ShouldReturnScoreCountCorrectly()
        {
            var expectedPromotersCount = random.Next(1, 10);
            var promotersSurvey = Enumerable
                .Range(1, expectedPromotersCount)
                .Select(_ => new SurveyBuilder().WithPromoterScore().Build())
                .ToList();

            var expectedNeutralsCount = random.Next(1, 10);
            var neutralsSurvey = Enumerable
                .Range(1, expectedNeutralsCount)
                .Select(_ => new SurveyBuilder().WithNeutralScore().Build())
                .ToList();

            var expectedDetractorsCount = random.Next(1, 10);
            var detractorsSurvey = Enumerable
                .Range(1, expectedDetractorsCount)
                .Select(_ => new SurveyBuilder().WithDetractorScore().Build())
                .ToList();

            var surveys = promotersSurvey.Concat(neutralsSurvey).Concat(detractorsSurvey).ToList();

            foreach (var survey in surveys)
            {
                await _surveysRepository.Save(survey);
            }

            var result = await _query.Handle();

            Assert.NotNull(result);
            Assert.Equal(result.NpsSummary.PromotersCount, expectedPromotersCount);
            Assert.Equal(result.NpsSummary.NeutralsCount, expectedNeutralsCount);
            Assert.Equal(result.NpsSummary.DetractorsCount, expectedDetractorsCount);
        }

        [Fact]
        public async void Handle_ShouldReturnRatingDistributionCorrectly()
        {
            var expectedFiveRatingCount = random.Next(1, 10);
            var fiveRatingSurveys = Enumerable
                .Range(1, expectedFiveRatingCount)
                .Select(_ => new SurveyBuilder().WithNpsScore(5).Build())
                .ToList();

            var expectedTenRatingCount = random.Next(1, 10);
            var tenRatingSurveys = Enumerable
                .Range(1, expectedTenRatingCount)
                .Select(_ => new SurveyBuilder().WithNpsScore(10).Build())
                .ToList();

            var surveys = fiveRatingSurveys.Concat(tenRatingSurveys).ToList();

            foreach (var survey in surveys)
            {
                await _surveysRepository.Save(survey);
            }

            var result = await _query.Handle();

            Assert.NotNull(result);
            var ratingDistribution = result.NpsSummary.RatingDistribution;
            Assert.True(ratingDistribution.ContainsKey("5"));
            Assert.Equal(ratingDistribution["5"], expectedFiveRatingCount);

            Assert.True(ratingDistribution.ContainsKey("10"));
            Assert.Equal(ratingDistribution["10"], expectedTenRatingCount);
        }
    }
}
