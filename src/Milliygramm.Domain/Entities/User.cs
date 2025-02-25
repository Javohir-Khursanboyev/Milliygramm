using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public sealed class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }    
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserDetail UserDetail {  get; set; }
    public IEnumerable<Chat> Chats { get; set; }
}
