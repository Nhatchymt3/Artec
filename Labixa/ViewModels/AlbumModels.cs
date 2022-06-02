using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation.Mvc;
using FluentValidation.Validators;
using FluentValidation;
using Outsourcing.Data.Models;
namespace Labixa.ViewModels
{
    public class AlbumModels
    {
      public string Url { get; set; }
      public string Id { get; set; }
    }
}