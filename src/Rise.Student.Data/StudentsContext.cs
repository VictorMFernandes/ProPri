using Microsoft.EntityFrameworkCore;
using Rise.Core.Data;
using Rise.Students.Domain;

namespace Rise.Students.Data
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