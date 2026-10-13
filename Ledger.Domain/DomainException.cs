using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public sealed class DomainException(string message) : Exception(message);
}
