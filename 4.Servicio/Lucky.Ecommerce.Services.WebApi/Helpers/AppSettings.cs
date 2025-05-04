namespace Lucky.Ecommerce.Services.WebApi.Helpers
{
    public class AppSettings
    {
        public class CorsSettings
        {
            public string[] AllowedOrigins { get; set; }
            public string AllowedHeaders { get; set; }
            public string AllowedMethods { get; set; }
        }

        public class JWTSettings
        {
            public string Secret { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int TokenExpirationMinutes { get; set; }


        }

    }
}
