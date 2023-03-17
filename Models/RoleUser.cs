using System;
namespace projet2_CRA.Models
{
	public class RoleUser
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}

