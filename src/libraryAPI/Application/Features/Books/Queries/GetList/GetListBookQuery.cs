using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Books.Queries.GetList;

public class GetListBookQuery : IRequest<GetListResponse<GetListBookListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListBookQueryHandler : IRequestHandler<GetListBookQuery, GetListResponse<GetListBookListItemDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetListBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBookListItemDto>> Handle(GetListBookQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Book> books = await _bookRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBookListItemDto> response = _mapper.Map<GetListResponse<GetListBookListItemDto>>(books);
            return response;
        }
    }
}