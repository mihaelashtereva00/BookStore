using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.Caches;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MsSql;
using BookStore.Caches;
using WebAPI.Models;
using BookStore.Models.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

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
        public static IServiceCollection RegisterServices<TKey, TValue>(this IServiceCollection services) where TValue : ICacheItem<TKey>
        {
            {
                services.AddSingleton<IPersonService, PersonService>();
                services.AddSingleton<IAuthorService, AuthorServices>();
                services.AddSingleton<IBookService, BookService>();
                services.AddTransient<IIdentityService, IdentityService>();
                services.AddSingleton<IEmployeeService, EmployeeUserInfoService>();
                services.AddSingleton<KafkaProducerService<int, string>>();

                return services;
            }
        }

        public static IServiceCollection Subsribe2Cache<TKey, TValue>(this IServiceCollection services) where   TValue : ICacheItem<TKey>
        {
                services.AddSingleton<KafkaHostedService<TKey, TValue>>();

            return services;
        }
    }
}
