namespace EducationPlatform.helpers;

    public record JWT
    {
      public required string Secret { get; init; }
    public required string Issuer { get; init; }

        public required  string Audience { get; init; }

     public required int LifetimeInMinutes { get; init; }


    }

