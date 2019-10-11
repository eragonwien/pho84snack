namespace Pho84SnackMVC.Models.ViewModels
{
   public class LinksEntryViewModel
   {
      public string Action { get; set; }
      public long Id { get; set; }
      public string DisplayName { get; set; }

      public LinksEntryViewModel(string action, long id, string displayName)
      {
         Action = action;
         Id = id;
         DisplayName = displayName;
      }
   }
}