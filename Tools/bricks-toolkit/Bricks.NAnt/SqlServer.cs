using System;
using System.IO;
using Bricks.Nant;
using NAnt.Contrib.Tasks;
using NAnt.Contrib.Util;
using NAnt.Core;

namespace Bricks.NAnt
{
    public class SqlServer
    {
        private readonly NantProject nantProject;
        private readonly string connectionString;
        private readonly string sqlServerInstallation;

        public SqlServer(NantProject nantProject, string connectionString)
            : this(nantProject, connectionString, @"C:\Program Files\Microsoft SQL Server")
        {
        }

        public SqlServer(NantProject nantProject, string connectionString, string sqlServerInstallation)
        {
            this.nantProject = nantProject;
            this.connectionString = connectionString;
            this.sqlServerInstallation = sqlServerInstallation;
        }

        public virtual void RestoreDump(FileInfo dumpFile, string databaseName, string originalDumpName)
        {
            string sql =
                string.Format(@"RESTORE DATABASE [{0}] FROM  DISK = N'{1}' WITH  FILE = 1,  
MOVE N'{3}' TO N'{2}\MSSQL.1\MSSQL\DATA\{0}.mdf',  MOVE N'{3}_log' 
TO N'{2}\MSSQL.1\MSSQL\DATA\{0}_log.LDF',  NOUNLOAD,  REPLACE,  STATS = 10", 
databaseName, dumpFile.FullName, sqlServerInstallation, originalDumpName);
            ExecuteSql(sql);
        }

        public virtual void ExecuteSql(string sql)
        {
            string tempSqlFile = Guid.NewGuid() + ".sql";
            File.WriteAllText(tempSqlFile, sql);
            nantProject.Log(Level.Info, sql);
            try
            {
                SqlTask sqlTask = nantProject.NewTask<SqlTask>();
                sqlTask.ConnectionString = connectionString;
                sqlTask.UseTransaction = true;
                sqlTask.Delimiter = ";";
                sqlTask.DelimiterStyle = DelimiterStyle.Normal;
                sqlTask.Source = tempSqlFile;
                sqlTask.Batch = false;
                sqlTask.Execute();
            }
            finally
            {
                File.Delete(tempSqlFile);
            }
        }

        public virtual void Backup(FileInfo dumpFile, string databaseName)
        {
            string sql = string.Format(@"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'{0}-Full Database Backup', SKIP, 
NOREWIND, NOUNLOAD,  STATS = 10", databaseName, dumpFile.FullName);
            ExecuteSql(sql);
        }
    }
}