using Application.Features.Authors.Constants;
using Application.Features.Authors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Authors.Constants.AuthorsOperationClaims;

namespace Application.Features.Authors.Commands.Update;

public class UpdateAuthorCommand : IRequest<UpdatedAuthorResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Bio { get; set; }

    public string[] Roles => [Admin, Write, AuthorsOperationClaims.Update];

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdatedAuthorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorBusinessRules _authorBusinessRules;

        public UpdateAuthorCommandHandler(IMapper mapper, IAuthorRepository authorRepository,
                                         AuthorBusinessRules authorBusinessRules)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _authorBusinessRules = authorBusinessRules;
        }

        public async Task<UpdatedAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author? author = await _authorRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBusinessRules.AuthorShouldExistWhenSelected(author);
            author = _mapper.Map(request, author);

            await _authorRepository.UpdateAsync(author!);

            UpdatedAuthorResponse response = _mapper.Map<UpdatedAuthorResponse>(author);
            return response;
        }
    }
}