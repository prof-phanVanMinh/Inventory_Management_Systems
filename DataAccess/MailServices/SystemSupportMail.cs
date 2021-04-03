using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MailServices
{
    class SystemSupportMail:MasterMailServices
    {
            public SystemSupportMail()
        {
            senderMail = "phanvanminh02031994@gmail.com";
            password = "minh1206030261203334";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
    }
}
