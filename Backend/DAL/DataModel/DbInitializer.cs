using System.Data.Entity;
using System.Web.Helpers;


namespace DAL.DataModel
{
    public class DbInitializer : CreateDatabaseIfNotExists<PhotoAlbumContext>
    {
        protected override void Seed(PhotoAlbumContext context)
        {

            var role = new Role
            {
                Name = "user"


            };
            var adminRole = new Role
            {
                Name = "admin"
            };
            var moderRole = new Role()
            {
                Name = "moderator",
            };
            context.Roles.Add(role);
            context.Roles.Add(moderRole);
            context.Roles.Add(adminRole);

            var tag1 = new Tag
            {
                Name = "море"
            };
            var tag2 = new Tag
            {
                Name = "природа"
            };
            var tag3 = new Tag
            {
                Name = "солнце"
            };
            var tag4 = new Tag
            {
                Name = "машины"
            };
            var tag5 = new Tag
            {
                Name = "кошка"

            };
            var tag6 = new Tag
            {
                Name = "животные"
            };
            context.Tags.Add(tag1);
            context.Tags.Add(tag2);
            context.Tags.Add(tag3);
            context.Tags.Add(tag4);
            context.Tags.Add(tag5);
            context.Tags.Add(tag6);
            context.SaveChanges();
        }
    }
}
