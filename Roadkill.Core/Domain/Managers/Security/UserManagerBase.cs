﻿using System;
using System.Collections.Generic;
namespace Roadkill.Core
{
	/// <summary>
	/// The base class for user management tasks.
	/// </summary>
	public abstract class UserManagerBase : ManagerBase
	{
		/// <summary>
		/// Indicates whether this UserManager can perform deletes, updates or inserts for users.
		/// </summary>
		public abstract bool IsReadonly { get; }

		/// <summary>
		/// Activates the user with given activation key
		/// </summary>
		/// <param name="activationKey">The randomly generated activation key for the user.</param>
		/// <returns>True if the activation was successful</returns>
		public abstract bool ActivateUser(string activationKey);

		/// <summary>
		/// Adds a user to the system, and sets the <see cref="User.IsActivated"/> to true.
		/// </summary>
		/// <param name="email">The email or username.</param>
		/// <param name="password">The password.</param>
		/// <param name="isAdmin">if set to <c>true</c> the user is added as an admin.</param>
		/// <param name="isEditor">if set to <c>true</c> the user is added as an editor.</param>
		/// <returns>true if the user was added; false if the user already exists.</returns>
		public abstract bool AddUser(string email, string password, bool isAdmin, bool isEditor);

		/// <summary>
		/// Authenticates the user with the specified email.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <param name="password">The password.</param>
		/// <returns>true if the authentication was sucessful;false otherwise.</returns>
		public abstract bool Authenticate(string email, string password);

		/// <summary>
		/// Changes the email(username) of user to a new email address.
		/// </summary>
		/// <param name="email">The existing email address or username of the user.</param>
		/// <param name="newEmail">The new email/username.</param>
		/// <returns>true if the change was successful;false if the new email address already exists in the system.</returns>
		public abstract bool ChangeEmail(string oldEmail, string newEmail);

		/// <summary>
		/// Changes the password of user with the given email.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <param name="newPassword">The new password.</param>
		public abstract void ChangePassword(string email, string newPassword);

		/// <summary>
		/// Changes the password of the user with the given email, authenticating their password first..
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <param name="oldPassword">The old password.</param>
		/// <param name="newPassword">The new password to change to.</param>
		/// <returns>true if the password change was successful;false if the previous password was incorrect.</returns>
		public abstract bool ChangePassword(string email, string oldPassword, string newPassword);

		/// <summary>
		/// Deletes a user with the given email from the system.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <returns>true if the deletion was successful;false if the user could not be found.</returns>
		public abstract bool DeleteUser(string email);

		/// <summary>
		/// Determines whether the specified user with the given email/username is an admin.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <returns>true if the user is an admin; false otherwise.</returns>
		public abstract bool IsAdmin(string email);

		/// <summary>
		/// Determines whether the specified user with the given email/username is an editor.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <returns>true if the user is an editor; false otherwise.</returns>
		public abstract bool IsEditor(string email);

		/// <summary>
		/// Lists all admins in the system.
		/// </summary>
		/// <returns>A list of email/usernames who are admins.</returns>
		public abstract IEnumerable<string> ListAdmins();

		/// <summary>
		/// Lists all editors in the system.
		/// </summary>
		/// <returns>A list of email/usernames who are editors.</returns>
		public abstract IEnumerable<string> ListEditors();

		/// <summary>
		/// Signs the user out with (typically with <see cref="FormsAuthentication"/>).
		/// </summary>
		public abstract void Logout();

		/// <summary>
		/// Resets the password for the user with the given email.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <returns>The new randomly generated password</returns>
		public abstract string ResetPassword(string email);

		/// <summary>
		/// Creates a user in the system without setting the <see cref="User.IsActivated"/>, in other words for a user confirmation email.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <param name="password">The password for the user.</param>
		/// <param name="completed">Called once the signup (e.g. email is sent) is complete. Pass Null for no action.</param>
		/// <returns>The activation key for the signup.</returns>
		public abstract string Signup(string email, string password, Action completed);

		/// <summary>
		/// Adds or remove the user with the email address as an admin.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		public abstract void ToggleAdmin(string email);

		/// <summary>
		/// Adds or remove the user with the email address as an editor.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		public abstract void ToggleEditor(string email);

		/// <summary>
		/// Determines whether the user with the given email exists.
		/// </summary>
		/// <param name="email">The email address or username of the user.</param>
		/// <returns>true if the user exists;false otherwise.</returns>
		public abstract bool UserExists(string email);	
	}
}
