﻿using System.ComponentModel.DataAnnotations;

namespace Pho84SnackMVC.Models
{
   public class ProductSize
   {
      public long Id { get; set; }
      public Product Product { get; set; }
      public Size Size { get; set; }
      [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:#.00}", ApplyFormatInEditMode = true)]
      public decimal Price { get; set; }
      public string Currency { get; set; }

      public ProductSize()
      {

      }

      public ProductSize(long id, Product product, Size size, decimal price, string currency = null)
      {
         Id = id;
         Product = product;
         Size = size;
         Price = price;
         Currency = currency;
      }
   }
}