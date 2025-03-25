using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Author : Entity<Guid>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!; // User ile bire bir ilişki
    public string Bio { get; set; } = string.Empty;
    public virtual ICollection<Book> Books { get; set; } = new List<Book>(); // Bire çok ilişki
}
