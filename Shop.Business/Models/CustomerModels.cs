﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Business.Models
{
    public class CustomerViewModel
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [Display(Name ="First Name")]
        public virtual string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name ="Last Name")]
        public virtual string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public virtual string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public virtual string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "User Type is required")]
        [Display(Name ="User Type")]
        public virtual UserType UserType { get; set; }
    }
}
