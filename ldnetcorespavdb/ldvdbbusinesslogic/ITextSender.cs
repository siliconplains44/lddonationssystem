using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ldvdbbusinesslogic
{
    interface ITextSender
    {
        bool SendSMSThroughProduction(string to, string body);
    }
}
