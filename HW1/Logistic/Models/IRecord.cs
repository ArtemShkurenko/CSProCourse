using System;


namespace Logistic.ConsoleClient.Models
{
    internal interface IRecord<TId>
        where TId : struct, IEquatable<TId>
    {
        public TId Id { get; set; }
    }
}
