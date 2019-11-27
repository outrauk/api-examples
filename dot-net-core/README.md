# .NET Core 3.0 Example

This example uses .NET Core 3.0 and can be run across platforms (though it's only been tested on macOS).

## Prerequisites

* [.NET Core SDK](https://dotnet.microsoft.com/download)
* Outra API credentials (email and password)

## Run

This code expects credentials to be set in environment variables:

* `OUTRA_EMAIL` -- email address of the API user
* `OUTRA_PASSWORD` -- password associated with the API user

The code does not care how you set those environment variables as long as they're set.

```bash
OUTRA_EMAIL="user@client.co.uk" OUTRA_PASSWORD="S0m3P4ssw0rd" dotnet run
```

If authentication is successful, it'll output properties that match a postcode.

