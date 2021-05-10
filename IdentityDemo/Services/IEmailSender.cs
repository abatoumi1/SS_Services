using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentityDemo.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
		/// <summary>
		/// Sends an e-mail to an individual address.  If file attachments are provided, pictures can be placed
		/// inline by specifying %IMG1%, %IMG2%, etc. in the body. These will be replaced by the appropriate image.
		/// </summary>
		/// <param name="to">The address to send to</param>
		/// <param name="subject">The subject of the e-mail</param>
		/// <param name="body">The body of the e-mail.  If HTML formatting is present, the e-mail will be sent as HTML.</param>
		/// <param name="images">If provided, these images will be attached.  Pictures can be placed inline by specifying %IMG1%, %IMG2%, etc. in the body.</param>
		string Send(string to, string subject, string body, IEnumerable<Attachment> images = null);

		/// <summary>
		/// Sends an e-mail to a list of addresses.  If file attachments are provided, pictures can be placed
		/// inline by specifying %IMG1%, %IMG2%, etc. in the body.  These will be replaced by the appropriate image.
		/// </summary>
		/// <param name="to">The addresses to send to</param>
		/// <param name="subject">The subject of the e-mail</param>
		/// <param name="body">The body of the e-mail.  If HTML formatting is present, the e-mail will be sent as HTML.</param>
		/// <param name="images">If provided, these images will be attached.  Pictures can be placed inline by specifying %IMG1%, %IMG2%, etc. in the body.</param>
		string Send(IEnumerable<string> to, string subject, string body, IEnumerable<Attachment> images = null);
	}
}
