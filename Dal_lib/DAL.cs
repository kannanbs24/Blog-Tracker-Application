using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_lib
{
    
        public class AdminInfo
        {
            [Required()]
            [EmailAddress]
        [Key]
            public string EmailId { get; set; }
            [Required()]
            public string Password { get; set; }

        }
        /// <summary>
        /// ///////////
        /// </summary>
        public class EmpInfo
        {
            [Required()]
            [EmailAddress]
        [Key]
        public string EmailId { get; set; }
            [Required()]
            [MaxLength(50, ErrorMessage = "Maximum 50 Characters only")]
            public string Name { get; set; }
            [Required()]
            public DateTime DateOfJoining { get; set; }
            [Required()]
            public int PassCode { get; set; }

        }
        /// <summary>
        /// ////////
        /// </summary>
        public class BlogInfo
        {
            [Required()]
        [Key]
        public int BlogId { get; set; }
            [Required()]

            [MaxLength(20, ErrorMessage = "Maximum 50 Characters only")]
            public string Title { get; set; }
            [Required()]
            [MaxLength(50, ErrorMessage = "Maximum 50 Characters only")]
            public string Subject { get; set; }
            [Required()]

            public DateTime DateOfCreation { get; set; }
            [Required()]

            public string BlogUrl { get; set; }
            [Required()]
           
            public string EmpEmailId { get; set; }
        }
        /// <summary>
        /// /////////
        /// </summary>
        public class MyContext : DbContext
        {
            public MyContext() : base("MyContext")
            {
                Database.SetInitializer(new Init());
                //  Database.SetInitializer<MyContext>(new DropCreateDatabaseIfModelChanges<MyContext>());
            }
            public virtual DbSet<AdminInfo> AdminInfos { get; set; }
            public virtual DbSet<EmpInfo> EmpInfos { get; set; }
            public virtual DbSet<BlogInfo> BlogInfos { get; set; }
        }
        /// <summary>
        /// /////////
        /// </summary>
        public class Init : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {
                List<AdminInfo> adminlist = new List<AdminInfo>();
                adminlist.Add(new AdminInfo() { EmailId = "kannan@gmail.com", Password = "kannan123" });
                context.AdminInfos.AddRange(adminlist);
                context.SaveChanges();
                base.Seed(context);
            }
        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        public class operation
        {

            MyContext context = null;

            public operation()
            {
                context = new MyContext();
            }
            public List<EmpInfo> GetAllEmp()
            {
                var ans = context.EmpInfos.ToList();
                return ans;
            }
            public void AddEmp(EmpInfo emp)
            {
                context.EmpInfos.Add(emp);
                context.SaveChanges();
            }
            public void EditEmp(string id, EmpInfo emp)
            {
                var ans = context.EmpInfos.ToList().Find(temp => temp.EmailId == id);
                context.EmpInfos.Remove(ans);
                context.EmpInfos.Add(emp);
                context.SaveChanges();
            }
            public void RemoveEmp(string id)
            {
                var ans = context.EmpInfos.ToList().Find(temp => temp.EmailId == id);
                context.EmpInfos.Remove(ans);
                context.SaveChanges();
            }
            /// <summary>
            /// ///////////////////
            /// </summary>
            /// <returns></returns>
            public List<BlogInfo> GetAllblog()
            {
                return context.BlogInfos.ToList();
            }
            public void Addblog(BlogInfo m)
            {
                context.BlogInfos.Add(m);
                context.SaveChanges();
            }
            public void Editbloag(int id, BlogInfo dept)
            {
                var ans = context.BlogInfos.ToList().Find(temp => temp.BlogId == id);
                context.BlogInfos.Remove(ans);
                context.BlogInfos.Add(dept);
                context.SaveChanges();
            }
            public void Removebloag(int id)
            {
                var ans = context.BlogInfos.ToList().Find(temp => temp.BlogId == id);
                context.BlogInfos.Remove(ans);
                context.SaveChanges();
            }


        }
    }





