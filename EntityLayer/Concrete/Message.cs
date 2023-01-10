using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Message
	{
		public Message()
		{
		}

        [Key]
        public int MessageID { get; set; }

        public int? SenderID { get; set; }
        public int? ReceiverID { get; set; }
        public string Subject { get; set; }
        public string MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }

        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }


        public AppUser SenderUser { get; set; }
        public AppUser ReceiverUser { get; set; }
    }
}

