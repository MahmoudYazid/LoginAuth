using LoginAuth.command;
using LoginAuth.jwt;
using LoginAuth.model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoginAuth.handeler
{
    public class CreateNewUserCommandHandler :IRequestHandler<CreateNewUserCommand , String>
    {
        public MasterContext MasterContext_ { get; set; }

 

        public CreateNewUserCommandHandler(MasterContext masterContext ) {
            MasterContext_= masterContext;
           


        }

        public async Task<string> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {


            var validateUser = await MasterContext_.users.FirstOrDefaultAsync(x => x.Name == request.Name || x.Email == request.Email);

            var NoOfUser = await MasterContext_.users.CountAsync();
            var _NoOfUser =  NoOfUser + 1;

            if (validateUser == null)
            {
                // get Img that aleady uploaded - and save it in the machiene
                
                var GetExtention =Path.GetExtension(request.ImageFile.FileName);
                
                string newName = Path.GetFileNameWithoutExtension(request.ImageFile.FileName) + _NoOfUser.ToString() + Path.GetExtension(request.ImageFile.FileName) ;
                
                var FileFullPath = Path.Combine("C:\\Users\\ahmed\\Desktop\\GIT\\LoginAuth\\LoginAuth\\LoginAuth\\UploadedImg\\", newName);
                
                using (var stream = new FileStream(FileFullPath , FileMode.Create)) {
                    await stream.CopyToAsync(stream);

                
                
                }

                // save new user

                var NewUser = new UserModel
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                    ImageStr = FileFullPath.ToString()



                };

                MasterContext_.users.Add(NewUser);
                await MasterContext_.SaveChangesAsync();
                
                return "user created";


            
            }
            else {

                return "this user exist before";
            }


        }
    }
}
