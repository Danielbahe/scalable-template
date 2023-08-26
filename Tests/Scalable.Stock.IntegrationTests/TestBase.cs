namespace Scalable.Stock.IntegrationTests
{
    public class TestBase
    {
        protected ApiWebApplicationFactory Application;

        protected TestBase()
        {
            Application = new ApiWebApplicationFactory();
        }

        public HttpClient GetClient()
        {
            return Application.CreateClient();
        }

        //protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        //{
        //    using var scope = Application.Services.CreateScope();

        //    var context = scope.ServiceProvider.GetService<MyAppDbContext>();

        //    context.Add(entity);

        //    await context.SaveChangesAsync();

        //    return entity;
        //}
    }
}
