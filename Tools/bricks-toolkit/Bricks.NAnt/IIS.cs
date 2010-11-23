using Bricks.Nant;
using NAnt.Core;
using NAnt.Core.Tasks;
using NAnt.Core.Types;

namespace Bricks.NAnt
{
    public class IIS
    {
        private readonly NantProject nantProject;

        public IIS(NantProject nantProject) {
            this.nantProject = nantProject;
        }

        public virtual void Reset()
        {
            Stop();
            Start();
        }

        public virtual void Stop()
        {
            RunCommand("/stop");
        }

        public virtual void Start()
        {
            RunCommand("/start");
        }

        private void RunCommand(string arg)
        {
            nantProject.Log(Level.Info, "iisreset " + arg);
            ExecTask execTask = nantProject.NewTask<ExecTask>();
            execTask.FileName = "iisreset.exe";
            execTask.Arguments.Add(new Argument(arg));
            execTask.Execute();
        }
    }
}