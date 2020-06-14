# How to use

## Create config

Create configuration file.

```json
[
  {
    "name": "request one"
    "url": "http://127.0.0.1:8081/api/resource1",
    "validations": {
      "request": { "verb": "GET" },
      "response": {
        "body": { "status": "authenticated" },
        "header": "",
        "query_string": { "code": "" },
        "cookie": {}
      }
    }
  }
]
```



 execute RestTest ".\config.cfg"
