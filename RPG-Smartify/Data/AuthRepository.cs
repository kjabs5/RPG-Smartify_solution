
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RPG_Smartify.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Smartify.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RPGdbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(RPGdbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        

        public async Task<ResponseData<string>> Login(string username, string password)
        {
            ResponseData<string> res = new ResponseData<string>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user==null)
            {
                res.Message = "user not found";
                res.success = false;
                
            }
            else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                res.Message = "password donot match";
                res.success = false;
               
            }
            else
            {
                res.Data = CreateToken(user);
               
            }
            return res;
        }

        public async Task<ResponseData<int>> Register(User user, string password)
        {
            ResponseData<int> res = new ResponseData<int>();
            if (await UserExist(user.Username))
            {
                res.Message = "user exist";
                res.success = false;
                return res;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
          
            res.Data = user.id;
            res.Message = "user Registered";
            return res;
        }

        public async Task<bool> UserExist(string username)
        {
            if( await _context.Users.AnyAsync(x=>x.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                 new Claim(ClaimTypes.Name, user.Username)
                 
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);

        }
    }
}
