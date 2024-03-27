namespace testApp;

public interface IAppRepository
{
    ICollection<App> GetApps();
    App GetApp(int ID);
    int CreateApp(string Nickname, DateTime LastUpdated);
    bool AppExists(int ID);
    bool UpdateLastUpdated(int ID, DateTime LastUpdated);
}
