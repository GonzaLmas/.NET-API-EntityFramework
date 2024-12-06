# BalearesChallenge





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


EL MAYOR DESAFÍO PERSONAL DURANTE EL DESARROLLO 

Uno de los aspectos más desafiantes del desarrollo de este proyecto fue la implementación de la autenticación con JWT. Aunque ya había oído hablar de JWT, no tenía experiencia práctica con esta tecnología. Tenía conocimiento y experience previa en el uso de cookies para el manejo de usuarios, pero al intentar aplicar esta solución me encontré con dificultades para obtener y retornar el token en los endpoints que requerían autenticación. Esto me llevó a optar por el uso de JWT, lo cual, aunque implicó una curva de aprendizaje, me permitió integrar de manera efectiva un sistema de autenticación robusto y seguro en la aplicación.

