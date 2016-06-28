namespace ION.Core.Database {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.IO;
  using System.Threading.Tasks;

  using SQLite.Net;
  using SQLite.Net.Interop;

  using ION.Core.App;
  using ION.Core.Database.Deprecated;
  using ION.Core.IO;
  using ION.Core.Util;

  public class IONDatabase : SQLiteConnection, IIONManager {
    /// <summary>
    /// The file/resource name that is used to upgrade the database.
    /// </summary>
    private const string UPGRADE_DATABASE = "UpgradeDatabase.sql";

    public bool isInitialized { get { return __isInitialized; } } bool __isInitialized; 

    /// <summary>
    /// The ion context that this database is running within.
    /// </summary>
    /// <value>The ion.</value>
    public IION ion { get; private set; }

    public IONDatabase(ISQLitePlatform platform, string path, IION ion) : base(platform, path)  {
      this.ion = ion;
      // Create the database
      CreateTable<JobRow>();
      CreateTable<DeviceRow>();
      CreateTable<LoggingDeviceRow>();
      CreateTable<SessionRow>();
      CreateTable<SensorMeasurementRow>();
    }

    /// <summary>
    /// Initializes the IIONManager to the given IION.
    /// </summary>
    /// <returns>The async.</returns>
    public Task<InitializationResult> InitAsync() {
      return Task.FromResult(new InitializationResult() { success = __isInitialized = true });
    }

    /// <summary>
    /// Queries for the item with the given id.
    /// </summary>
    /// <returns>The for async.</returns>
    /// <param name="id">Identifier.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public Task<T> QueryForAsync<T>(long id) where T : class, ITableRow {
      return Task.FromResult(Table<T>().Where(x => x._id == id).First());
    }

    /// <summary>
    /// Queries for all of the items of the given type. 
    /// </summary>
    /// <returns>The for all async.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public Task<List<T>> QueryForAllAsync<T>() where T : class, ITableRow {
      return Task.FromResult(new List<T>(Table<T>().AsEnumerable()));
    }

    /// <summary>
    /// Queries the table count for the given item.
    /// </summary>
    /// <returns>The async.</returns>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public Task<long> CountAsync<T>() where T : class, ITableRow {
      return Task.FromResult((long)Table<T>().Count());
    }

    /// <summary>
    /// Attempts to save the given item to the database.
    /// </summary>
    /// <returns>The async.</returns>
    /// <param name="t">T.</param>
    public Task<bool> SaveAsync<T>(T t) where T : ITableRow {
      BeginTransaction();

      try {
        int affected = 0;
        if (t._id > 0) {
          affected = Update(t);
        } else {
          affected = Insert(t);
        }

        if (affected > 0) {
          Commit();
          return Task.FromResult(true);
        } else {
          throw new Exception("Did not modify the database");
        }
      } catch (Exception e) {
        Log.E(this, "Failed to save the item: " + t + " to the database", e);
        Rollback();
        return Task.FromResult(false);
      }
    }

    /// <summary>
    /// Attempts to delete the given item from the database.
    /// </summary>
    /// <returns>The async.</returns>
    /// <param name="t">T.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public Task<bool> DeleteAsync<T>(T t) where T : ITableRow {
      BeginTransaction();

      try {
        var affected = Delete(t);

        if (affected > 0) {
          Commit();
          return Task.FromResult(true);
        } else {
          throw new Exception("Did not modify the database");
        }
      } catch (Exception e) {
        Log.E(this, "Failed to delete the item: " + t + " to the database", e);
        Rollback();
        return Task.FromResult(false);
      }
    }

    /// <summary>
    /// Performs all of the upgrades necessary for the database.
    /// </summary>
    private async void PerformUpgrade() {
      var sr = new StreamReader(EmbeddedResource.Load(UPGRADE_DATABASE));
      var query = await sr.ReadToEndAsync();
      try {
        Execute(query);
      } catch (Exception e) {
        Log.E(this, "Failed to upgrade database.", e);
      }
    }
  }
}

