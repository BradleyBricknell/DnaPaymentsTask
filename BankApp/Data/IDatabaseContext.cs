using BankApp.Data.Entities;
using BankApp.Model;

namespace BankApp.Data
{
	/// <summary>
	/// The interface to define the behaviour required to maintain a user, as well as the user's bank accounts
	/// </summary>
	public interface IDatabaseContext
	{
		#region User
		User GetUser(Guid userId);
		User CreateUser(CreateUserRequest userRequest);
		User ChangeUserMembershipType(ChangeMembershipTypeRequest changeMemebershipTypeRequest);
		#endregion

		#region Account
		List<Account> GetAccountsForUser(Guid userId);
		Account CreateAccount(CreateAccountRequest createAccountRequest);
		Account FreezeAccount(FreezeAccountRequest freezeAccountRequest);
		Account UnfreezeAccount(UnfreezeAccountRequest freezeAccountRequest);
		#endregion
	}
}
