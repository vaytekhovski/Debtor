namespace Debtor.API;

public static class SetupMiddlewarePipeline
{
    public static WebApplication SetupMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.RoutePrefix = string.Empty;
            config.SwaggerEndpoint("swagger/v1/swagger.json", "Authors API");
        });
        //app.UseExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}