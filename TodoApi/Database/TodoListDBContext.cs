using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Database.Auth;
using TodoApi.Models.Database.Todo;

namespace TodoApi.Database
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreationDate").CurrentValue = DateTime.Now;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("UpdatedDate").CurrentValue = DateTime.Now;
            });
            return base.SaveChanges();
        }
    }
}
