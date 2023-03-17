using System;
namespace projet2_CRA.Models
{
	public class Notification
	{
		public int Id { get; set; }

		public string MessageContent { get; set; }
		public string ClassContext { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }
	}
}

