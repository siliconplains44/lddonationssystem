using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ldvdbbusinesslogic
{
    interface IEmailSender
    {
        bool SendThroughLocalTest(string to, string subject, string body);
        bool SendThroughProduction(string to, string subject, string body);
    }
}
