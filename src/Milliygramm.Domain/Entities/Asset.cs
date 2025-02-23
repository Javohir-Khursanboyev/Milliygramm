using Milliygramm.Domain.Commons;
using Milliygramm.Domain.Enums;

namespace Milliygramm.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public FileType FileType { get; set; }
}
