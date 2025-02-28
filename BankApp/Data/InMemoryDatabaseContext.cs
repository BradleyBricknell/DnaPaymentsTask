using BankApp.Data.Entities;
using BankApp.Model;

namespace BankApp.Data
{
	/// <summary>
	/// In memory solution for the database context
	/// By leveraging an interface we simply need to create another inheritor of IDatabaseContext, install the database library we wish to use and register the new type in program.cs
	/// </summary>
	public class InMemoryDatabaseContext : IDatabaseContext
	{
		#region DataSets
		/// <summary>
		/// In-memory dataset of the Users
		/// Dictionary Key is the UserId to easier lookups
		/// </summary>
		public static Dictionary<Guid, User> Users = new();

		/// <summary>
		/// Static In-memory dataset of the known UserMembershipTypes
		/// Dictionary Key is the MembershipTypeId to easier lookups
		/// </summary>
		public static Dictionary<long, UserMembershipType> UserMembershipTypes => new Dictionary<long, UserMembershipType>
		{
			{ 
				1, new UserMembershipType
				{
					MembershipTypeId = 1,
					MembershipAlias = "Free",
					SavingsAccountInterest = 3,
					CurrentAccountInterest =  1.5M,
					NumberOfSavingsAccountsPermitted = 1
				}
			},
			{ 
				2, new UserMembershipType
				{
					MembershipTypeId = 2,
					MembershipAlias = "Plus",
					SavingsAccountInterest = 4,
					CurrentAccountInterest =  2,
					NumberOfSavingsAccountsPermitted = 3
				}
			},
			{ 
				3, new UserMembershipType
				{
					MembershipTypeId = 3,
					MembershipAlias = "Premium",
					SavingsAccountInterest = 5,
					CurrentAccountInterest =  3,
					NumberOfSavingsAccountsPermitted = 5
				}
			}
		};

		/// <summary>
		///Static In-memory dataset of the Accounts
		/// Dictionary Key is the AccountId to easier lookups
		/// </summary>
		public static Dictionary<Guid, Account> Accounts { get; set; } = new();

		/// <summary>
		/// Static In-memory dataset of the known Account Types
		/// Dictionary Key is the AccountTypeId to easier lookups
		/// </summary>
		public static Dictionary<long, AccountType> AccountTypes = new Dictionary<long, AccountType>
		{
			{ 
				1, 
				new AccountType
				{
					AccountTypeId = 1,
					AccountTypeAlias = "Current",
					CustomerToBusinessPaymantsAllowed = true
				} 
			},
			{ 
				2, 
				new AccountType
				{
					AccountTypeId = 2,
					AccountTypeAlias = "Savings",
					FulfilllNextBusinessDay = true
				} 
			}
		};
		#endregion

		#region User

		/// <summary>
		/// Get User by their UserId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public User GetUser(Guid userId)
		{
			var user = GetExistingUser(userId);

			if (user is null)
			{
				throw new Exception($"Unable to get user with {userId}. Use does not exist");
			}

			return user;
		}

		/// <summary>
		/// Method to create and add a new user to the Users Dataset
		/// </summary>
		/// <param name="userRequest"></param>
		/// <returns></returns>
		public User CreateUser(CreateUserRequest userRequest)
		{
			var newUserId = Guid.NewGuid();

			// Create new user entity
			var newUser = new User
			{
				UserId = newUserId,
				FirstName = userRequest.FirstName,
				LastName = userRequest.LastName,
				EmailAddress = userRequest.EmailAddres,
				DateOfBirth = userRequest.DateOfBirth,
				StreetAddress = userRequest.StreetAddress,
				PostCode = userRequest.PostCode,
				CreatedAt = DateTimeOffset.UtcNow
			};

			// set membership type, use the provided membership type, if one has not been provided, default to FreeBanking
			// For the purposes of this exercise
			// We know that the test data added in program.cs already maps the MembershipTypeId to the enum values
			// In an environment with a DBMS ingretration we could have some simple mapping class/method to
			// more reliably map the enum from the API to the accountType stored in the database
		
			var membershipTypeId = userRequest.MembershipType.HasValue
				? (long)userRequest.MembershipType.Value
				: (long)MembershipType.FreeBanking;

			newUser.MembershipType = UserMembershipTypes[membershipTypeId];
			newUser.MembershipTypeId = membershipTypeId;

			// save the new user
			Users.Add(newUserId, newUser);

			return newUser;
		}

		/// <summary>
		/// Method to edit the membershiptype of an existing user
		/// </summary>
		/// <param name="changeMemebershipTypeRequest"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public User ChangeUserMembershipType(ChangeMembershipTypeRequest changeMemebershipTypeRequest)
		{
			// assert user exists and fetch the user 
			var existingUser = GetExistingUser(changeMemebershipTypeRequest.UserId);

			if (existingUser is null)
			{
				throw new Exception($"Unable to change membership type for {changeMemebershipTypeRequest.UserId}. User does not exist");
			}

			// apply the membership change
			existingUser.MembershipType = UserMembershipTypes[(long)changeMemebershipTypeRequest.NewMembershipType];
			existingUser.MembershipTypeId = (long)changeMemebershipTypeRequest.NewMembershipType;

			return existingUser;
		}
		#endregion

		#region Account
		/// <summary>
		/// Method to create a new account for an existing user
		/// </summary>
		/// <param name="createAccountRequest"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public Account CreateAccount(CreateAccountRequest createAccountRequest)
		{
			// assert user exists and fetch the user 
			var existingUser = GetExistingUser(createAccountRequest.UserId);
			if (existingUser is null)
			{
				throw new Exception($"Unable to create account for {createAccountRequest.UserId}. User does not exist");
			}

			// create new Account entity
			var newAccountId = Guid.NewGuid();

			var newAccount = new Account
			{
				Id = newAccountId,
				UserId = existingUser.UserId,
				AccountAlias = createAccountRequest.AccountAlias,
				CurrentBalance = createAccountRequest.StartingBalance ?? 0,
				AvailableBalance = createAccountRequest.StartingBalance ?? 0,
				CreatedAt = DateTimeOffset.UtcNow,
			};

			// set account type
			// For the purposes of this exercise
			// We know that the test data added in program.cs already maps the AccountTypeId to the enum values
			// In an environment with a DBMS ingretration we could have some simple mapping class/method to
			// more reliably map the enum from the API to the accountType stored in the database
			newAccount.AccountType = AccountTypes[(long)createAccountRequest.AccountType];
			newAccount.AccountTypeId = (long)createAccountRequest.AccountType;
			
			Accounts.Add(newAccountId, newAccount);

			return newAccount;
		}

		/// <summary>
		/// Method to freeze a user's account
		/// </summary>
		/// <param name="freezeAccountRequest"></param>
		/// <returns></returns>
		public Account FreezeAccount(FreezeAccountRequest freezeAccountRequest)
		{
			// assert user exists and fetch the user 
			var existingUser = GetExistingUser(freezeAccountRequest.UserId);
			if (existingUser is null)
			{
				throw new Exception($"Unable to freeze account for {freezeAccountRequest.UserId}. User does not exist");
			}

			var existingAccount = GetExistingAccount(freezeAccountRequest.AccountId);

			if (existingAccount is null)
			{
				throw new Exception($"Unable to freeze account {freezeAccountRequest.AccountId}. Account does not exist");
			}


			if (existingAccount.UserId != freezeAccountRequest.UserId)
			{
				throw new Exception($"Unable to freeze account {freezeAccountRequest.AccountId}. The account does not belong to the user {freezeAccountRequest.UserId}");
			}

			if (existingAccount.LastFrozenAt > existingAccount.LastUnfrozenAt)
			{
				throw new Exception($"Unable to freeze account {freezeAccountRequest.AccountId}. Account already frozen");
			}

			existingAccount.LastFrozenAt = DateTimeOffset.UtcNow;

			return existingAccount;
		}

		/// <summary>
		/// Method to freeze a user's account
		/// </summary>
		/// <param name="freezeAccountRequest"></param>
		/// <returns></returns>
		public Account UnfreezeAccount(UnfreezeAccountRequest unfreezeAccountRequest)
		{
			// assert user exists and fetch the user 
			var existingUser = GetExistingUser(unfreezeAccountRequest.UserId);
			if (existingUser is null)
			{
				throw new Exception($"Unable to unfreeze account for {unfreezeAccountRequest.UserId}. User does not exist");
			}

			var existingAccount = GetExistingAccount(unfreezeAccountRequest.AccountId);

			if (existingAccount is null)
			{
				throw new Exception($"Unable to unfreeze account {unfreezeAccountRequest.AccountId}. Account does not exist");
			}


			if (existingAccount.UserId != unfreezeAccountRequest.UserId)
			{
				throw new Exception($"Unable to unfreeze account {unfreezeAccountRequest.AccountId}. The account does not belong to the user {unfreezeAccountRequest.UserId}");
			}

			if (existingAccount.LastUnfrozenAt > existingAccount.LastFrozenAt)
			{
				throw new Exception($"Unable to unfreeze account {unfreezeAccountRequest.AccountId}. Account already unfrozen");
			}

			existingAccount.LastUnfrozenAt = DateTimeOffset.UtcNow;

			return existingAccount;
		}
		#endregion

		#region UserAccount
		/// <summary>
		/// Get accounts which match a userId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<Account> GetAccountsForUser(Guid userId)
		{
			var userAccounts = Accounts.Where(a => a.Value.UserId == userId).Select(v => v.Value).ToList();

			return userAccounts;
		}

		#endregion

		#region Helper Methods
		/// <summary>
		/// Helper method to fetch the User entity from the users DataSet
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		private User? GetExistingUser(Guid userId)
		{
			var userExists = Users.TryGetValue(userId, out var existingUser);

			if (!userExists || existingUser is null)
			{
				return null;
			}

			return existingUser;
		}

		/// <summary>
		/// Helper method to fetch the Account entity from the Accounts DataSet
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		private Account? GetExistingAccount(Guid accountId)
		{
			var accountExists = Accounts.TryGetValue(accountId, out var existingAccount);

			if (!accountExists || existingAccount is null)
			{
				return null;
			}

			return existingAccount;
		}
		#endregion

	
	}
}
