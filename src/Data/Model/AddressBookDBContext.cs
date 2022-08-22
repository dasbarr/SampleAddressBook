using Microsoft.EntityFrameworkCore;
using System;

namespace SampleAddressBook;

/// <summary>
/// Database context for local address book database.
/// </summary>
public class AddressBookDBContext : DbContext
{
    //-------------------------------------------------------------
    // Nested
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class constants
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Constructor/finalizer
    //-------------------------------------------------------------

    static AddressBookDBContext()
    {
        // set proper DataDirectory for database connections
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        AppDomain.CurrentDomain.SetData("DataDirectory", baseDir);
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

    public DbSet<AddressBookEntry> AddressBookData { get; set; }

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Properties.Settings.Default.DBConnectionString);
    }

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------
}