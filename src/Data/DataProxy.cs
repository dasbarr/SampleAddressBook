using System;

namespace SampleAddressBook;

/// <summary>
/// Provides access to address book data.
/// </summary>
public class DataProxy : IDisposable
{
    //-------------------------------------------------------------
    // Nested
    //-------------------------------------------------------------

    public enum DataProviderType
    {
        LocalDB, // address book data stored in the local database
        Json     // address book data stored in JSON file
    }

    //-------------------------------------------------------------
    // Class constants
    //-------------------------------------------------------------

    /// <summary>
    /// That type of data provider will be used if it's impossible to get
    /// an actual setting.
    /// </summary>
    private const DataProviderType cDefaultDataProviderType = DataProviderType.Json;

    //-------------------------------------------------------------
    // Class variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Constructor/finalizer
    //-------------------------------------------------------------

    public DataProxy()
    {
        // get data provider type from the settings
        if (!Enum.TryParse(Properties.Settings.Default.DataProviderType, out DataProviderType dataProviderType))
        {
            SimpleLogger.Error("Can't get data source type from settings, default ("
                               + cDefaultDataProviderType.ToString() + ") will be used");

            dataProviderType = cDefaultDataProviderType;
        }

        DataProvider = dataProviderType switch
        {
            DataProviderType.LocalDB => new LocalDBDataProvider(),
            _                        => new JsonDataProvider()
        };
    }

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Events
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Properties
    //-------------------------------------------------------------

    /// <summary>
    /// Current data provider.
    /// </summary>
    public IDataProvider<AddressBookEntry> DataProvider { get; private set; }

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    public void Dispose()
    {
        if (DataProvider != null)
        {
            DataProvider.Dispose();
            DataProvider = null;
        }
    }

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------
}
