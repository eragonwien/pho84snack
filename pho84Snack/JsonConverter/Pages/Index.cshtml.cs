using JsonConverter.Models;
using JsonConverter.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;

namespace JsonConverter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHostingEnvironment Environment;
        private readonly IConvertService cService;
        private const string DIRECTORY = "Data";
        public IndexModel(IHostingEnvironment environment, IConvertService convertService)
        {
            this.Environment = environment;
            this.cService = convertService;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public string Type { get; set; }

        public List<SelectListItem> Types
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = nameof(Contact), Text = nameof(Contact) },
                    new SelectListItem { Value = nameof(OpenHour), Text = nameof(OpenHour) },
                    new SelectListItem { Value = nameof(Category), Text = nameof(Product) },
                    new SelectListItem { Value = nameof(GalleryItem), Text = "Gallery" },
                    new SelectListItem { Value = nameof(Feature), Text = nameof(Feature) }
                };
            }
        }

        [ViewData]
        public string Message { get; set; }

        [ViewData]
        public bool IsSuccess { get; set; }

        public IActionResult OnPostUpload()
        {
            if (Upload == null && !string.IsNullOrEmpty(Type))
            {
                Message = "Upload is empty";
                return Page();
            }
            var result = cService.ConvertFileToJsonAsync(Type, Upload);

            if (result.IsSuccess)
            {
                result = cService.SaveToFile(Type, result.Content, Path.Combine(Environment.ContentRootPath, DIRECTORY));
                Message = result.IsSuccess ? "File uploaded" : result.ErrorMessage;
                IsSuccess = result.IsSuccess;
            }
            else
            {
                Message = $"Error converting file {result.ErrorMessage}";
                IsSuccess = false;
            }
            
            return Page();
        }

        public IActionResult OnPostDownload()
        {
            // Checks type
            if (string.IsNullOrEmpty(Type))
            {
                Message = "Choose a type";
                return Page();
            }
            string fileName = cService.GetFileName(Type);
            string path = Path.Combine(Environment.ContentRootPath, DIRECTORY, fileName);
            if (System.IO.File.Exists(path))
            {
                Message = "File downloaded";
                IsSuccess = true;
                return PhysicalFile(path, "application/json", fileName);
            }
            Message = $"File {Type}.json not found";
            IsSuccess = false;
            return Page();
        }
    }
}
