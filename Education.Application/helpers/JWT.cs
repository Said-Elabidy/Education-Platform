namespace Education.Application.helpers;

    public record JWT
    {
      public required string AccessTokenSecret { get; init; }
    public required string RefreshTokenSecret { get; init; }
    public required string Issuer { get; init; }

        public required  string Audience { get; init; }

     public required int AccessTokenLifetimeInMinutes { get; init; }

    public required int RefreshTokenLifetimeInMinutes { get; init; }

}

