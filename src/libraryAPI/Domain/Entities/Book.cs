using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Book : Entity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public Guid AuthorId { get; set; } // Foreign Key
    public virtual Author Author { get; set; } = null!; // Bire çok ilişki
    public DateTime PublishedDate { get; set; }
}
