namespace BankApp.Data.Entities
{
	/// <summary>
	/// Class object to represent the Account data entity
	/// </summary>
	public class Account
	{
		/// <summary>
		/// The unique, auto generated AccountId
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The unique, auto generated UserId
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// User facing alias for the account. I.e. 'My Savings Pot'
		/// </summary>
		public string AccountAlias { get; set; }

		/// <summary>
		/// Current balance £ value 
		/// </summary>
		public decimal CurrentBalance { get; set; }

		/// <summary>
		/// The available balance £ value 
		/// </summary>
		public decimal AvailableBalance { get; set; }
		
		/// <summary>
		/// Timestamp the account was created at
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Timestamp the account was last frozen
		/// </summary>
		public DateTimeOffset LastFrozenAt { get; set; }

		/// <summary>
		/// Timestamp the account was last frozen
		/// In conjuction with the LastFrozenAt timestamp, we can calculate if the account is currently frozen
		/// </summary>
		public DateTimeOffset LastUnfrozenAt { get; set; }

		/// <summary>
		/// The account type Id
		/// </summary>
		public long AccountTypeId { get; set; }
		
		/// <summary>
		/// The account type navigation property, using a library like entity framework, this will be automatically populated.
		/// </summary>
		public AccountType AccountType { get; set; }

	}

	/// <summary>
	/// Class object to represent the AccountType data entity
	/// Tracking the account type in a seperate entity means we can set specific account type centric behavior.
	/// I.e. for a savings account we want to communicate a transaction will have to wait until the next business day. But from a current account we would
	/// communicate the transaction can occur instantly. We may also want to introduce limits where standing orders can be fulfilled
	/// from a savings account, but a customer to business transaction cannot be made from a Savings account
	/// </summary>
	public class AccountType
	{
		/// <summary>
		/// A unique account type Id which is used to create a relationship between the account and their account type
		/// </summary>
		public long AccountTypeId { get; set; }

		/// <summary>
		/// User facing alias of the account type. I.e. 'Current' or 'Savings'
		/// </summary>
		public required string AccountTypeAlias { get; set; }
		
		/// <summary>
		/// A flag to determine where we communicate to the user that transactions from this account type won't be fulfilled until the next business day
		/// </summary>
		public bool FulfilllNextBusinessDay { get; set; }

		/// <summary>
		/// A flag to determine whether customer to business payments are allowed
		/// </summary>
		public bool CustomerToBusinessPaymantsAllowed { get; set; }
	}
}
