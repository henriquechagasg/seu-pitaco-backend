using Amazon.DynamoDBv2.DataModel;

namespace WebApi.Surveys.Infra.Database;

[DynamoDBTable("Surveys-trz5bbedcnaipdftxnls3sroya-NONE", LowerCamelCaseProperties = true)]
public class DynamoDbSurvey
{
    [DynamoDBHashKey]
    public required string Id { get; set; }

    public required string Company { get; set; }

    public required int Duration { get; set; }

    public string? AttendantName { get; set; }

    public string? InvitedToSurvey { get; set; }

    public string? ReferralSource { get; set; }

    public string? ReferralOtherComment { get; set; }

    public string? TrustSource { get; set; }

    public string? TrustOtherComment { get; set; }

    public string? VisitFrequency { get; set; }

    public int? AttendanceRating { get; set; }

    public string? AttendanceComment { get; set; }

    public int? EnvironmentRating { get; set; }

    public string? EnvironmentComment { get; set; }

    public int? ProductQualityRating { get; set; }

    public string? ProductQualityComment { get; set; }

    public int? WaitTimeRating { get; set; }

    public string? WaitTimeComment { get; set; }

    public int? CostBenefitRating { get; set; }

    public string? CostBenefitComment { get; set; }

    public int? PriceRating { get; set; }

    public string? PriceComment { get; set; }

    public int? DeliveryRating { get; set; }

    public string? DeliveryComment { get; set; }

    public int? DeliveryTimeRating { get; set; }

    public string? DeliveryTimeComment { get; set; }

    public int? NpsScore { get; set; }

    public string? GeneralComment { get; set; }

    public string? ContactName { get; set; }

    public string? Contact { get; set; }

    public string? UserAgent { get; set; }

    public string? Platform { get; set; }

    public required string RecaptchaToken { get; set; }

    public float? RecaptchaScore { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
