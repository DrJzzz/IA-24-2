import discord
import respuestas


async def send_message(message, user_message, is_private): // al autor se le manda el mensaje  y mesange contiene muchos
    response=respuestas.get_response(user_massage)
    await message.author.send(response) if is_private else await message.channel.send(response)

//esta funcion es la magica
def run_discord_bot():
    TOKEN='AQUI PONEMOS EL TOKEN DEL BOT'
    intents=discord.Intents.default()
    intents.message_content=True
    client=discord.Client(intents=intents)    // osea que esto sera el contenido del mensaje

@client.event  // esta funcion servira para saber que el bot funciono sin problema 
async def on_ready():
    print(f'{client.user} Esta vivo!!! ')

@client.event  // esta funcion recibira los mensajes
async def on_message(message):   // en estas lineas verifican que el autor del mensaje no sea el mismo bot 
    if message.author== client.user:
        return 

// esto es por si acaso no lo ocupara ahorita pero por si lo queriamos 
//ahi esta 
//dice que el str sirve para limpiar la cadena como de idioma o caracteres para no romper el bot 
username=str(message.author) 
user_message=str (message.content)
channel=str (message.channel)


print(f'{username}mando: "{user_message}"({channel })')

// lo usa para los prefijos para que conteste uso el ?
if user_message[0]=='?':
    user_message=username[1:]
    await send_message(message,user_message,is_private=True)
else :
    await send_message(message,user_message,is_private=False)

client.run(TOKEN)   // Dice que hasta este punto el bot ya debe de jalar
                    // el problema fue mal identado 



// el get_response es lo que responde que si dice hola devuelva hola 
// dice que esto solo sirve mientras la pc este corriendo 

// el import random va en el archivo de respuestas 
// En la parte de formacion de equipos ponerlo ahi ,enviarlo antes de la tarea
// enlace del Bot
debe de tener una dinamica el chat bot


