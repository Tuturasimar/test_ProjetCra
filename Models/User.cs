﻿using System;
using System.ComponentModel.DataAnnotations;

namespace projet2_CRA.Models
{
	public class User
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ doit être rempli.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Ce champ doit être rempli.")]
        public string Password { get; set; }
		public DateTime CreationDate { get; set; }

		public int UserDataId { get; set; }
		public UserData UserData { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

		public int? ManagerId { get; set; }
		public User Manager { get; set; }
    }
}

