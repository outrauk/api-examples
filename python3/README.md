# Python3 Example

This example uses Python 3 and can be run across platforms (though it's only been tested on macOS).

## Prequisites

* Python version 3.6 or higher
* [requests](https://pypi.org/project/requests/) library 
  * For [Pipenv](https://github.com/pypa/pipenv) users, simply `pipenv install`
* Outra API credentials (email and password)

## Run

This code expects credentials to be set in environment variables:

* `OUTRA_EMAIL` -- email address of the API user
* `OUTRA_PASSWORD` -- password associated with the API user

```bash
OUTRA_EMAIL="user@client.co.uk" OUTRA_PASSWORD="S0m3P4ssw0rd" python example.py
```

If authentication is successful, it'll output properties that match a postcode.
