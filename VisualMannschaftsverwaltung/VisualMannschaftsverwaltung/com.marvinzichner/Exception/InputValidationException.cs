using System;

public class InputValidationException : Exception
{
    #region Eigenschaften
    private string _causedBy;
    private string _humanReadable;
    private string _field;
    private string _pattern;
    #endregion

    #region Accessoren / Modifier
    public string CausedBy { get => _causedBy; set => _causedBy = value; }
    public string HumanReadable { get => _humanReadable; set => _humanReadable = value; }
    public string Field { get => _field; set => _field = value; }
    public string Pattern { get => _pattern; set => _pattern = value; }
    #endregion

    #region Konstruktoren
    public InputValidationException()
    {
    }

    public InputValidationException(string message, string by, string field, string pattern)
        : base(message)
    {
        this.HumanReadable = message;
        this.CausedBy = by;
        this.Field = field;
        this.Pattern = pattern;
    }

    public InputValidationException(string message, Exception inner)
        : base(message, inner)
    {
        
    }
    #endregion

    #region Worker
    #endregion
}