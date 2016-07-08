The BirdWatcher.dll in this workspace contains a Bird class and a static method for you to be able to get a List of Birds.  Use this if you need a break between videos or need to restart workspaces.  

You can get a list of birds to use with LINQ queries with the following instructions.

1. Enter the C# REPL with csharp (enter).
2. Type in LoadAssembly("BirdWatcher.dll"); (enter)
3. Type in using BirdWatcher; (enter)
4. Type in var birds = BirdRepository.LoadBirds();

Now you should have a birds variable with a list of bird objects!
