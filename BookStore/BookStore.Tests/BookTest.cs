using AutoMapper;
using BookStore.AutoMapper;
using BookStore.BL.Services;
using BookStore.Controllers;
using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.MsSql;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.Tests
{
    public class BookTest
    {
        private IList<Book> _books = new List<Book>()
        {
            new Book()
            {
            Id = 1,
            AuthorId = 1,
            Title = "T1",
            LastUpdated = DateTime.Now,
            Quantity = 1,
            Price = 10
            },
            new Book()
            {
            Id = 2,
            AuthorId = 2,
            Title = "T2",
            LastUpdated = DateTime.Now,
            Quantity = 1,
            Price = 10
            },
        };

        private readonly IMapper _mapper;
        private Mock<ILogger<BookService>> _loggerMock;
        private Mock<ILogger<BookController>> _loggerBookControllerMock;
        private readonly Mock<IBookRepository> _bookRepositoory;

        public BookTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });

            _mapper = mockMapperConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<BookService>>();
            _loggerBookControllerMock = new Mock<ILogger<BookController>>();
            _bookRepositoory = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Book_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            _bookRepositoory.Setup(x => x.GetAllBooks())
                                  .ReturnsAsync(_books);

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //Act
            var result = await controller.Get();

            //Assert
            var okObjecrResult = result as OkObjectResult;
            Assert.NotNull(okObjecrResult);
            var books = okObjecrResult.Value as IEnumerable<Book>;
            Assert.NotNull(books);
            Assert.NotEmpty(books);
            Assert.Equal(expectedCount, books.Count());
            Assert.Equal(books, _books);
        }

        [Fact]
        public async Task Book_GetBookById_Ok()
        {
            //setup
            var bookId = 1;
            var expectedBook = _books.First(a => a.Id == bookId);

            _bookRepositoory.Setup(x => x.GetById(bookId)).ReturnsAsync(expectedBook);

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.GetById(bookId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var book = okObjectResult.Value as Book;
            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }

        [Fact]
        public async Task Author_GetBookById_NotFound()
        {
            //setup
            var bookId = 3;

            _bookRepositoory.Setup(x => x.GetById(bookId)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookId));

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.GetById(bookId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedBookId = (int)notFoundObjectResult.Value;

            Assert.Equal(bookId, returnedBookId);
        }

        [Fact]
        public async Task Book_Add_Book_Ok()
        {
            //setup
            var bookRequest = new BookRequest()
            {
                Id = 1,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };

            var expectedBookId = 3;

            _bookRepositoory.Setup(x => x.AddBook(It.IsAny<Book>()))
                                 .Callback(() =>
                                 {
                                     _books.Add(new Book()
                                     {
                                         Id = expectedBookId,
                                         AuthorId = bookRequest.AuthorId,
                                         Title = bookRequest.Title,
                                         LastUpdated = bookRequest.LastUpdated,
                                         Quantity = bookRequest.Quantity,
                                         Price = bookRequest.Price
                                     });
                                 })!.ReturnsAsync(() => _books.FirstOrDefault(x => x.Id == expectedBookId));

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.AddBook(bookRequest);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var resultValue = okObjectResult.Value as BookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedBookId, resultValue.Book.Id);
        }

        [Fact]
        public async Task Book_Add_Book_Exist()
        {
            //setup
            var bookRequest = new BookRequest()
            {
                Id = 1,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };

            _bookRepositoory.Setup(x => x.GetById(bookRequest.Id)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookRequest.Id));

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);


            //act
            var result = await controller.AddBook(bookRequest);

            //assert
            var badObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badObjectResult);
            var resultValue = badObjectResult.Value as BookResponse;
            Assert.Equal("Book already exist", resultValue.Message);
        }

        [Fact]
        public async Task Book_Update_Ok()
        {
            //setup
            var bookRequest = new BookRequest()
            {
                Id = 1,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };


            _bookRepositoory.Setup(x => x.GetById(bookRequest.Id)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookRequest.Id));

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.Update(bookRequest);

            //assert
            var OkObjectResult = result as OkObjectResult;
            Assert.NotNull(OkObjectResult);
            var resultValue = OkObjectResult.Value as BookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Successfully updated book", resultValue.Message);
        }

        [Fact]
        public async Task Book_Update_BadRequest()
        {
            //setup
            //setup
            var bookRequest = new BookRequest()
            {
                Id = 3,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };

            _bookRepositoory.Setup(x => x.GetById(bookRequest.Id)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookRequest.Id));

            //inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            var result = await controller.Update(bookRequest);

            //assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);
            var resultValue = badRequestObjectResult.Value as BookResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Book does not exist", resultValue.Message);
        }

        [Fact]
        public async Task Book_Delete_Ok()
        {
            //setup
            int bookIdRequest = 1;
            int expectedCount = 1;

            _bookRepositoory.Setup(x => x.GetAllBooks()).ReturnsAsync(_books);
            _bookRepositoory.Setup(x => x.GetById(bookIdRequest)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookIdRequest));

            var bookToRemove = new Book()
            {
                Id = 1,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };

            _bookRepositoory.Setup(x => x.DeleteBook(bookIdRequest))
                                 .Callback(() =>
                                 {
                                     _books.Remove(bookToRemove);
                                 }).ReturnsAsync(() => bookToRemove);




            ///inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);

            var controller = new BookController(_loggerBookControllerMock.Object, service);
            //act
            var delete = await controller.Delete(bookIdRequest);
            var result = await controller.Get();

            //assert
            var okObjecrResult = result as OkObjectResult;
            var books = okObjecrResult.Value as IEnumerable<Book>;
            Assert.NotNull(books);
            var OkDelete = delete as OkObjectResult;
            var book = OkDelete.Value as Book;
            Assert.NotNull(book);
            Assert.DoesNotContain(book, books);
        }
        [Fact]
        public async Task Book_Delete_Id_Not_Exist()
        {
            //setup
            int bookIdRequest = 3;
            int expectedCount = 2;

            _bookRepositoory.Setup(x => x.GetAllBooks()).ReturnsAsync(_books);
            _bookRepositoory.Setup(x => x.GetById(bookIdRequest)).ReturnsAsync(_books.FirstOrDefault(x => x.Id == bookIdRequest));

            var bookToRemove = new Book()
            {
                Id = 1,
                AuthorId = 1,
                Title = "T1",
                LastUpdated = DateTime.Now,
                Quantity = 1,
                Price = 10
            };

            _bookRepositoory.Setup(x => x.DeleteBook(bookIdRequest))
                                 .Callback(() =>
                                 {
                                     _books.Remove(bookToRemove);
                                 }).ReturnsAsync(() => bookToRemove);

            ///inject
            var service = new BookService(_bookRepositoory.Object, _mapper, _loggerMock.Object);
            var controller = new BookController(_loggerBookControllerMock.Object, service);

            //act
            await controller.Delete(bookIdRequest);
            var result = await controller.Get();

            //assert
            var okObjecrResult = result as OkObjectResult;
            var books = okObjecrResult.Value as IEnumerable<Book>;
            Assert.NotNull(books);
            Assert.Equal(expectedCount, books.Count());
        }
    }
}