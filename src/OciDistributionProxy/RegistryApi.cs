namespace OciDistributionProxy
{
    public static class RegistryApi
    {
        const string API_HEADER_KEY = "Docker-Distribution-API-Version";
        const string API_HEADER_VALUE = "registry/2.0";

        public static RouteGroupBuilder MapRegsitryApi(this RouteGroupBuilder group)
        {
            group.MapMethods("/", new string[] { "GET", "HEAD" }, (HttpContext http) =>
            {
                http.Response.Headers[API_HEADER_KEY] = API_HEADER_VALUE;
                return Results.Ok();
            });
            group.MapMethods("/{*remainder}", new string[] { "GET", "HEAD" }, (HttpContext http, string remainder) =>
            {
                http.Response.Headers[API_HEADER_KEY] = API_HEADER_VALUE;
                return Results.Redirect($"https://us-central1-docker.pkg.dev/v2/oci-proxy/oci-proxy-images/{remainder}");
            });
            return group;
        }
    }
}
