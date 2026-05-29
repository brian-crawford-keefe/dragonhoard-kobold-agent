# KoboldBrain

A minimal ASP.NET Core backend for code summarization.

## Endpoints

- `POST /summarize` — Accepts code as plain text in the request body and returns a JSON summary.

## Usage

Run with:

```
dotnet run --project KoboldBrain/KoboldBrain.csproj
```

Then POST code to `http://localhost:5077/summarize`.
