using Authentication.Net.Domain.Entities;
using Authentication.Net.Domain.Exceptions;
using Authentication.Net.Domain.Validation;
using FluentAssertions;

namespace Authentication.Net.xUnit.Domain
{
	public class UserUnitTest
	{
		[Theory(DisplayName = "Create new User with valid parameters.")]
		[InlineData(1, "Nome completo", "email@email.com", "123.456.789-00", "password", true)]
		[InlineData(1, "Nome completo", "email@email.com", "123.456.789-00", "password", false)]
		public void CreateUser_WithValidParameters_ReturnValidObjectState(int id, string fullName, string email, string cpf, string password, bool isAdmin)
		{
			Action action = () => new User(id, fullName, email, cpf, password, isAdmin);
			action.Should()
				.NotThrow<DomainExceptionValidation>();
		}
		[Theory(DisplayName = "Create new User without valid parameters.")]
		[InlineData(-1, "Ricardo Menezes", "ricardo@email.com", "123.456.789-00", "password", true)] // negative id
		[InlineData(1, "", "ricardo@email.com", "123.456.789-00", "password", true)] // Name empty
		[InlineData(1, "Ri", "ricardo@email.com", "123.456.789-00", "password", true)] // Name too short
		[InlineData(1, null, "ricardo@email.com", "123.456.789-00", "password", true)] // Name = null
		[InlineData(1, "Ricardo Menezes", "", "123.456.789-00", "password", true)] // Email empty
		[InlineData(1, "Ricardo Menezes", null, "123.456.789-00", "password", true)] // Email = null
		[InlineData(1, "Ricardo Menezes", "email", "123.456.789-00", "password", true)] // Email invalid format 1
		[InlineData(1, "Ricardo Menezes", "email.com", "123.456.789-00", "password", true)] // Email invalid format 2
		[InlineData(1, "Ricardo Menezes", "email@.com", "123.456.789-00", "password", true)] // Email invalid format 3
		[InlineData(1, "Ricardo Menezes", "email@adjska", "123.456.789-00", "password", true)] // Email invalid format 4
		[InlineData(1, "Ricardo Menezes", "email@email.com", "", "password", true)] // CPF empty
		[InlineData(1, "Ricardo Menezes", "email@email.com", null, "password", true)] // CPF = null
		[InlineData(1, "Ricardo Menezes", "email@email.com", "123-456-789-0", "password", true)] // CPF incorrect length
		[InlineData(1, "Ricardo Menezes", "email@email.com", "123.456.789-00", "", true)] // Password empty
		[InlineData(1, "Ricardo Menezes", "email@email.com", "123.456.789-00", null, true)] // Password = null
		public void CreateUser_WithoutValidParameters_DomainExceptionValidation(int id, string fullName, string email, string cpf, string password, bool isAdmin)
		{
			Action action = () => new User(id, fullName, email, cpf, password, isAdmin);
			action.Should()
				.Throw<DomainExceptionValidation>();
		}

		[Fact(DisplayName ="Hash Password Method with valid string.")]
		public void HashPasswordMethod_WithValidParameter_UpdatePropWithPrivateSet()
		{
			User user = new(1, "Ricardo Menezes", "email@email.com", "123.456.789-00", "password", true);
			Action action = () => user.UserHashPassword("HashedPasswordByBcryptProvider");
			action.Should()
				.NotThrow<InternalErrorException>();
		}
		[Theory(DisplayName = "Hash Password Method without valid string.")]
		[InlineData("")]
		[InlineData(null)]
		[InlineData(" ")]
		public void HashPasswordMethod_WithoutValidParameter_InternalErrorException(string password)
		{
			User user = new(1, "Ricardo Menezes", "email@email.com", "123.456.789-00", "password", true);
			Action action = () => user.UserHashPassword(password);
			action.Should()
				.Throw<InternalErrorException>();
		}
	}
}
