using Application.Features.Authors.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Authors.Rules;

public class AuthorBusinessRules : BaseBusinessRules
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public AuthorBusinessRules(IAuthorRepository authorRepository, ILocalizationService localizationService, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository)
    {
        _authorRepository = authorRepository;
        _localizationService = localizationService;
        _operationClaimRepository = operationClaimRepository;
        _userOperationClaimRepository = userOperationClaimRepository;
    }
    public async Task AddBookRoleToAuthor(Guid userId)
    {
        var operationClaimPagination = await _operationClaimRepository.GetListAsync(predicate: p => p.Name.StartsWith("Books."),index:0,size:100);

        List<OperationClaim> operationClaims = operationClaimPagination.Items.ToList();

        foreach (var claim in operationClaims)
        {
            UserOperationClaim userOperationClaim = new() { UserId = userId,OperationClaimId=claim.Id };
            await _userOperationClaimRepository.AddAsync(userOperationClaim);
        }
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthorsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AuthorShouldExistWhenSelected(Author? author)
    {
        if (author == null)
            await throwBusinessException(AuthorsBusinessMessages.AuthorNotExists);
    }

    public async Task AuthorIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Author? author = await _authorRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AuthorShouldExistWhenSelected(author);
    }
}