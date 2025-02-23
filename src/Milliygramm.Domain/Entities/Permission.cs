using Milliygramm.Domain.Commons;

namespace Milliygramm.Domain.Entities;

public class Permission : Auditable
{
    public string Action {  get; set; }
    public string Controller { get; set; }
}
