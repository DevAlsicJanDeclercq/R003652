# R003652

## Prerequisites

- Dotnet 8.xx

## Build and run

Terminal 1:

``` powershell
git clone <cloneurl>
dotnet run
```

## Execute FileList

Terminal 2:

``` powershell
curl localhost:5000/FileList
```

**Result:**

```json
[{"modified":"2024-02-23T08:12:30+00:00","name":"/test1"},{"modified":"2024-02-23T08:12:35+00:00","name":"/test2"}]
```

## Execute PublicIP

``` powershell
curl localhost:5000/PublicIP
```

**Result:**

```json
"xxx.xxx.xxx.xxx"
```
