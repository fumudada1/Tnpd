# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a C# console application project named "DeleteNotUseFile" targeting .NET Framework 4.6.2. The project appears to be a utility application, possibly for file management operations, though the main functionality is not yet implemented (empty Main method).

## Build and Development Commands

### Building the Project
```bash
# Build in Debug mode
msbuild DeleteNotUseFile.sln /p:Configuration=Debug

# Build in Release mode  
msbuild DeleteNotUseFile.sln /p:Configuration=Release

# Alternative using dotnet (if available)
dotnet build DeleteNotUseFile.sln
```

### Running the Application
```bash
# Run the compiled executable
./bin/Debug/DeleteNotUseFile.exe
# or
./bin/Release/DeleteNotUseFile.exe
```

## Project Structure

- **Program.cs**: Main entry point with empty Main method
- **App.config**: Application configuration including Entity Framework setup and database connections
- **DeleteNotUseFile.csproj**: MSBuild project file with dependencies and build configuration
- **packages.config**: NuGet package dependencies (Entity Framework 5.0.0)
- **Properties/AssemblyInfo.cs**: Assembly metadata and version information

## Key Dependencies

- **Entity Framework 5.0.0**: Used for database operations
- **Target Framework**: .NET Framework 4.6.2
- **Database**: SQL Server connections configured for TnpdDB database

## Configuration

The App.config contains:
- Two database connection strings (TnpdConnection and TnpdConnection2)
- Application settings with SourcePath and DestinationPath configurations
- Entity Framework configuration for LocalDB

## Architecture Notes

This is a simple console application structure with minimal implementation. The project is set up for database operations using Entity Framework but currently contains no business logic. The naming suggests it may be intended for file deletion or cleanup operations based on the SourcePath and DestinationPath settings.