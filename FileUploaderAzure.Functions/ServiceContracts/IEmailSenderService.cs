using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploaderAzure.Functions.ServiceContracts
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends email with link to blob
        /// </summary>
        /// <param name="email">Email to which the message will be sent</param>
        /// <param name="blobUri">Url that should be in the message.</param>
        /// <returns></returns>
        Task SendEmail(string email, string blobUri);
    }
}
