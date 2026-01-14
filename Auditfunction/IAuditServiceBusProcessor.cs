using System;
using System.Collections.Generic;
using System.Text;

namespace Auditfunction
{
    public interface IAuditServiceBusProcessor
    {
         Task SendDataToAuditQueue(string messageBody);
    }
}
