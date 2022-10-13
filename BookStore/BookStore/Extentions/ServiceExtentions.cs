using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MsSql;

namespace BookStore.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPersonRepository, PersonInmemoryRepository>();
            services.AddSingleton<IAuthorRepository, AuthorRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepository>();
            services.AddSingleton<IEmployeesRepository, EmployeeRepository>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IAuthorService, AuthorServices>();
            services.AddSingleton<IBookService, BookService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<IEmployeeService, EmployeeUserInfoService>();
            //services.AddSingleton<IHostedService ,KafkaConsumerService<int,string>>();
            services.AddSingleton<KafkaProducerService<int,string>>();
            
            return services;
        }
    }
}
