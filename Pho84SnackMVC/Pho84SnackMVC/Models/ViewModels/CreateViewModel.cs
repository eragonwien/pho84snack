﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class CreateViewModel
   {
      [Required]
      [MaxLength(32)]
      public string Name { get; set; }
      [MaxLength(256)]
      public string Description { get; set; }
   }
}
