using OptionsPattern.Models;

namespace OptionsPattern.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CommandDbContext context)
        {
            if (context.Commands.Any())
            {
                return;
            }
            var gitInit = new Command { Name = "Git Initialize", Comm = "git init" };

            var gitAdd = new Command { Name = "Git Add", Comm = "git add [*.txt, ., -A]" };

            var gitCommit = new Command { Name = "Git Commit", Comm = "git commit -m initial commit" };

            var gitRm = new Command { Name = "Git Remove", Comm = "git rm [*.txt, --cached]" };

            var gitList = new Command { Name = "Git List Files", Comm = "git ls-files" };

            var gitStatus = new Command { Name = "Git Status", Comm = "git status [-s]" };

            context.AddRange(gitInit, gitAdd, gitCommit, gitRm, gitList, gitStatus);
            context.SaveChanges();

        }
    }
}