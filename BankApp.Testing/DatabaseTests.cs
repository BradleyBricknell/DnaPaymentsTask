using BankApp.Data.Entities;
using BankApp.Data;
using BankApp.Model;
using Type = BankApp.Model.Type;

namespace BankApp.Tests
{
    public partial class DatabaseTests
    {
		private User CreateUser(InMemoryDatabaseContext databaseContext)
		{
			var dateOfBirth = new DateTimeOffset(2022, 10, 10, 10, 10, 10, TimeSpan.FromHours(12));

			var newUserRequest = new CreateUserRequest
			{
				FirstName = "test",
				LastName = "testman",
				StreetAddress = "123 test road",
				PostCode = "TES T12",
				DateOfBirth = dateOfBirth,
				MembershipType = MembershipType.FreeBanking
			};

			var newUser = databaseContext.CreateUser(newUserRequest);

			return newUser;
		}		
		
		private Account CreateAccount(InMemoryDatabaseContext databaseContext, Guid userId)
		{
			var database = new InMemoryDatabaseContext();

			var newCurrentAccountRequest = new CreateAccountRequest
			{
				UserId = userId,
				AccountType = Type.Current,
				StartingBalance = 25.75M,
				AccountAlias = "Test Current Account"
			};

			// act
			var newAccount = database.CreateAccount(newCurrentAccountRequest);

			return newAccount;
		}
	}
}
