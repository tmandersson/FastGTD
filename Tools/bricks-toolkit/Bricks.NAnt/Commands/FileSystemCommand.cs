using System.IO;
using Bricks.Nant;
using Bricks.NAnt.Commands.FileSystem;
using NAnt.Core.Tasks;

namespace Bricks.NAnt.Commands
{
    public class FileSystemCommand
    {
        private readonly NantProject nantProject;

        public FileSystemCommand(NantProject nantProject)
        {
            this.nantProject = nantProject;
        }

        public virtual void Delete(DirectoryInfo directoryInfo)
        {
            DeleteTask deleteTask = nantProject.NewTask<DeleteTask>();
            deleteTask.Directory = directoryInfo;
            deleteTask.Execute();
        }

        public virtual void DeleteFile(string fileName)
        {
            DeleteTask deleteTask = nantProject.NewTask<DeleteTask>();
            deleteTask.File = new FileInfo(fileName);
            deleteTask.Execute();
        }

        public virtual void DeleteFile(string directory, string file)
        {
            DeleteFile(FileName.ToString(directory, file));
        }

        public virtual void CopyFile(string fromFile, string toFile)
        {
            CopyTask copyTask = nantProject.NewTask<CopyTask>();
            copyTask.SourceFile = new FileInfo(fromFile);
            copyTask.ToFile = new FileInfo(toFile);
            copyTask.Execute();
        }

        public virtual void CopyFile(string fromDirectory, string fromFile, string toDirectory, string toFile)
        {
            CopyFile(FileName.ToString(fromDirectory, fromFile), FileName.ToString(toDirectory, toFile));
        }
    }
}