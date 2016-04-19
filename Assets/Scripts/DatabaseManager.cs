
static public class DatabaseManager
{
    static private bool _isInitialized = false;


    static public
    void Initialize( GameSettings.GameSettings settings )
    {
        if ( _isInitialized )
        {
            return;
        }

        _isInitialized = true;

        Database.settings = settings;

    }
}
