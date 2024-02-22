Que comportamiento se quiere desarrollar:

Nuestro chatBot tiene como fin el dar definiciones de palabras en ingles. 

Intents y sus regex:

- Saludo/Bienvenida:
            r'.*hola.*',
            r'.*buen(a|o)s (dias|tardes|noches).*',
            r'.*que (tal|tranza|paso).*',

- Preguntar la definicion de una palabra:
            r'.*que significa (.*)\??$',
            r'.*que es (.*)$\??$',
            r'.*que\s+significa\s+[a-zA]+?\?',
            r'.*cual\s+es\s+(el|la)\s+(significado|definicion)\s+de\s+[a-zA]+?\?',
            r'.*que\s+quiere\s+decir\s+[a-z]+?\?'


Miembros del equipo:

Garcia Rivera Adrian
Zavala Hernandez Angel Boris
Menchaca Carrillo Josué

### Link para invitar Bot
https://discord.com/api/oauth2/authorize?client_id=1206667741361209415&permissions=412317370432&scope=bot
