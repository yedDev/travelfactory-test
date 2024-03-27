namespace testApp;

public class AppRepository : IAppRepository
{
    private readonly DataContext _context;
    public AppRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public ICollection<App> GetApps()
    {
        return _context.Apps.OrderBy(a => a.ID).ToList();
    }

    public App GetApp(int ID)
    {
        return _context.Apps.Where(a => a.ID == ID).FirstOrDefault();
    }

    public int CreateApp(string Nickname, DateTime LastUpdated)
    {
        try
        {
            var new_app = new App()
            {
                Nickname = Nickname,
                LastUpdated = LastUpdated,
            };

            _context.Add(new_app);
            bool isSaved = Save();
            if (!isSaved) return 0;
            return new_app.ID;
        }
        catch (System.Exception)
        {
            return 0;
        }
    }

    public bool UpdateLastUpdated(int ID, DateTime LastUpdated)
    {
        try
        {

            var updating_app = _context.Apps.Where(a => a.ID == ID).FirstOrDefault();
            if (updating_app == null)
            {
                return false;
            }

            updating_app.LastUpdated = LastUpdated;

            _context.Update(updating_app);
            return Save();
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool AppExists(int ID)
    {
        return _context.Apps.Any(p => p.ID == ID);
    }

}
