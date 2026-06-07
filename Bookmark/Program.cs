if(args.Length == 0) return;

string command = args[0];
string? key = args.Length > 1 ? args[1] : null;
string? value = args.Length > 2 ? args[2] : null;

CommandHandler.ExecuteCommand(command, key, value);
