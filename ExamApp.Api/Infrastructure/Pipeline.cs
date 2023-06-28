namespace ExamApp.Api.Infrastructure
{
    public static class Pipeline
    {
        public static IApplicationBuilder AddPipeline(this IApplicationBuilder applicationBuilder,WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            return applicationBuilder;
        }
    }
}
