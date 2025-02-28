using BankApp.Data;
using BankApp.Extensions;
using BankApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;
	private readonly IDatabaseContext _databaseContext;

	public UserController(ILogger<UserController> logger, IDatabaseContext databaseContext)
	{
		_logger = logger;
		_databaseContext = databaseContext;
	}

	/// <summary>
	/// Get a user by their userId
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpGet]
	public UserDto GetUser(Guid userId)
	{
		var newUser = _databaseContext.GetUser(userId);

		return newUser.ToDto();
	}
	
	/// <summary>
	/// Create a new user
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpPost("create")]
	public UserDto CreateUser(CreateUserRequest userRequest)
	{
		var newUser = _databaseContext.CreateUser(userRequest);

		return newUser.ToDto();
	}

	[HttpPatch("membership")]
	public UserDto ChangeUserMembership(ChangeMembershipTypeRequest changeMembershipTypeRequest)
	{
		var amendedUser = _databaseContext.ChangeUserMembershipType(changeMembershipTypeRequest);

		return amendedUser.ToDto();
	}
}
