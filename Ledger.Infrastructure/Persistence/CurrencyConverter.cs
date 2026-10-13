using Ledger.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Infrastructure.Persistence
{

    public class CurrencyConverter() : ValueConverter<Currency, string>(
        c => c.Code,
        code => new Currency(code));
}
