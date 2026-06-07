using System.Diagnostics;
using System.Runtime.InteropServices;

public static class CommandHandler
{
    public static void ExecuteCommand(string command, string? key, string? value)
    {
        switch(command)
        {
            case "add":
                if(key == null || value == null) return;
                BookmarkManager.Add(key, value);
                return;
            case "remove":
                if(key == null) return;
                BookmarkManager.Remove(key);
                return;
            case "list":
                Dictionary<string, string> bookmarks = BookmarkManager.List();
                foreach ((string bookmarkKey, string bookmarkValue) in bookmarks)
                {
                    Console.WriteLine(bookmarkKey + " " + bookmarkValue);
                }
                return;
            case "help":
                Console.WriteLine("Usage: bookmark add <key> <directory> : Bookmarks a new directory");
                Console.WriteLine("       bookmark remove <key>          : Removes a directory from bookmarks");
                Console.WriteLine("       bookmark list <key>            : List all available bookmarks");
                Console.WriteLine("       bookmark <key>                 : Opens a new terminal in the bookmarked directory");
                Console.WriteLine("       bookmark help                  : Shows usage help");
                return;
            default:
                string commandToExecute = BookmarkManager.Get(command);
                Console.WriteLine(commandToExecute);
                ExecuteCommand(commandToExecute);
                return;
        }
    }

    private static void ExecuteCommand(string command)
    {
        ProcessStartInfo info;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}"
            };
        }
        else
        {
            info = new ProcessStartInfo
            {
                FileName = "/bin/sh",
                Arguments = $"-c \"{command}\""
            };
        }

        Process.Start(info)?.WaitForExit();
    }
}