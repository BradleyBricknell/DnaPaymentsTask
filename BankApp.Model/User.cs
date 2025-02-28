using System;

namespace BankApp.Model
{
	/// <summary>
	/// Class object used by the api controller to provide user data to be created.
	/// </summary>
	public class CreateUserRequest
	{
		/// <summary>
		/// The user's given first name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The User's given last name
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// The User's given email address
		/// </summary>
		public string EmailAddres { get; set; }

		/// <summary>
		/// The user's date of birth
		/// </summary>
		public DateTimeOffset DateOfBirth { get; set; }

		/// <summary>
		/// The user's street address
		/// </summary>
		public string StreetAddress { get; set; }

		/// <summary>
		/// The user's post code
		/// </summary>
		public string PostCode { get; set; }

		/// <summary>
		/// Optional MembershipType field
		/// </summary>
		public MembershipType? MembershipType { get; set; }
	}

	/// <summary>
	/// Class object to provide to the api controller the details of changing a user's membership
	/// </summary>
	public class ChangeMembershipTypeRequest
	{
		/// <summary>
		/// The userId to change the membership type of
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The new membership type that the user will be changed to
		/// </summary>
		public MembershipType NewMembershipType { get; set; }
	}

	public enum MembershipType
	{
		FreeBanking = 1,
		BankingPlus = 2,
		BankingPlatinum = 3
	}

	/// <summary>
	/// Data-transfer object for sending the User data back to the sender
	/// </summary>
	public class UserDto 
	{ 
		/// <summary>
		/// The user's unique
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// The user's firstname
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// The user's lastname
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// The user's email address
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// The user's Date of birth
		/// </summary>
		public DateTimeOffset DateOfBirth { get; set; }

		/// <summary>
		/// The User's current street address
		/// </summary>
		public string StreetAddress { get; set; }

		/// <summary>
		/// The user's current postal code
		/// </summary>
		public string PostCode { get; set; }

		/// <summary>
		/// The user's current membership type
		/// </summary>
		public string MembershipType { get; set; }

		/// <summary>
		/// The timestamp the user was created
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }
	}
}
