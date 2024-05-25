# Source API
This is a kind of "framework" to develop console apps with C#.

## Getting Started
In the template project, there are 4 projects, `API`, `App`, `Logger` and `Compiler`, this is because now you can modify the engine to suit your needs.\
The `API` project contains all the classes and logic behind the engine functionality.\
The `App` project its your app's logic, and you can add as many projects as you want. This one by default contains a file called `SourceConfig.json`.\
The `Logger`project contains all the functionality for the Source Logger.\
The `Compiler` project its in charge to create the release build of the final app.\

In your Main method inside of your project, make sure of calling the `Application.Init` method before doing anything.

## SourceConfig.json file
There's a file called `SourceConfig.json` that can be inside of every project inside of the solution (created by you, such as the App project).\
This file contains some needed data:\
`loggerEnabled` Specifies if the Source Logger needs to be initialized, useful for debugging. By default is `true`.\
`mainAppProjName` Specifies the name of the project where the Main method is located. By default is `App`.\
`mainMethodClassName` Specifies the name of the class where the Main method is located. By default is `Program`.

## Content Folder
This folder is located **ONLY** in the Main App Project (defined by the SourceConfig.json file).\
The files located inside this folder can be loaded later using the `Resources` API class.

## Source API Classes and Methods
### Application
`dataPath` Points to the data folder of the app.\
`isFocus` Specifies if this app is the active window.\
`Application.isDebugBuild` Specifies if the app is in a debug build.
`Init() or Init(windowTitle)` Initialize the Source API. Needs to be called at the start of the runtime.\
`Quit() or Quit(exitCode)` Stops the code execution and exits of the app.\
`Run(url)` Runs a local file in the machine or an online URL.
### Debug
`Debug.isLoggerActive` Specifies if the app stills connected to the Source Launcher.\
`Debug.isDebugBuild` Specifies if the app is in a debug build.\
`Debug.Log(obj)` Logs an object if the logger it's active.\
`Debug.LogInfo(obj)` Logs an object as info if the logger it's active.\
`Debug.LogWarning(obj)` Logs an object as a warning if the logger it's active.\
`Debug.LogError(obj)` Logs an object as an error if the logger it's active.
### Resources
`Load<T>(fileName)` Loads a specified file from the "Content" folder if exists.\
`Load(fileName)` Loads a specified file from the "Content" folder if exists.
### Window
`MAIN_WIDTH` Represents a const of the default width of the window.
`MAIN_HEIGHT` Represents a const of the default height of the window.
`width` Represents the width of the window.
`height` Represents the height of the window.
`title` Represents the title of the window.
`SetWindowSize(width, height, setBufferSize)` Sets the window size.

## Compiler
All the App builds created by Visual Studio are marked as `Debug builds`, to create a release build, you need to use the `Source Compiler`, these custom builds have the following structure:
- Content
  - (all content files)
  - resources.data (Contains info about every file inside of the content folder)
- data
  - (all compiled projects)
- default.src (Source Executable File)
### preferences.json file
This file is located inside of the Compiler project, and contains the following properties:\
`showCompilerLog` Specifies if the compiler should show the .csproj files compilation logs. By default is `false`\
`deletePreviousBuild` Specifies if the previous release build should be deleted. By default is `true`.\
`saveLogFile` Specifies if the compilation log should be saved on a file called `compiler.log`. By default is `true`.

## Logger
All this project is inside of ONE single class. This is maded to make a connection with a Source App that allows the execution of a logger.
When the app throws a `Debug.Log()`, `Debug.LogInfo()`, etc. If the app is succesfully connected with a Source Logger, the logger shows the log object.

## Executing the app
See [Source Logger](https://github.com/Javialonqv/Source-Launcher) for more details.
