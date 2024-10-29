
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller
{
    public class AccountController:BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,ITokenService tokenService){
            _context=context;
            _tokenService=tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTOs>> Register(RegisterDtos registerDtos){
           if(await UserExists(registerDtos.UserName)) return BadRequest("UserName is taken");
           using var hmac=new HMACSHA512();
           var user=new AppUser{
            UserName=registerDtos.UserName,
            PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDtos.Password)),
            PasswordSalt=hmac.Key
           };
           _context.Users.Add(user);
           await _context.SaveChangesAsync();
           return new UserDTOs{
            Username=user.UserName,
            Token=_tokenService.CreateToken(user)           
           };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTOs>> Login(LoginDTOs loginDTOs){
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.UserName==loginDTOs.UserName);
            if(user==null) return Unauthorized("Invalid username");
            var hmac=new HMACSHA512(user.PasswordSalt);
            var ComputedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTOs.Password));
            for(int i=0;i<ComputedHash.Length;i++){
                if(ComputedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password!");
            }
            return new UserDTOs{
            Username=user.UserName,
            Token=_tokenService.CreateToken(user)           
           };
        }
        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}