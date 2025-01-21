





DOCUMENTACIÓN PARA LEVANTAR EL PROYECTO

Esta documentación describe cómo clonar el repositorio, abrir el proyecto en Visual Studio, configurar la cadena de conexión y ejecutar la aplicación en HTTPS.

1- Clonar el repositorio
	Abrir la terminal o el símbolo del sistema.

	Clonar el repositorio en la carpeta que desee usando el siguiente comando: git clone 	https://github.com/GonzaLmas/BalearesChallenge.git

	Dentro de la carpta abrir la solución con Visual Studio (idealmente 2022)


2- Configurar la cadena de conexión a la base de datos
	Abrir el archivo "appsettings.json" en el proyecto.

	Modificar la sección de ConnectionStrings según su entorno en SSMS. 
	Si usa autenticación de Windows, la cadena de conexión debe tener la opción de "Integrated Security=True". 
	Si usa autenticación con usuario y contraseña, especificar el "User Id" y la "Password".

	Ejecutar el proyecto






PASOS PARA PROBAR LOS ENDPOINTS:

Al ejecutar el proyecto se va abrir Swagger en el navegador. En él nos vamos a apoyar para probar el proyecto. También vamos a necesitar de Postman para probar los endpoints.

Correos y contraseñas de los usuarios existentes:
Usuario 1: user1@gmail.com; Pass123;
Usuario 2: user2@gmail.com; Pass456;
Usuario 3: user3@gmail.com; Pass789;

Ingresar cómo usuario al sistema:
-	Click en el desplegable /api/Acceso/Ingresar
-	Click en Try it out
-	Dentro del body completar DENTRO DE LAS COMILLAS el correo y la contraseña
-	Click en Execute
-	Copiar el valor del “token” SIN LAS COMILLAS del response body y guardarlo en un lugar de fácil acceso

Crear un contacto:
-	Click en el desplegable /api/Contacto
-	Copiar el json de ejemplo y guardarlo para reutilizarlo luego
-	Abrir Postman, nueva pestaña. Seleccionar método POST y en la url pegar https://localhost/api/Contacto
-	Dentro de “Authorization”, en Auth Type seleccionar “Bearer Token” y en “Token” pegar el token que se guardó al iniciar sesión exitosamente
-	Dentro de “Body”, pegar el json de ejemplo. Completar con datos DENTRO DE LAS COMILLAS. 
-	Click en Send

Obtener todos los contactos:
-	En Postman, ingresar el token
-	Seleccionar el método GET e ingresar la url https://localhost/api/Contacto
-	Click en Send

Obtener contacto por id:
-	En Postman, ingresar el token
-	Seleccionar el método GET e ingresar la url https://localhost/api/Contacto/1 Reemplazar el 1 por el id deseado
-	Click en Send


Actualizar contacto:
-	En Postman, ingresar el token
-	Seleccionar el método PUT e ingresar la url https://localhost/api/Contacto/1 Reemplazar el 1 por el id deseado
-	Click en Send

Borrar contacto:
-	En Postman, ingresar el token
-	Seleccionar el método DELETE e ingresar la url https://localhost/api/Contacto/1 Reemplazar el 1 por el id deseado
-	Click en Send

Buscar contacto por email o número de teléfono:
-	En Postman, ingresar el token
-	Seleccionar el método GET e ingresar la url https://localhost/api/Contacto/buscar?email=isabel@gmail.com&telefono=567890123 Reemplazar el email por el email deseado. Reemplazar el número de teléfono por el número de teléfono deseado
-	Click en Send

Listar contactos ordenados por email:
-	En Postman, ingresar el token
-	Seleccionar el método GET e ingresar la url https://localhost/api/Contacto/ordenados 
-	Click en Send

Cerrar sesión:
-	En Postman, ingresar el token
-	Seleccionar el método POST e ingresar la url https://localhost/api/Acceso/CerrarSesion
-	Click en Send

Registrar usuario:
-	Click en el desplegable /api/Acceso/Registrarse
-	Click en Try it out
-	Dentro del body completar DENTRO DE LAS COMILLAS el correo, contraseña, nombre y apellido
-	Click en Execute


