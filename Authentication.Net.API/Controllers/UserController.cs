using Authentication.Net.Application.DTOs;
using Authentication.Net.Application.Interfaces;
using Authentication.Net.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Net.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userServices;

		public UserController(IUserServices userServices)
		{
			_userServices = userServices;
		}
		[HttpGet("/confirmation/User/{userIdentifier}")]
		public async Task<IActionResult> ConffirmAccount(string userIdentifier)
		{
			try
			{
				await _userServices.ConffirmAccount(userIdentifier);
				return Ok("Account confirmed.");
			}
			catch (UnauthorizedException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> UserRegister(UserRegisterDTO userRegisterDTO)
		{
			try
			{
				await _userServices.CreateNewUser(userRegisterDTO);
				return Created("Created", userRegisterDTO);
			}
			catch (BadRequestException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (ConflictException ex)
			{
				return Conflict(ex.Message);
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDTO)
		{
			try
			{
				string jwtToken = await _userServices.AuthorizeUser(userLoginDTO);
				return Ok(jwtToken);
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (UnauthorizedException ex)
			{
				return Unauthorized(ex.Message);
			}
		}

		[HttpPost]
		[Route("RecoveryPassword")]
		public async Task<IActionResult> RecoveryPassword(RecoveryPasswordDTO recoveryDTO)
		{
			try
			{
				await _userServices.RecoveryPassword(recoveryDTO.Email);
				return Ok("New password sent to registered email.");
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var user = await _userServices.GetById(id);
				return Ok(user);
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("EnableUser/{id}")]
		[AuthorizationMiddleware]
		public async Task<IActionResult> EnableUser(int id)
		{
			try
			{
				await _userServices.EnableUser(id);
				return Ok($"User ID: {id} Activated.");
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("DisableUser/{id}")]
		[AuthorizationMiddleware]
		public async Task<IActionResult> DisableUser(int id)
		{
			try
			{
				await _userServices.DisableUser(id);
				return Ok($"User ID: {id} Disabled.");
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			try
			{
				await _userServices.DeleteUser(id);
				return Ok($"User ID: {id} Deleted from Database.");
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet]
		[Route("all")]
		[AuthorizationMiddleware]
		public async Task<IActionResult> GetUsers()
		{
			try
			{
				var users = await _userServices.GetUsers();
				return Ok(users);
			}
			catch (InternalErrorException ex)
			{
				return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
			}
		}
	}
}
