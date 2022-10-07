using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.CommandHandlers
{
    public class GetByIdAuthorCommandHandler : IRequestHandler<GetByIdAuthorCommand, Author>
    {
        private readonly IAuthorService _authorReposiory;

        public GetByIdAuthorCommandHandler(IAuthorService authorRepository)
        {
            _authorReposiory = authorRepository;
        }

        public async Task<Author> Handle(GetByIdAuthorCommand request, CancellationToken cancellationToken)
        {
            return await _authorReposiory.GetById(request.authorId);

        }
    }
}
