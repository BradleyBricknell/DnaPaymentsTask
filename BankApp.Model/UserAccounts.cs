using System.Collections.Generic;

namespace BankApp.Model
{
	/// <summary>
	/// Data transfer object for sending back the user's account data back to the sender
	/// </summary>
	public class UserAccountsDto
	{
		/// <summary>
		/// The user
		/// </summary>
		public UserDto User { get; set; }

		/// <summary>
		/// The accounts pertaining to the user
		/// </summary>
		public List<AccountDto> Accounts { get; set; }
	}
}
