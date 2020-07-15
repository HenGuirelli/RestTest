# RestTest - Aplicação para automatizar testes de API Rest

RestTest é uma ferramenta para facilitar a evolução de API Rest fornecendo uma maneira simples de criar testes automatizados dos Endpoints a partir de um unico arquivo de configuração.

## Como usar
A aplicação lê os testes de um arquivo de configuração JSON:
```json
{
  "name": "sum",
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

## Teste simples
Endpoint em que recebe dois valores no body e a validação é a soma desses números.
```json
{
  "name": "sum",
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
É possivel ter vários testes em um arquivo para testar vários endpoints:
```json
[
  {
    "name": "sum",
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
]
```
A sequencia em que as chamadas acontecem não podem ser garantida.

## Dependências
Também é possivel usar o recurso da dependência, em que pode-se referenciar outro teste.

Quando é detectado uma dependência do teste A que precisa de uma informação do teste B, o teste A automaticamente aguarda a conclusão do teste B para prosseguir.

O body do response da primeira requisição "insert_user" é reaproveitada na URL da segunda requisição "verify_user_exists" e por fim, foi utilizado a clausula "wait" para esperar a conclusao do teste "verify_user_exists" para realizar a operação delete.
```json
[
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
  },
  {
    "name": "delete_user",
    "method": "DELETE",
    "header": {
      "Content-Type": "application/json; charset=utf-8"
    },
    "url": "http://localhost:59635/api/UserCrud/${insert_user.response.body.user_id}",
    
    "wait": "verify_user_exists"
  }
]
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
