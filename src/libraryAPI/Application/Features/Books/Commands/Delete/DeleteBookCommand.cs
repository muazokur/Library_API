using Application.Features.Books.Constants;
using Application.Features.Books.Constants;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Books.Constants.BooksOperationClaims;

namespace Application.Features.Books.Commands.Delete;

public class DeleteBookCommand : IRequest<DeletedBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, BooksOperationClaims.Delete];

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, DeletedBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;

        public DeleteBookCommandHandler(IMapper mapper, IBookRepository bookRepository,
                                         BookBusinessRules bookBusinessRules)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
        }

        public async Task<DeletedBookResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            if (book == null)
                throw new NullReferenceException();

            await _bookBusinessRules.CheckAuthorToOwn(book.AuthorId);

            await _bookBusinessRules.BookShouldExistWhenSelected(book);

            await _bookRepository.DeleteAsync(book!);

            DeletedBookResponse response = _mapper.Map<DeletedBookResponse>(book);
            return response;
        }
    }
}