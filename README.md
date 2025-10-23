# IntensitySegments

A console app that manages piecewise constant "intensity" segments over a 1-dimensional axis. It supports adding and setting intensity over intervals and produces a canonical string representation useful for testing and debugging.

- Target: .NET 6
- C# language: 10.0

## Project layout

- `IntensitySegments` — library containing:
  - `IntensitySegments.cs` — main implementation.
  - `StringExtensions.cs` — contains the `Dump()` string extension used by the sample program to write output to the console.
  - `Program.cs` — a small console example demonstrating usage.
- `IntensitySegments.UnitTests` — NUnit unit tests that exercise the expected behaviors.

## Quick features

- `Add(int from, int to, int amount)` — adds `amount` to the intensity on the half-open interval `[from, to)`.
- `Set(int from, int to, int amount)` — sets the intensity to `amount` for `[from, to)`, removing interior breakpoints.
- `ToString()` — returns a canonical representation of breakpoints and intensities: `[[pos1,val1],[pos2,val2],...]`.
- `Dump()` extension — convenience extension that writes the string to the console and returns it for chaining.

## Build and run

Using the .NET CLI:

- Build the solution:
  - `dotnet build`
- Run the console example:
  - `dotnet run --project IntensitySegments`
- Run unit tests:
  - `dotnet test`

## License

No license file included.