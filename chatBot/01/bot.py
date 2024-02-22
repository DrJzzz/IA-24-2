import os
import discord
from respuestas import Respuestas
from dotenv import load_dotenv

load_dotenv()

async def send_message(message, respuestas, is_private):
    response = respuestas.get_response(message)
    await message.author.send(response) if is_private else await message.channel.send(response)


def run_discord_bot():
    # For this to work you need to create a .env file an populate it with DISCORD_TOKEN=<app token>
    TOKEN = 'MTIwNjY2Nzc0MTM2MTIwOTQxNQ.GtWN8S.vc8QgtY6J9g8k2dBRSbQRpV5EdUEkU5ri1hjOo'
    intents = discord.Intents.default()
    intents.message_content = True
    client = discord.Client(intents=intents)
    respuestas = Respuestas()
    @client.event
    async def on_ready():
        print(f'{client.user} Está vivo!!')

    @client.event
    async def on_message(message):
        if message.author == client.user:
            return

        username = str(message.author)
        user_message = str(message.content)
        channel = str(message.channel)

        print(f'{username} mandó: "{user_message}" ({channel})')

        # Para conversación en privado
        if user_message[0] == '?':
            user_message = user_message[1:]
            await send_message(message, respuestas, is_private=True )
        else:
            await send_message(message, respuestas, is_private=False)

    client.run(TOKEN)
