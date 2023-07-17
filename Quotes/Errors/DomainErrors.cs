using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Errors
{
    public static class DomainErrors
    {
        public static string CantDeletCharacterThatHaveQuotes = "Can't delete a character while quote from that character still exist";
        public static string CharacterNotFound = "Character not found";
        public static string QuoteNotFound = "Quote not found";
    }
}
