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

Copias en otra ubicación.

Control de versiones y cifrado.

