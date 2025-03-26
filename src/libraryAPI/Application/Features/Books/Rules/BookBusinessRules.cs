using Application.Features.Books.Constants;
using Application.Services.Repositories;
using Application.Services.UserContextService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Books.Rules;

public class BookBusinessRules : BaseBusinessRules
{
    private readonly IBookRepository _bookRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorRepository _authorRepository;

    public BookBusinessRules(IBookRepository bookRepository, ILocalizationService localizationService, IUserContextService userContextService, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _localizationService = localizationService;
        _userContextService = userContextService;
        _authorRepository = authorRepository;
    }
    public async Task CheckAuthorToOwn(Guid authorId)
    {
        Book? book = await _bookRepository.GetAsync(
      predicate: p => p.AuthorId == authorId,
      include: query => query.Include(b => b.Author));


        string userId = _userContextService.GetUserId();
        if (userId != book.Author.UserId.ToString())
            throw new ArgumentException("Yazar sadece kendi kitabi üzerinden islem yapabilir");
    }

    public async Task<Book> AddAuthorIdToBook(Book book)
    {
        string userId = _userContextService.GetUserId();
        Author? author = await _authorRepository.GetAsync(
             predicate: p => p.UserId == Guid.Parse(userId));

        book.AuthorId = author.Id;

        return book;

    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BookShouldExistWhenSelected(Book? book)
    {
        if (book == null)
            await throwBusinessException(BooksBusinessMessages.BookNotExists);
    }

    public async Task BookIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await BookShouldExistWhenSelected(book);
    }
}