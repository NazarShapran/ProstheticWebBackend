using Api.Dtos.UserDtos;
using Api.Modules.Errors;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Users.Commands;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("user")]
[ApiController]

public class UserController(ISender sender, IUserRepository userRepository, IUserQueries userQueries) : ControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await userQueries.GetAll(cancellationToken);
        return users.Select(UserDto.FromDomainModel).ToList();
    }
    
    [HttpGet("get/{userId:guid}")]
    public async Task<ActionResult<UserDto>> Get([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var entity = await userRepository.GetById(new UserId(userId), cancellationToken);
        
        return entity.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u),
            () => NotFound());
    }
    
    [HttpPost("signup")]
    public async Task<ActionResult<CreateUserDto>> Create([FromBody] CreateUserDto request, CancellationToken cancellationToken)
    {
        
        var input = new RegisterUserCommand()
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            BirthDate = request.BirthDate,
            Password = request.Password,
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<CreateUserDto>>(
            u => CreateUserDto.FromUser(u),
            e => e.ToObjectResult());
    }
    [HttpPost("signin")]
    public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto loginUserDto,
        CancellationToken cancellationToken)
    {
        var input = new LoginUserCommand
        {
            Email = loginUserDto.Email,
            Password = loginUserDto.Password
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<string>>
        (token => token,
            e => e.ToObjectResult());
    }
    
    [HttpDelete("delete/{userId:guid}")]
    public async Task<ActionResult<UserDto>> Delete([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var input = new DeleteUserCommand
        {
            Id = userId
        };
        
        var result = await sender.Send(input, cancellationToken);
        
        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }
}