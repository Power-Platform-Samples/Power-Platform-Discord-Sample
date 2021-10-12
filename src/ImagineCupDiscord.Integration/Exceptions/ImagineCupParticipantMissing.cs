using System;
using System.Collections.Generic;
using System.Text;

namespace ImagineCupDiscord.Integration.Exceptions
{
    public class ImagineCupParticipantMissing : Exception
    {
        /// <inheritdoc />
        public override string Message => "Participant not found";
    }
}
