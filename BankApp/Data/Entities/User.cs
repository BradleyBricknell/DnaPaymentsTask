namespace BankApp.Data.Entities
{
	/// <summary>
	/// Class object to represent the User data entity
	/// </summary>
	public class User
	{
		/// <summary>
		/// User Id
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The user's given first name
		/// </summary>
		public required string FirstName { get; set; }

		/// <summary>
		/// The User's given last name
		/// </summary>
		public required string LastName { get; set; }

		/// <summary>
		/// The User's given email address
		/// </summary>
		public required string EmailAddress { get; set; }

		/// <summary>
		/// The user's date of birth
		/// </summary>
		public DateTimeOffset DateOfBirth { get; set; }

		/// <summary>
		/// The user's street address
		/// </summary>
		public required string StreetAddress { get; set; }

		/// <summary>
		/// The user's post code
		/// </summary>
		public required string PostCode { get; set; }

		/// <summary>
		/// The timestamp the user's account was created
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// The membership Id type the user holds with the bank.
		/// </summary>
		public long MembershipTypeId { get; set; }

		/// <summary>
		/// The account type navigation property, using a library like entity framework, this will be automatically populated.
		/// </summary>
		public UserMembershipType MembershipType { get; set; }
	}

	/// <summary>
	/// Class object to represent the UserMembershipTypes
	/// This class would allow us to extend the functionality of our banking system and modify aspects of a user's account, given their membership level
	/// I.e. A 'Premium' or 'Plus' user could have an enhanced interest rate over a 'free' user.
	/// </summary>
	public class UserMembershipType
	{
		/// <summary>
		/// A unique membership type Id which is used to create a relationship between the user and their membership type
		/// </summary>
		public long MembershipTypeId { get; set; }

		/// <summary>
		/// User facing alias of the membership type. I.e 'Free', 'Plus', or 'Premium'
		/// </summary>
		public required string MembershipAlias { get; set; }

		/// <summary>
		/// The interest value to increment an account's Savings balance by
		/// </summary>
		public required decimal SavingsAccountInterest { get; set; }

		/// <summary>
		/// The interest value to increment an account's Current balance by
		/// </summary>
		public required decimal CurrentAccountInterest { get; set; }

		/// <summary>
		/// A count of how many savings accounts a user is permitted to have
		/// I.e. 
		/// Free accounts - 1
		/// Plus accounts - 3
		/// Premium accounts - 5
		/// </summary>
		public int NumberOfSavingsAccountsPermitted { get; set; }

		/// The current functionality would dictact that all users (regardless of account membership type) can only have a single (1) current account.
		/// However this could easily be extended if the user case existed.
	}

}
