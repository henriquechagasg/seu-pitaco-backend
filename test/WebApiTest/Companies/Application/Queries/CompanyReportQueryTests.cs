using WebApi.Companies.Application.Queries;
using WebApi.Surveys.Domain.Entities;
using WebApi.Surveys.Domain.Infra.Database;

namespace WebApiTest.Companies.Application.Queries
{
    public class CompanyReportQueryTests
    {
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

            Assert.Equal(result.Count, expectedCount);
        }

        [Fact]
        public async void Handle_ShouldReturnPositiveNps()
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

            Assert.True(result.Nps > 0);
        }

        [Fact]
        public async void Handle_ShouldReturnNegativeNps()
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

            Assert.True(result.Nps < 0);
        }
    }
}
