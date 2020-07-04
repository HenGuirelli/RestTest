# RestTest - Aplicação para automatizar testes de API Rest

## Como usar
A aplicação lê os testes baseado em um arquivo de configuração JSON:
```json
{
    "name": "sum",
    "url": "http://localhost:59635/sum",
    "type": "unique_test",
    "method": "POST",
    "header": {
      "Transfer-Encoding": "chunked",
      "Content-Type": "application/json; charset=utf-8",
      "Server": "Microsoft-IIS/10.0",
      "X-Powered-By": "ASP.NET"
    },
    "body": {
      "num1": 10,
      "num2": 20
    },

    "validation": {
      "body": {
        "result": 30
      }
    }
}
  ```

Para executar os testes:
```
RestTest.ConsoleApp.exe .\config.json .\test_result
```