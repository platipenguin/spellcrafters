using System;

/// <summary>
/// Exception raised during spell execution. Types of Aberrations are:
/// InsufficientGandalfs
/// NegativeGandalfs
/// ImpossibleNumber
/// </summary>
[Serializable()]
public class AberrationException : Exception
{
    public static string NEGATIVE_GANDALFS = "NegativeGandalfsAberration!";
    public static string INSUFFICIENT_GANDALFS = "InsufficientGandalfsAberration!";
    public static string IMPOSSIBLE_NUMBER = "ImpossibleNumberAberration!";

    public AberrationException() : base() { }
    public AberrationException(string message) : base(message) { }
    public AberrationException(string message, System.Exception inner) : base(message, inner) { }
    protected AberrationException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
