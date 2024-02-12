import random

def get_response(message: str) -> str:
	mensaje = message.lower()

	if mensaje == 'hola':
		return '¡Hola!'

	if mensaje == 'dado':
		return str(random.randint(1, 6))

	if mensaje == 'chiste':
		return 'No me se ninguno :c'

	return 'Lo siento, no te entendí'