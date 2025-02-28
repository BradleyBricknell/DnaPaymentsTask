using BankApp.Data;
using BankApp.Model;
using FluentAssertions;
using Type = BankApp.Model.Type;

namespace BankApp.Tests
{
	public partial class DatabaseTests
	{
		[Fact]
		public void GivenAnExistingUser_WhenCreatingANewAccount_ANewAccountIsCreated()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
			var existingUser = CreateUser(database);

			var newCurrentAccountRequest = new CreateAccountRequest
			{
				UserId = existingUser.UserId,
				AccountType = Type.Current,
				StartingBalance = 25.75M,
				AccountAlias = "Test Current Account"
			};

			// act
			var newAccount = database.CreateAccount(newCurrentAccountRequest);

			// assert
			InMemoryDatabaseContext.Accounts.Should().HaveCount(1);
			newAccount.AccountAlias.Should().Be("Test Current Account");
			newAccount.AvailableBalance.Should().Be(25.75M);
			newAccount.CurrentBalance.Should().Be(25.75M);
			newAccount.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1));
			newAccount.LastUnfrozenAt.Should().Be(DateTimeOffset.MinValue);
			newAccount.LastFrozenAt.Should().Be(DateTimeOffset.MinValue);
			newAccount.AccountType.AccountTypeAlias.Should().Be("Current");
		}		
		
		[Fact]
		public void GivenAnExistingAccount_WhenFreezingTheAccount_TheAccountIsFrozen()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
			var existingUser = CreateUser(database);
			var userId = existingUser.UserId;
			var newAccount = CreateAccount(database, userId);

			var freezeAccountRequest = new FreezeAccountRequest
			{
				UserId = userId,
				AccountId = newAccount.Id,
			};

			// act
			var frozenAccount = database.FreezeAccount(freezeAccountRequest);

			// assert
			frozenAccount.LastFrozenAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1));
		}	
		
		[Fact]
		public void GivenAnExistingFrozenAccount_WhenUnfreezingTheAccount_TheAccountIsUnfrozen()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
			var existingUser = CreateUser(database);
			var userId = existingUser.UserId;
			var existingAccount = CreateAccount(database, userId);

			var freezeAccountRequest = new FreezeAccountRequest
			{
				UserId = userId,
				AccountId = existingAccount.Id,
			};
			database.FreezeAccount(freezeAccountRequest);

			var unfreezeAccountRequest = new UnfreezeAccountRequest
			{
				UserId = userId,
				AccountId = existingAccount.Id,
			};

			// act
			var unfrozenAccount = database.UnfreezeAccount(unfreezeAccountRequest);

			// assert
			unfrozenAccount.LastUnfrozenAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1));
		}
	}
}
