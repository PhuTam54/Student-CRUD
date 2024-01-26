using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_CRUD.Models;

namespace Student_CRUD.Data
{
    public class Student_CRUDContext : DbContext
    {
        public Student_CRUDContext (DbContextOptions<Student_CRUDContext> options)
            : base(options)
        {
        }

        public DbSet<Student_CRUD.Models.User> User { get; set; } = default!;
    }
}
