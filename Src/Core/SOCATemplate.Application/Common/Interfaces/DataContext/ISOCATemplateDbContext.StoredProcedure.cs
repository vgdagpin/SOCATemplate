using System;
using System.Collections.Generic;
using System.Text;

namespace SOCATemplate.Application.Common.Interfaces
{
    public partial interface ISOCATemplateDbContext
    {
        // Custom context procedures

        DateTime GetCurrentSQLServerTime();
    }
}
