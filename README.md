# DeepThought
## Overview

DeepThought is a console-based application that simulates a job processing system. It allows users to submit questions, process them using different algorithms, and store the results locally. The system supports asynchronous execution, job persistence, and progress tracking.

## Features

Submit questions with selectable algorithms

List all existing jobs and view their results

Relaunch unfinished jobs

Cancel running jobs with Ctrl + C

Persistent job storage using JSON

Unit tests for strategy and job handling logic

## Project Structure
```bash
DeepThought/
├─ src/
│  └─ DeepThought/
│     ├─ Domain/            # Core models (Job, JobResult)
│     ├─ Services/          # JobRunner, JobStore
│     ├─ Strategies/        # Algorithms (Trivial, SlowCount, RandomGuess)
│     ├─ Util/              # ConsoleHelpers, AppConstants
│     ├─ Program.cs         # Application entry point
│     └─ DeepThought.csproj
└─ tests/
   └─ DeepThought.Tests/
      ├─ JobStoreTests.cs
      ├─ TrivialStrategyTests.cs
      ├─ SlowCountStrategyTests.cs
      ├─ RandomGuessStrategyTests.cs
      └─ DeepThought.Tests.csproj
```
## Requirements

.NET 8.0 SDK or later

Windows, macOS, or Linux

## How to Build and Run

From the project root:
```bash
dotnet clean
dotnet build src/DeepThought/DeepThought.csproj
dotnet run --project src/DeepThought/DeepThought.csproj
```
Running Tests
```bash
dotnet test tests/DeepThought.Tests/DeepThought.Tests.csproj
```
## Example Usage

Start the application:
```bash
dotnet run --project src/DeepThought/DeepThought.csproj
```

Choose an option from the menu:

(1) Submit Question
(2) List Jobs
(3) View Result by JobId
(4) Relaunch Last Incomplete Job
(5) Exit

Relaunch last unfinished job

Exit
