using System;
using System.ComponentModel.DataAnnotations;

namespace projet2_CRA.Models
{
	public class Role
	{

		public int Id { get; set; }

		[MaxLength(30)]
		public string JobLabel { get; set; }

		public RoleType RoleType { get; set; }
	}

	public enum RoleType
	{
		ADMIN, SALARIE, MANAGER
	}
}

