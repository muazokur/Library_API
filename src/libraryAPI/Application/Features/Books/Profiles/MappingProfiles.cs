using Application.Features.Books.Commands.Create;
using Application.Features.Books.Commands.Delete;
using Application.Features.Books.Commands.Update;
using Application.Features.Books.Queries.GetById;
using Application.Features.Books.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Books.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBookCommand, Book>();
        CreateMap<Book, CreatedBookResponse>();

        CreateMap<UpdateBookCommand, Book>();
        CreateMap<Book, UpdatedBookResponse>();

        CreateMap<DeleteBookCommand, Book>();
        CreateMap<Book, DeletedBookResponse>();

        CreateMap<Book, GetByIdBookResponse>();

        CreateMap<Book, GetListBookListItemDto>();
        CreateMap<IPaginate<Book>, GetListResponse<GetListBookListItemDto>>();
    }
}