﻿//from Ncqrs
using System;

namespace Hdc
{
    /// <summary>
    /// Generator that generates unique identifiers.
    /// </summary>
    public interface IUniqueIdentifierGenerator
    {
        /// <summary>
        /// Generates the new identifier.
        /// </summary>
        /// <returns>A new <see cref="Guid"/>.</returns>
        Guid GenerateNewId();
    }
}