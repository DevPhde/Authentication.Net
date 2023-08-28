using Authentication.Net.Domain.Exceptions;
using Authentication.Net.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Authentication.Net.Domain.Entities
{
	public sealed class User
	{
		public int Id { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public string FullName { get; private set; }
		public string Email { get; private set; }
		public string Cpf { get; private set; }
		public string Password { get; private set; }
		public bool IsEmailConfirmed { get; private set; } = false;
		public bool IsActive { get; private set; } = true;
		public bool IsAdmin { get; private set; }
		public string Auth { get; private set; } = string.Empty;

		public User(int id, string fullName, string email, string cpf, string password, bool isAdmin = false)
		{
			DomainExceptionValidation.When(id < 0, "Invalid Id. Non Negative Number is required.");
			Id = id;
			UserValidation(fullName, email, cpf, password);
			IsAdmin = isAdmin;
		}
		public User(string fullName, string email, string cpf, string password, bool isAdmin = false)
		{
			UserValidation(fullName, email, cpf, password);
			IsAdmin = isAdmin;
		}
		public void UserHashPassword(string password)
		{
			if(string.IsNullOrWhiteSpace(password))
			{
				throw new InternalErrorException("Criptography sytem is unavailable. Please, Notify the support.");
			}
			Password = password;
		}
		public void UserConffirmAccount()
		{
			IsEmailConfirmed = true;
		}
		public void UpdateAuth(string token)
		{
			Auth = token;
		}
		public void updatePassword(string password)
		{
			Password = password;
		}
		public void IsUserEnabled(bool param)
		{
			IsActive = param;
		}
		private void UserValidation(string fullName, string email, string cpf, string password)
		{
			DomainExceptionValidation.When(string.IsNullOrWhiteSpace(fullName) || fullName.Length < 5, "Invalid Name. Name is required and must have more than 5 characters.");
			DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"), "Invalid Email. Valid email is required.");
			DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpf) || cpf.Length != 14, "Invalid CPF. Valid CPF is required.");
			DomainExceptionValidation.When(string.IsNullOrWhiteSpace(password), "Invalid password. Valid password is required.");

			FullName = fullName;
			Email = email;
			Cpf = cpf;
			Password = password;
		}
	}
}
