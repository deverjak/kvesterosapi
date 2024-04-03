namespace KvesterosApi;
public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Hikes
    {
        private const string Base = $"{ApiBase}/hikes";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:int}}";
        public const string GetAll = Base;
    }
}

