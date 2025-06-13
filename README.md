Ejercicio 1

a. TLS/HTTPS:

Usa: Encriptación simétrica, asimétrica y hashing.

Dónde: Asimétrica en el handshake, simétrica para transferencias, hashing para integridad.

Por qué: Asegura confidencialidad, autenticación e integridad.

b. WPA2:

Usa: Encriptación simétrica (AES).

Dónde: En la comunicación entre dispositivo y router.

Por qué: Garantiza confidencialidad de los datos inalámbricos.

c. Bitlocker/FileVault:

Usa: Encriptación simétrica.

Dónde: En el disco completo.

Por qué: Protege datos almacenados ante robos o accesos indebidos.

d. Blockchain:

Usa: Hashing (integridad) y criptografía asimétrica (transacciones).

Dónde: Firmado con clave privada y verificado con pública.

Por qué: Permite validación sin intermediarios.

Ejercicio 2

a. Comunicación HTTP:

Petición: método + URL + headers + (opcional) body.

Respuesta: código + headers + (opcional) body.

b. Verbos comunes:

GET: Obtener.

POST: Crear.

PUT: Reemplazar.

PATCH: Modificar.

DELETE: Borrar.

c. Códigos de respuesta:
i. GET con recurso encontrado: 200 OK + objeto en body.
ii. DELETE con fallo en BBDD: 500 Internal Server Error.
iii. PATCH recurso no encontrado: 404 Not Found.
iv. Login incorrecto: 401 Unauthorized.
v. POST con rol no permitido: 403 Forbidden.

Ejercicio 3

Característica

Socket tradicional

WebSocket

Tipo

Bajo nivel

Full-duplex sobre HTTP(S)

Protocolo

TCP/UDP

Upgrade desde HTTP

Seguridad

SSL/TLS manual

TLS incorporado

Uso

Aplicaciones personalizadas

Aplicaciones en tiempo real

Técnicas de seguridad: TLS, autenticación, certificados, firewalls.

Ejercicio 4

a. Roles y privilegios:

Estudiante: ver apuntes, comentar, entregar trabajos.

Profesor: publicar apuntes, comentar, poner notas, censurar.

Jefatura: crear cursos, asignar personas, publicar anuncios.

Secretaría: alta/baja de alumnos.

Seguridad: accede a funcionalidades pero no a datos sensibles.

b. Políticas de contraseña:

Estudiante: mínimo 8 caracteres, una mayúscula y un número.

Profesor/admin: mínimo 12, cambio cada 3 meses, incluir símbolo.

c. Clasificación de datos:

Nivel alto: cuenta bancaria, contacto emergencia (cifrado fuerte, acceso restringido).

Nivel medio: notas, entregas (cifrado, acceso autenticado).

Nivel bajo: nombre, nacionalidad (control básico de acceso).

d. Backups:

Incrementales diarios.

Completos semanales.

Ejercicio 8

1. [HttpDelete("login")] (Línia 1)
Error: Es fa servir el mètode HTTP DELETE per a una acció de login.

Per què és un error: El login és una acció de lectura/autenticació, no d’eliminació. El mètode adequat seria POST.

Principi vulnerat: Correcte ús dels mètodes HTTP (RESTful design).

2. Comparació incorrecta entre correus (Línia 5)
csharp
Copiar
Editar
if (usuari.Email != user.Email)
Error: Aquesta comprovació és innecessària, perquè si FindByEmailAsync retorna un usuari, ja és perquè l’email coincideix.

Error addicional: Si usuari és null (usuari no trobat), accedir a usuari.Email llençarà una excepció.

Principi vulnerat: Gestió segura d’errors i validació d’inputs.

3. Logging d’informació sensible (Línia 14–16)
csharp
Copiar
Editar
_logger.Information("Usuari {usuari.UserName} amb id {usuari.Id.ToString()} i password {usuari.password} ha fet logging amb èxit!");
Error: S’està mostrant la contrasenya (usuari.password) al log.

Per què és greu: Les contrasenyes mai s’han de registrar ni mostrar.

Principi vulnerat: Confidencialitat — mai exposar informació sensible.

4. Retornar l’objecte sencer usuari a la resposta (Línia 19)
csharp
Copiar
Editar
return Ok(usuari);
Error: Pot incloure propietats sensibles (com el hash de la contrasenya, rols, etc.).

Principi vulnerat: Exposició mínima d’informació. Sempre cal retornar DTOs limitats.

5. No es verifica la contrasenya (no hi ha cap PasswordCheck)
Error: No hi ha cap línia que comprovi si la contrasenya subministrada per l’usuari és correcta.

Principi vulnerat: Autenticació incompleta. S’està validant només l’existència de l’email.

6. Possible manca de protecció contra atacs de força bruta
Error: No s’observa cap sistema de limitació de peticions ni logging de intents fallits.

Principi vulnerat: Prevenció d’abús (Rate Limiting / Lockout). És important per seguretat en login.

✅ Codi corregit seguint principis de programació segura
csharp
Copiar
Editar
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
{
    var usuari = await _userManager.FindByEmailAsync(user.Email);

    if (usuari == null || !await _userManager.CheckPasswordAsync(usuari, user.Password))
    {
        _logger.Warning("Intent fallit d'inici de sessió per a l'email: {Email}", user.Email);
        return Unauthorized("Credencials incorrectes.");
    }

    var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, usuari.UserName),
        new Claim(ClaimTypes.NameIdentifier, usuari.Id.ToString())
    };

    var token = CreateToken(claims.ToArray());

    _logger.Information("Usuari {UserName} ha iniciat sessió correctament.", usuari.UserName);

    var response = new
    {
        Token = token,
        UserName = usuari.UserName,
        Email = usuari.Email
    };

    return Ok(response);
}

Copias en otra ubicación.

Control de versiones y cifrado.

