using System;
using System.Collections.Generic;
using System.Text;

namespace ImagineCupDiscord.Integration.Exceptions
{
    /// <summary>
    /// Thrown when a Imagine Cup participant cannot be found (e.g. when they are not registered).
    /// </summary>
    public class ImagineCupParticipantMissingException : Exception
    {
        /// <inheritdoc />
        public override string Message => "Participant not found";
    }
}
