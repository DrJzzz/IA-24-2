import random
import re

CTA = "!"

def get_response(message: str) -> str:
	mensaje = message.lower()
	args_arr = message.split()

	if mensaje[0] == '!':
		if re.match( re.escape(CTA) + r"^def\s\w+$", message):
			return f'La definicion de {args_arr[1]} es: ... No me la se jeje'

		if re.match( re.escape(CTA) , r'^wiki\s+\w+(?:\s+\w+)*$' , message ):
			return f'Articulo en wikipedia de {args_arr[1]}...'

		
		return "Estas tratando de usarme? Prueba {CTA}help para saber mas de los comandos disponibles"

	return