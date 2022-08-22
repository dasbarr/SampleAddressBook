using System;
using System.Collections.ObjectModel;

namespace SampleAddressBook;

/// <summary>
/// Common interface for data providers.
/// </summary>
public interface IDataProvider<T> : IDisposable
{
    //-------------------------------------------------------------
    // Properties
    //-------------------------------------------------------------

    /// <summary>
    /// Read-only data for visualization.
    /// </summary>
    ReadOnlyObservableCollection<T> Data { get; }

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    /// <summary>
    /// Adds an entry to the data provider.
    /// </summary>
    /// <param name="entry">Entry to add.</param>
    void AddEntry(T entry);
    /// <summary>
    /// Removes an entry from the data provider.
    /// </summary>
    /// <param name="entry">Entry to remove.</param>
    void RemoveEntry(T entry);
    /// <summary>
    /// Notifies the data provider that the entry was modified.
    /// </summary>
    /// <param name="entry">Modified entry.</param>
    void UpdateEntry(T entry);
}
