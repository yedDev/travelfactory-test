namespace testApp;

public class App
{
    public int ID { get; set; }
    public string Nickname { get; set; }
    public DateTime? LastUpdated { get; set; }
}


public class CreateApp
{
    public string Nickname { get; set; }
}