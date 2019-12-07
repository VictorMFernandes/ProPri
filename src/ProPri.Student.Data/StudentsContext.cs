using Microsoft.EntityFrameworkCore;
using ProPri.Core.Data;

namespace ProPri.Students.Data
{
    public class StudentsContext : DataContext<StudentsContext>
    {
        public StudentsContext(DbContextOptions<StudentsContext> options)
            : base(options)
        {
        }
    }
}