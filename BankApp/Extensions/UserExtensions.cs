using BankApp.Data.Entities;
using BankApp.Model;

namespace BankApp.Extensions
{
	public static class UserExtensions
	{
		/// <summary>
		/// Extension method to map a User to UserDto
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static UserDto ToDto(this User user)
		{
			return new UserDto
			{
				UserId = user.UserId,
				FirstName = user.FirstName,
				LastName = user.LastName,
				EmailAddress = user.EmailAddress,
				DateOfBirth = user.DateOfBirth,
				StreetAddress = user.StreetAddress,
				PostCode = user.PostCode,
				MembershipType = user.MembershipType.MembershipAlias,
				CreatedAt = user.CreatedAt
			};
		}

		public static UserAccountsDto ToUserAccountsDto(this User user, List<Account> accounts)
		{
			return new UserAccountsDto
			{
				User = user.ToDto(),
				Accounts = accounts.Select(account => account.ToDto()).ToList()
			};
		}

	}
}
