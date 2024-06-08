using MediatR;

namespace LoginAuth.Query
{
    public class LoginQuery : IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
