﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Models.ViewModels
{
   public class EditFormSingleViewModel
   {
      public long Id { get; set; }
      public string DisplayName { get; set; }
      public string DisplayValue { get; set; }
      public string Url { get; set; }
      public InputFieldType Type { get; set; } = InputFieldType.text;
      public string Method { get; set; } = "POST";

      public EditFormSingleViewModel()
      {
      }

      public EditFormSingleViewModel(long id, string displayName, string displayValue, string url, InputFieldType type = InputFieldType.text)
      {
         Id = id > 0 ? id : 0;
         DisplayName = displayName;
         DisplayValue = displayValue;
         Url = url;
         Type = type;
      }
   }
}