namespace common_mapping.Enums
{
    /// <summary>
    /// Types of connection where saved data of mapping
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// Connecting to MS SQL Server. For database exist special SQL commands in common-mapping/Mappings/MSSQL/Migrate.sql
        /// </summary>
        Database_MSSQL,
        /// <summary>
        /// Connection to SQLite database
        /// </summary>
        Database_SQLite,
        /// <summary>
        /// Connection to common-mapping.API
        /// </summary>
        API
    }
}
