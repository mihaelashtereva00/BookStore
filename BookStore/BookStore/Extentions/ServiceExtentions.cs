using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;

namespace BookStore.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonInmemoryRepository>();
            services.AddSingleton<IAuthorRepository, AuthorInMemoryRepository>();
            services.AddSingleton<IBookRepository, BookInMemoryRepository>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorServices>();
            services.AddSingleton<IBookService, BookService>();
            
            return services;
        }
    }
}
