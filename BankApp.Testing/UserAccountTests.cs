using BankApp.Data;
using FluentAssertions;

namespace BankApp.Tests
{
	public partial class DatabaseTests
	{
		[Fact]
		public void GivenAnExistingUserWithAccounts_WhenFetchingTheUserAccounts_TheUserAccountsAreCorrecltyReturned()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
			var existingUser = CreateUser(database);
			var userId = existingUser.UserId;
			var newAccount = CreateAccount(database, userId);
			var newAccountTwo = CreateAccount(database, userId);

			//act
			var userAccounts = database.GetAccountsForUser(userId);

			// assert
			InMemoryDatabaseContext.Users.Should().HaveCount(1);
			InMemoryDatabaseContext.Accounts.Should().HaveCount(2);
			userAccounts.Should().HaveCount(2);

			userAccounts.First().Should().BeEquivalentTo(newAccount);
			userAccounts.Skip(1).First().Should().BeEquivalentTo(newAccountTwo);
		}
	}
}
