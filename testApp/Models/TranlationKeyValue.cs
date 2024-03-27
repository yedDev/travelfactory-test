namespace testApp;

public class TranlationKeyValue
{
    public string Key { get; set; }
    public LanguageValue[] ValueArray { get; set; }
}

public class LanguageValue
{
    public string language { get; set; }
    public string value { get; set; }
}
