using BankApp.Data;
using BankApp.Model;
using FluentAssertions;

namespace BankApp.Tests
{
	public partial class DatabaseTests
	{
		[Fact]
		public void GivenACleanDatabase_WhenANewUserIsCreated_AUserIsInserted()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
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

			// act
			var newUser = database.CreateUser(newUserRequest);

			// assert
			InMemoryDatabaseContext.Users.Should().HaveCount(1);

			newUser.FirstName.Should().Be("test");
			newUser.LastName.Should().Be("testman");
			newUser.StreetAddress.Should().Be("123 test road");
			newUser.PostCode.Should().Be("TES T12");
			newUser.DateOfBirth.Should().Be(dateOfBirth);
			newUser.MembershipTypeId.Should().Be(1);
			newUser.MembershipType.MembershipAlias.Should().Be("Free");
		}	
		
		
		[Fact]
		public void GivenAnExistingUser_WhenPatchingTheMembershipType_TheUsersMembershipTypeIsUpdated()
		{
			// arrange
			var database = new InMemoryDatabaseContext();
			var existingUser = CreateUser(database);

			var changeMembershipRequest = new ChangeMembershipTypeRequest
			{
				NewMembershipType = MembershipType.BankingPlatinum,
				UserId = existingUser.UserId
			};

			// act
			var updatedUser = database.ChangeUserMembershipType(changeMembershipRequest);

			// assert
			updatedUser.MembershipTypeId.Should().Be(3);
			updatedUser.MembershipType.MembershipAlias.Should().Be("Premium");
		}
	}
}
