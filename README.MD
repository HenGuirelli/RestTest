# RestTest - Aplicação para automatizar testes de API Rest

RestTest é uma ferramenta para facilitar a evolução de API Rest fornecendo uma maneira simples de criar testes automatizados dos Endpoints a partir de um unico arquivo de configuração.

## Como usar
A aplicação lê os testes de um arquivo de configuração JSON:
```json
{
  "name": "sum",
  "type": "unique_test",
  "url": "http://localhost:59635/sum",
  "method": "POST",
  "header": {
    "Content-Type": "application/json; charset=utf-8"
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

Executar os testes:
```
RestTest.ConsoleApp.exe .\config.json
```

A aplicação suporta dois tipos de testes:
* unique_test
* sequence_test

## Unique Test
O tipo de teste unico é uma chamada independente com validação simples, por exemplo um endpoint em que recebe dois valores no body da requisição e a validação é a soma desses números.
```json
{
  "name": "sum",
  "type": "unique_test",
  "url": "http://localhost:59635/sum",
  "method": "POST",
  "header": {
    "Content-Type": "application/json; charset=utf-8"
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
É possivel ter vários testes unicos em um arquivo para testar vários endpoints:
```json
{
  "name": "sum",
  "type": "unique_test",
  "url": "http://localhost:59635/sum",
  "method": "POST",
  "header": {
    "Content-Type": "application/json; charset=utf-8"
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
},
{
  "name": "sub",
  "type": "unique_test",
  "url": "http://localhost:59635/sub",
  "method": "POST",
  "header": {
    "Content-Type": "application/json; charset=utf-8"
  },
  "body": {
    "num1": 10,
    "num2": 20
  },

  "validation": {
    "body": {
      "result": -10
    }
  }
}
```
A sequencia em que as chamadas unicas acontecem não pode ser garantida.

## Sequence Test
O teste em sequecia é o tipo de teste em que a sequencia das requisições é mantida, esse tipo de teste faz sentido quando se usa o recurso de **dependências** que é a forma de referenciar um teste já concluido, por exemplo:

O body do response da primeira requisição "insert_user" é reaproveitada na Url da segunda requisição "verify_user_exists"

```json
{
  "name": "CrudTest",
  "type": "sequence_test",
  "sequence": [
    {
      "name": "insert_user",
      "method": "POST",
      "url": "http://localhost:59635/api/UserCrud/",
      "header": {
        "Content-Type": "application/json; charset=utf-8"
      },
      "body": {
        "name": "Robson",
        "age": 30
      },
      "validation": {
        "status": 200
      }
    },
    {
      "name": "verify_user_exists",
      "method": "GET",
      "url": "http://localhost:59635/api/UserCrud/${insert_user.response.body.user_id}",
      "header": {
        "Content-Type": "application/json; charset=utf-8"
      },
      "validation": {
        "status": 200,
        "body": {
          "user": {
            "name": "Robson",
            "age": 30,
            "id": "${NUMBER}"
          }
        }
      }
    }
  ]
}
```

## Valores especiais
Há valores especiais que podem ser usados para validações mais genéricas

|Valor |Aceite|
|------|------|
|${ANY}|Qualquer valor|
|${NUMBER}|Qualquer número|
|${Regex: \<regex\>}|Expressões Regulares. Ex: ${Regex: AGE:.?1[7-9]}

No exemplo é passado um header customizado e o teste consiste em validar que a API irá retornar o mesmo valor passado na requisição, a API retorna alguns headers que não são interessantes para o teste então foi utilizado o valor especial "${ANY}" para ignorar a validação nesses campos.
```json
{
  "name": "header",
  "type": "unique_test",
  "url": "http://localhost:59635/name",
  "header": {
    "Content-Type": "application/json; charset=utf-8",
    "Custom-Header": "Value"
  },

  "validation": {
    "header": {
      "Transfer-Encoding": "${ANY}",
      "Server": "${ANY}",
      "X-Powered-By": "${ANY}",
      "Date": "${ANY}",
      "Custom-Header": "Value"
    }
  }
}
```
