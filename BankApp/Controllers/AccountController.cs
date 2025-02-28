using BankApp.Data;
using BankApp.Extensions;
using BankApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly ILogger<AccountController> _logger;
	private readonly IDatabaseContext _databaseContext;

	public AccountController(ILogger<AccountController> logger, IDatabaseContext databaseContext)
	{
		_logger = logger;
		_databaseContext = databaseContext;
	}

	/// <summary>
	/// Create a new user
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpPost("create")]
	public AccountDto CreateAccount(CreateAccountRequest accountRequest)
	{
		var newAccount = _databaseContext.CreateAccount(accountRequest);

		return newAccount.ToDto();
	}	
	
	/// <summary>
	/// Freeze an account
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpPatch("freeze")]
	public AccountDto FreezeAccount(FreezeAccountRequest accountRequest)
	{
		var frozenAccount = _databaseContext.FreezeAccount(accountRequest);

		return frozenAccount.ToDto();
	}
	

	/// <summary>
	/// Freeze an account
	/// </summary>
	/// <param name="userRequest"></param>
	/// <returns></returns>
	[HttpPatch("unfreeze")]
	public AccountDto UnfreezeAccount(UnfreezeAccountRequest accountRequest)
	{
		var unfrozenAccount = _databaseContext.UnfreezeAccount(accountRequest);

		return unfrozenAccount.ToDto();
	}
}
