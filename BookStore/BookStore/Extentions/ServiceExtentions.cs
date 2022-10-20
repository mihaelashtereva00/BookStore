using BookStore.BL.DeliveryPurchaseConsumer;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.DL.Repositories.MongoRepository;
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
            services.AddSingleton<IPurcahseRepository, PurchaseRepository>();
            services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();


            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            {
                services.AddSingleton<IShoppingCard, ShoppingCardService>();
                services.AddSingleton<IPurchaseService, PurchaseService>();
                services.AddSingleton<IPersonService, PersonService>();
                services.AddSingleton<IAuthorService, AuthorServices>();
                services.AddSingleton<IBookService, BookService>();
                services.AddTransient<IIdentityService, IdentityService>();
                services.AddSingleton<IEmployeeService, EmployeeUserInfoService>();
                services.AddSingleton<KafkaProducerService<int, string>>();
                services.AddSingleton<KafkaConsumer<int, string>>();
                services.AddHostedService<PurchaseConsumerService>();
                services.AddHostedService<DeliveryConsumerService>();

                return services;
            }
        }
    }
}
