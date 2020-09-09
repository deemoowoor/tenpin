# Ten-pin bowling score computer

## Building

### Requirements

.NET Core 3

### Commands

```bash
git clone 
dotnet restore
dotnet build --configuration Release --no-restore
dotnet test --no-restore --verbosity normal
```

### Running

```bash
cd ./tenpin/bin/Release/netcoreapp3.1
./tenpin.exe ./scores.csv
```

