namespace Structure.Interface
{
    using SQLite;

    public interface ISqLiteDb
    {
        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();
    }

}
