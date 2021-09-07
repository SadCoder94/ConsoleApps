using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultebAPI.DataAccess
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
                            
        }


        public DbSet<DemoData> DemoData { get; set; }
        public DbSet<DemoTable2> DemoTable2 { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Standard> Standard { get; set; }
    }

    public class DemoData
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string label { get; set; }
        public int DID { get; set; }
        
        
        public virtual DemoTable2 DemoTable2 { get; set; }
    }

    public class DemoTable2
    {
        public int Id { get; set; }
        public string RelatedData { get; set; }
        public string RelatedStrings { get; set; }
        [ForeignKey("DID")]
        public virtual ICollection<DemoData> DemoData { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        [ForeignKey("Standard")]
        public int StandardRefId { get; set; }
        public virtual Standard Standard { get; set; }
    }

    public class Standard
    {
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public virtual IList<Student> Students { get; set; }
    }
}
