namespace OntoMath_QAS
{
    public sealed class AppConstants
    {
        public const string AppName = "OntoMath-QAS";

        public sealed class API
        {
            public static string Version = "v1";

            public static string Bearer = "Bearer";

            public sealed class ContentType
            {

                public static string BinaryFile = "application/octet-stream";

                public static string JSON = "appliation/json";
            }

            public sealed class Swagger
            {
                public static string Endpoint = $"/swagger/{Version}/swagger.json";

                public static string Title = $"{AppName} API {Version}";
            }
        }
    }
}