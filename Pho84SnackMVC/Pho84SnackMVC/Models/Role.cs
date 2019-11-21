namespace Pho84SnackMVC.Models
{
   public class Role
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }

      public Role(long id, string name, string description)
      {
         Id = id;
         Name = name;
         Description = description;
      }

      public const string RoleBasic = "BASIC";
      public const string RoleAdmin = "ADMIN";
   }
}