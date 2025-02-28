using System;

namespace BankApp.Model
{
	/// <summary>
	/// Class object to provide account data to be created.
	/// </summary>
	public class CreateAccountRequest
	{
		/// <summary>
		/// The user the account is created for
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The AccountType 
		/// </summary>
		public Type AccountType { get; set; }

		/// <summary>
		/// An optional starting balance value
		/// </summary>
		public decimal? StartingBalance { get; set; }

		/// <summary>
		/// An optional Account alias
		/// </summary>
		public string AccountAlias { get; set; }
	}

	/// <summary>
	/// Class object to provide which account should be frozen, and by whom
	/// </summary>
	public class FreezeAccountRequest
	{
		/// <summary>
		/// The user attempting to freeze the account
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The account the freeze attempt is happening to
		/// </summary>
		public Guid AccountId { get; set; }

		/// <summary>
		/// Optional reason field to track the reason for the freeze which could be generated and populate the Request field depending on the context the API was called from
		/// Such as the customer banking app, or the Customer Service rep using an internal portal to manage the account on behalf of the user
		/// I.e. 'Custom Service Rep freeze due on behalf of customer' or 'User froze account on banking app'
		/// </summary>
		public string Reason { get; set; }
	}	
	
	/// <summary>
	/// Class object to provide which account should be frozen, and by whom
	/// </summary>
	public class UnfreezeAccountRequest
	{
		/// <summary>
		/// The user attempting to unfreeze the account
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The account the unfreeze attempt is happening to
		/// </summary>
		public Guid AccountId { get; set; }

		/// <summary>
		/// Optional reason field to track the reason for the unfreeze which could be generated and populate the Request field depending on the context the API was called from
		/// Such as the customer banking app, or the Customer Service rep using an internal portal to manage the account on behalf of the user
		/// I.e. 'Custom Service Rep unfroze account on customer request' or 'User unfroze account on banking app'
		/// </summary>
		public string Reason { get; set; }
	}

	/// <summary>
	/// Enum to capture which account type a user wishes to create with a default fallback value if the account type is not provided
	/// </summary>
	public enum Type
	{
		Unknown = 0,
		Current = 1,
		Savings = 2
	}

	/// <summary>
	/// Data-transfer object for sending the account data back to the sender
	/// </summary>
	public class AccountDto
	{
		/// <summary>
		/// The account Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The account type
		/// </summary>
		public string AccountType { get; set; }

		/// <summary>
		/// The alias of the account
		/// </summary>
		public string AccountAlias { get; set; }

		/// <summary>
		/// The account's current balance
		/// </summary>
		public decimal CurrentBalance { get; set; }

		/// <summary>
		/// The account's available balance
		/// </summary>
		public decimal AvailableBalance { get; set; }

		/// <summary>
		/// The account's current status
		/// </summary>
		public AccountStatus Status { get; set; }
	}

	public enum AccountStatus
	{
		Unknown = 0,
		Frozen = 1,
		Active = 2
	}
}
