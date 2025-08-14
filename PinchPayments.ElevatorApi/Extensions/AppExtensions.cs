namespace PinchPayments.ElevatorApi.Extensions
{
    public static class AppExtensions
    {
        public static WebApplication SetupApp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
