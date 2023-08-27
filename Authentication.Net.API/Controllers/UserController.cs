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
		[HttpGet("/confirmation/user/{userIdentifier}")]
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
	}
}
