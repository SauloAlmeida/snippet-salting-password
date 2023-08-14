# Salting Password (Snippet project)

## Motivação
Ter uma implementação de boa prática de segurançaa.

## Como funciona
Ao armazenar uma conta, a senha informada é criptografada juntamente com um valor aleatório `salt` gerado.
Com isso, em vez de ter apenas a `senha criptografada armazenada`, o `salt` também é salvo para que seja feita a combinação (`hash` + `salt`) para verificar a integridade da senha.

### Código
1. KEY_SIZE => O tamanho do hash/salt em bytes.
2. ITERATIONS => Número de interações para gerar um hash, quanto maior melhor a segurança, no entanto, leva mais tempo para calcular o hash.
3. HashAlgorithm => Algoritmo usado para gerar o hash.
4. `salt = RandomNumberGenerator.GetBytes(KEY_SIZE)` => Gera um valor aleatório do tamanho do bytes definido.
5. Hash => É gerado atráves do método Rfc2898DeriveBytes.Pbkdf2.

```csharp
// Application.Service.SecurityService

private const int KEY_SIZE = 64;
private const int ITERATIONS = 350000;
private static HashAlgorithmName HashAlgorithm => HashAlgorithmName.SHA512;

public string GenerateHash(string password, out byte[] salt)
{
    salt = RandomNumberGenerator.GetBytes(KEY_SIZE);

    var passwordBytes = Encoding.UTF8.GetBytes(password);

    var hash = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, 
                                         salt, 
                                         ITERATIONS,
                                         HashAlgorithm, 
                                         KEY_SIZE);

    return Convert.ToHexString(hash);
}
```

Para validação, é verificado a partir da `senha` informada pelo usúario, com os dados armazenados (`hash` + `salt`).

```csharp

public bool ValidateHash(string password, string hash, byte[] salt)
{
    var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, HashAlgorithm, KEY_SIZE);

    return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
}
```

<hr>

### Instalação

1. Clone o projeto

```bash
git clone https://github.com/SauloAlmeida/snippet-salting-password
```

2. Acesse o diretório
```bash
cd SP.SaltingPassword
```

## Rodar o projeto

1. Rode o projeto
```bash
dotnet run
```

2. Veja a rota informada
```bash
[00:00:01 INF] Now listening on: http://localhost:xxxx
```

3. Acesse o swagger
```bash
http://localhost:xxxx/swagger
```
