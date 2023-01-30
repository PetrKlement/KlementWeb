using System;
using System.Collections.Generic;
using System.Text;

namespace KlementWeb.Business.Interfaces
{
    public interface IEmail   
    {
        void SendEmail(string receiverEmail, string subject, string emailBody, string parameterEmailSender = null);
    }
}
