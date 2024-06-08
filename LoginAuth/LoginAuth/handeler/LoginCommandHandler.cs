using LoginAuth.jwt;
using LoginAuth.model;
using LoginAuth.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAuth.handeler
{
    public class LoginCommandHandler : IRequestHandler<LoginQuery , string>
    {
        public MasterContext MasterContext_ { get; set; }

        public JwtSettings Jwtsetting { get; set; }
        public LoginCommandHandler(MasterContext masterContext , JwtSettings jwt) {
            Jwtsetting = jwt;
            MasterContext_ = masterContext;

        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var getUserValidation = await MasterContext_.users.FirstOrDefaultAsync(x =>
            x.Name == request.Name &&
            x.Password == request.Password

            );
            if (getUserValidation != null)
            {
                var GetsymmetricCode = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtsetting.Key));
                var credintial = new SigningCredentials(GetsymmetricCode , SecurityAlgorithms.HmacSha256);

                var Claims = new[] {
                    new Claim( ClaimTypes.NameIdentifier, getUserValidation.Name),
                    new Claim( ClaimTypes.Email, getUserValidation.Email),
                    new Claim( ClaimTypes.Name, getUserValidation.Id),
                    



                };
                var newJwtToken = new JwtSecurityToken(
                    
                    Jwtsetting.Issuer,
                     Jwtsetting.Audience,
                  
                    claims:Claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials : credintial




                    );


                return new JwtSecurityTokenHandler().WriteToken(newJwtToken);



            }
            else {

                return "user not valid"; 
            }



        }
    }
}
