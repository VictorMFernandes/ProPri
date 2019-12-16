using Microsoft.EntityFrameworkCore;
using ProPri.Core.Data;
using ProPri.Students.Domain;

namespace ProPri.Students.Data
{
    public class StudentsContext : DataContext<StudentsContext>
    {
        public DbSet<Student> Students { get; set; }

        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options)
        {
        }
    }
}