using BankApp.Data;
using BankApp.Extensions;
using BankApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserAccountController : ControllerBase
{
	private readonly ILogger<UserAccountController> _logger;
	private readonly IDatabaseContext _databaseContext;

	public UserAccountController(ILogger<UserAccountController> logger, IDatabaseContext databaseContext)
	{
		_logger = logger;
		_databaseContext = databaseContext;
	}

	/// <summary>
	/// Create a new user
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpGet]
	public UserAccountsDto GetUserAccounts(Guid userId)
	{
		var user = _databaseContext.GetUser(userId);

		var accounts = _databaseContext.GetAccountsForUser(userId);

		return user.ToUserAccountsDto(accounts);
	}
}
