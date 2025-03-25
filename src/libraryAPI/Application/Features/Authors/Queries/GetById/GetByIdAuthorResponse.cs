using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Queries.GetById;

public class GetByIdAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Bio { get; set; }
}