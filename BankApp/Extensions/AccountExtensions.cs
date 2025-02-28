using BankApp.Data.Entities;
using BankApp.Model;

namespace BankApp.Extensions
{
	public static class AccountExtensions
	{
		/// <summary>
		/// Extension method to map a Account to AccountDto
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public static AccountDto ToDto(this Account account)
		{
			return new AccountDto
			{
				Id = account.Id,
				AccountType = account.AccountType.AccountTypeAlias,
				AccountAlias = account.AccountAlias,
				CurrentBalance = account.CurrentBalance,
				AvailableBalance = account.AvailableBalance,
				Status =  account.LastFrozenAt > account.LastUnfrozenAt ? AccountStatus.Frozen : AccountStatus.Active
			};
		}
	}
}
