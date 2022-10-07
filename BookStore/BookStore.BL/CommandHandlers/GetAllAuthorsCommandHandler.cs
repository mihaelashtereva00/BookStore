using BookStore.BL.Interfaces;
using BookStore.DL.Repositories.InMemoryRepositories;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.CommandHandlers
{
    public class GetAllAuthorsCommandHandler : IRequestHandler<GetAllAuthorsCommand, IEnumerable<Author>>
    {
        private readonly IAuthorService _authorReposiory;

        public GetAllAuthorsCommandHandler(IAuthorService authorRepository)
        {
            _authorReposiory = authorRepository;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorReposiory.GetAllAuthors();
        }

    }
}
