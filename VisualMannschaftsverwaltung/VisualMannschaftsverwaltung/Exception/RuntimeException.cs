using System;

public class RuntimeException : Exception
{
    #region Eigenschaften
    private string _causedBy;
    private string _humanReadable;
    private string _context;
    #endregion

    #region Accessoren / Modifier
    public string CausedBy { get => _causedBy; set => _causedBy = value; }
    public string HumanReadable { get => _humanReadable; set => _humanReadable = value; }
    public string Context { get => _context; set => _context = value; }

    #endregion

    #region Konstruktoren
    public RuntimeException()
    {
    }

    public RuntimeException(string message, string by, string context)
        : base(message)
    {
        this.HumanReadable = message;
        this.CausedBy = by;
        this.Context = context;
    }

    public RuntimeException(string message, Exception inner)
        : base(message, inner)
    {
        
    }
    #endregion

    #region Worker
    #endregion
}