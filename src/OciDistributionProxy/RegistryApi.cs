namespace OciDistributionProxy
{
    public static class RegistryApi
    {
        const string API_HEADER_KEY = "Docker-Distribution-API-Version";
        const string API_HEADER_VALUE = "registry/2.0";
        const string UPSTREAM_CONFIG = "UpstreamRegistry";

        private static string GetUpstream(IConfiguration config)
        {
            string? upstream = config[UPSTREAM_CONFIG];

            if (string.IsNullOrEmpty(upstream))
                throw new Exception($"Missing configuration for upstream: {UPSTREAM_CONFIG}");

            if (!Uri.TryCreate(upstream, UriKind.Absolute, out Uri? url))
            {
                throw new Exception($"Invalid URI for config setting {UPSTREAM_CONFIG}: {upstream}");
            }

            if (url.Scheme != "https")
            {
                throw new Exception($"Invalid URI scheme for config setting {UPSTREAM_CONFIG}, expected https. Full setting was: {upstream}");
            }

            return url.ToString();
        }

        public static RouteGroupBuilder MapRegsitryApi(this RouteGroupBuilder group, IConfiguration config)
        {
            string upstream = GetUpstream(config);

            group.MapGet("/", (HttpContext http) =>
            {
                http.Response.Headers[API_HEADER_KEY] = API_HEADER_VALUE;
                return Results.Ok();
            });
            group.MapGet("/{*remainder}", (HttpContext http, string remainder) =>
            {
                http.Response.Headers[API_HEADER_KEY] = API_HEADER_VALUE;
                return Results.Redirect($"{upstream}{remainder}");
            });
            return group;
        }
    }
}
