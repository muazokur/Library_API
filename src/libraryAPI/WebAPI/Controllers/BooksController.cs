using Application.Features.Books.Commands.Create;
using Application.Features.Books.Commands.Delete;
using Application.Features.Books.Commands.Update;
using Application.Features.Books.Queries.GetById;
using Application.Features.Books.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBookResponse>> Add([FromBody] CreateBookCommand command)
    {
        CreatedBookResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBookResponse>> Update([FromBody] UpdateBookCommand command)
    {
        UpdatedBookResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBookResponse>> Delete([FromRoute] Guid id)
    {
        DeleteBookCommand command = new() { Id = id };

        DeletedBookResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBookResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdBookQuery query = new() { Id = id };

        GetByIdBookResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListBookListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBookQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBookListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}