using AutoMapper;
using BookStore.AutoMapper;
using BookStore.BL.Services;
using BookStore.Controllers;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.Tests
{
    public class AuthorTest
    {
        private IList<Author> _authors = new List<Author>()
        {
            new Author()
            {
            Id = 1,
            Age = 21,
            DateOfBirth = DateTime.Now,
            Name = "Name1",
            Nickname = "Nickname1"
            },
            new Author()
            {
            Id = 2,
            Age = 54,
            DateOfBirth = DateTime.Now,
            Name = "Author2",
            Nickname = "Nickname2"
            },
        };

        private readonly IMapper _mapper;
        private Mock<ILogger<AuthorServices>> _loggerMock;
        private Mock<ILogger<AuthorController>> _loggerAuthorControllerMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        public AuthorTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });

            _mapper = mockMapperConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<AuthorServices>>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _loggerAuthorControllerMock = new Mock<ILogger<AuthorController>>();
        }

        [Fact]
        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            _authorRepositoryMock.Setup(x => x.GetAllAuthors())
                                  .ReturnsAsync(_authors);

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //Act
            var result = await controller.Get();

            //Assert
            var okObjecrResult = result as OkObjectResult;
            Assert.NotNull(okObjecrResult);
            var authors = okObjecrResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
            Assert.Equal(authors, _authors);
        }

        [Fact]
        public async Task Author_GetAuthorById_Ok()
        {
            //setup
            var authorId = 1;
            var expectedAuthor = _authors.First(a => a.Id == authorId);

            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(expectedAuthor);

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var author = okObjectResult.Value as Author;
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
        }

        [Fact]
        public async Task Author_GetAuthorById_NotFound()
        {
            //setup
            var authorId = 3;

            _authorRepositoryMock.Setup(x => x.GetById(authorId)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.GetById(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);

            var returnedAuthorId = (int)notFoundObjectResult.Value;

            Assert.Equal(authorId, returnedAuthorId);
        }

        [Fact]
        public async Task Author_Add_Author_Ok()
        {
            //setup
            var authorRequest = new AuthorRequest()
            {
                Nickname = "New nickname",
                Age = 22,
                DateOfBirth = DateTime.Now,
                Name = "Author Request Tset"
            };

            var expectedAuthorId = 3;

            _authorRepositoryMock.Setup(x => x.AddAuthor(It.IsAny<Author>()))
                                 .Callback(() =>
                                 {
                                     _authors.Add(new Author()
                                     {
                                         Id = expectedAuthorId,
                                         Name = authorRequest.Name,
                                         Age = authorRequest.Age,
                                         DateOfBirth = authorRequest.DateOfBirth,
                                         Nickname = authorRequest.Nickname
                                     });
                                 })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.Id == expectedAuthorId));

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.AddAuthorAsync(authorRequest);

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var resultValue = okObjectResult.Value as AuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal(expectedAuthorId, resultValue.Author.Id);
        }

        [Fact]
        public async Task Author_Add_Author_Exist()
        {
            //setup
            var authorRequest = new AuthorRequest()
            {
                Age = 77,
                DateOfBirth = DateTime.Now,
                Name = "Name1",
                Nickname = "Nickname1"
            };

            _authorRepositoryMock.Setup(x => x.GetByName(authorRequest.Name)).ReturnsAsync(_authors.FirstOrDefault(x => x.Name == authorRequest.Name));

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.AddAuthorAsync(authorRequest);

            //assert
            var badObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badObjectResult);
            var resultValue = badObjectResult.Value as AuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Author already exist", resultValue.Message);
        }

        [Fact]
        public async Task Author_Update_Ok()
        {
            //setup
            var authorRequest = new AuthorRequest()
            {
                Id = 1,
                Age = 21,
                DateOfBirth = DateTime.Now,
                Name = "Name1",
                Nickname = "Nickname1"
            };

            _authorRepositoryMock.Setup(x => x.GetById(authorRequest.Id)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.Update(authorRequest);

            //assert
            var OkObjectResult = result as OkObjectResult;
            Assert.NotNull(OkObjectResult);
            var resultValue = OkObjectResult.Value as AuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Successfully updated", resultValue.Message);
        }

        [Fact]
        public async Task Author_Update_BadRequest()
        {
            //setup
            var authorRequest = new AuthorRequest()
            {
                Id = 4,
                Age = 21,
                DateOfBirth = DateTime.Now,
                Name = "Name1",
                Nickname = "Nickname1"
            };

            _authorRepositoryMock.Setup(x => x.GetById(authorRequest.Id)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorRequest.Id));
            _authorRepositoryMock.Setup(x => x.GetById(authorRequest.Id)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);

            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var result = await controller.Update(authorRequest);

            //assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);
            var resultValue = badRequestObjectResult.Value as AuthorResponse;
            Assert.NotNull(resultValue);
            Assert.Equal("Author does not exist", resultValue.Message);
        }

        [Fact]
        public async Task Author_Delete_Ok()
        {
            //setup
            int authorRequestId = 1;
            int expectedCount = 1;

            _authorRepositoryMock.Setup(x => x.GetAllAuthors()).ReturnsAsync(_authors);
            _authorRepositoryMock.Setup(x => x.GetById(authorRequestId)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorRequestId));

            var authorToRemove = new Author()
            {
                Id = 1,
                Age = 21,
                DateOfBirth = DateTime.Now,
                Name = "Name1",
                Nickname = "Nickname1"
            };

            _authorRepositoryMock.Setup(x => x.DeleteAuthor(authorRequestId))
                                 .Callback(() =>
                                 {
                                     _authors.Remove(authorToRemove);
                                 }).ReturnsAsync(() => authorToRemove);

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            var delete = await controller.Delete(authorRequestId);
            var result = await controller.Get();

            //assert
            var okObjecrResult = result as OkObjectResult;
            var authors = okObjecrResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            var OkDelete = delete as OkObjectResult;
            var author = OkDelete.Value as Author;
            Assert.NotNull(author);
            Assert.DoesNotContain(author, authors);
        }
        [Fact]
        public async Task Author_Delete_Id_Not_Exist()
        {
            //setup
            int authorRequestId = 3;
            int expectedCount = 2;

            _authorRepositoryMock.Setup(x => x.GetAllAuthors()).ReturnsAsync(_authors);
            _authorRepositoryMock.Setup(x => x.GetById(authorRequestId)).ReturnsAsync(_authors.FirstOrDefault(x => x.Id == authorRequestId));

            var authorToRemove = new Author()
            {
                Id = 1,
                Age = 21,
                DateOfBirth = DateTime.Now,
                Name = "Name1",
                Nickname = "Nickname1"
            };

            _authorRepositoryMock.Setup(x => x.DeleteAuthor(authorRequestId))
                                 .Callback(() =>
                                 {
                                     _authors.Remove(authorToRemove);
                                 }).ReturnsAsync(() => authorToRemove);

            //inject
            var service = new AuthorServices(_authorRepositoryMock.Object, _mapper, _loggerMock.Object);
            var controller = new AuthorController(_loggerAuthorControllerMock.Object, service, _mapper);

            //act
            await controller.Delete(authorRequestId);
            var result = await controller.Get();

            //assert
            var okObjecrResult = result as OkObjectResult;
            var authors = okObjecrResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.Equal(expectedCount, authors.Count());
        }
    }


}