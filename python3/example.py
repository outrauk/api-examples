from typing import Any, List

import json
import requests
import os

BASE_URL = "https://api.outra.co.uk/api/v0"


def authenticate_session(session: requests.Session) -> requests.Session:
    login_details = {
        "email": os.environ.get("OUTRA_EMAIL", ""),
        "password": os.environ.get("OUTRA_PASSWORD", ""),
    }
    auth_token = session.post(f"{BASE_URL}/Users/login", data=login_details)

    if auth_token.status_code != 200:
        raise ValueError(
            "Login error - "
            "confirm environment variables OUTRA_EMAIL and OUTRA_PASSWORD are correct."
        )

    session.headers.update({"Authorization": auth_token.json()["id"]})
    return session


def query(session: requests.Session, endpoint: str, filter: dict) -> List[Any]:
    query = session.get(f"{BASE_URL}/{endpoint}", params={"filter": json.dumps(filter)})
    return query.json()


if __name__ == "__main__":
    with requests.Session() as session:
        session = authenticate_session(session)
        properties = query(
            session, endpoint="Properties", filter={"where": {"postcode": "W6 9PF"}}
        )

    print(json.dumps(properties, indent=4))
