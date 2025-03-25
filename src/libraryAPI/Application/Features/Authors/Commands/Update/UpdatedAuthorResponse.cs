using NArchitecture.Core.Application.Responses;

namespace Application.Features.Authors.Commands.Update;

public class UpdatedAuthorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Bio { get; set; }
}