import random
import re
from conocimiento import conocimientoT, error_definiciones, error_chistes
import definicion
import chistes

#Base de conocimiento
base_conocimiento = conocimientoT()
class Respuestas:

	contexto = "DEFAULT"
	def __init__(self):
		"""
		Respuestas consta de una base de conocimiento
		representada como una lista de casos o intents.
		"""
		self.conocimiento = []
		self.chistes = []
		self.definiciones = []
		for caso in base_conocimiento:
			caso['regex'] = list(map(lambda x: re.compile(x, re.IGNORECASE), caso['regex']))
			self.conocimiento.append(caso)


	def get_response(self, message: str) -> str:
		'''
		Flujo básico para identificar coincidencias de intents para responder al usuario.
		Con el texto del usuario como parámetro, los paso a realizarse son:
		1. Encontrar el caso de la base de conocimiento usando expresiones regulares
		2. Si es necesario, realizar acciones asociadas al intent (por ejemplo: consultar información adicional)
		3. Seleccionar una respuesta de la lista de respuestas según el caso del intent
		4. Si es necesario, identificar los parámetros o entidades del texto para dar formato a la respuesta seleccionada
		5. Devolver la respuesta

		:param str message: El texto escrito por el usuario
		:return Un texto de respuesta al usuario
		:rtype: str
		'''
		username = str(message.author) #Cadena de usuario de discord
		user_message = str(message.content).lower() #COntenido del mensaje de discord
		channel = str(message.channel) #Nombre del canal de discord

		caso = self.encontrar_intent(user_message)
		self.identifica_contexto(caso)
		informacion_adicional = self.acciones(caso, user_message)
		respuesta = self.convertir_respuesta(random.choice(caso['respuesta']), caso, user_message, username)
		return respuesta+informacion_adicional



	def encontrar_intent(self, user_input):
		'''
		Encuentra el caso o intent asociado en la base de conocimiento

		:param str user_input: El texto escrito por el usuario
		:return El diccionario que representa el caso o intent deseado
		:rtype: str
		'''
		for caso in self.conocimiento:
			for regularexp in caso['regex']:
				match = regularexp.match(user_input)
				if match:
					self.regexp_selected = regularexp
					return caso
		return {}

	def identifica_contexto(self, caso):
		'''
        Asegura que el contexto sea el adecuado para que
        DiscordBot responde de manera coherente

        :param dict caso: El intent del cual se solicita información
        '''
		intent = caso['intent']
		if intent == 'bienvenida':
			self.contexto = "BIENVENIDA"
		elif intent == 'definicion'	:
			self.contexto = "DEFINICION"
		elif intent == 'chiste':
			self.contexto = "CHISTE"
		elif intent == 'desconocido':
			self.contexto = "DEFAULT"

	def acciones(self, caso,user_input):
		'''
		Obtiene información adicional necesaria para dar una respuesta coherente al usuario.
		El tipo de acciones puede ser una consulta de información, revisar base de datos, generar
		un código, etc. y el resultado final es expresado como una cadena de texto

		:param dict caso: El caso o intent asociado a la respuesta
		:return Texto que representa información adicional para complementar la respuesta al usuario
		:rtype: str
		'''
		intent = caso['intent']
		if intent == 'definicion':
			self.consulta_palabra(caso,user_input)
		elif intent == 'chiste':
			self.consulta_chistes()
		elif intent == 'repetir':
			return self.da_respuesta_apropiada(user_input)
		return ''

	def convertir_respuesta(self, respuesta, caso, user_input, username):
		'''
		Cambia los textos del tipo %1, %2, %3, etc., por su correspondiente propiedad
		identificada en los grupos parentizados de la expresión regular asociada.

		:param str respuesta: Una respuesta que desea convertirse
		:param dict caso: El caso o intent asociado a la respuesta
		:param str user_input: El texto escrito por el usuario
		:param str username: El nombre del usuario de discord
		:return La respuesta con el cambio de parámetros
		:rtype: str
		'''
		respuesta_cambiada = respuesta
		intent = caso['intent']
		match = self.regexp_selected.match(user_input)
		if intent == 'bienvenida':
			respuesta_cambiada = respuesta_cambiada.replace('%1', username)
		elif intent == 'definicion':
			if len(self.definiciones) > 1:
				respuesta_cambiada = respuesta_cambiada.replace('%1', match.group(1))
				respuesta_cambiada = respuesta_cambiada.replace('%2', self.definiciones[1])
			else:
				return random.choice(error_definiciones())
		elif intent == 'chiste':
			if len(self.chistes) > 0:
				respuesta_cambiada = respuesta_cambiada.replace('%1', username)
				respuesta_cambiada = respuesta_cambiada.replace('%2', random.choice(self.chistes))
			else: return random.choice(error_chistes())
		return respuesta_cambiada

	def da_respuesta_apropiada(self, user_input):
		'''
        Devuelve la respuesta según el contexto en el que se encuentre

        :param str user_input: El texto escrito por el usuario
        :return Texto que representa la respuesta
        :rtype str
        '''

		if self.contexto == 'DEFINICION':
			return "Aqui tienes otra definicion\n "+random.choice(self.definiciones)
		elif self.contexto == 'CHISTE':
			return "Espero y este si te haga reir\n "+random.choice(self.chistes)
		else:
			return '¿Podrías tratar de expresarte mejor?'

	def consulta_palabra(self, caso, user_input):
		'''
		Funcion que consulta la palabra en ingles y regresa una lista de definiciones
		:param palabra: la palabra en ingles a consultar en el diccionario
		:return: un arreglo de definiciones
		'''
		match = self.regexp_selected.match(user_input)
		self.definiciones = definicion.consulta(match.group(1))

	def consulta_chistes(self):
		'''
		Funcion que consulta en la base de datos una lista de chistes
		:return:
		'''
		self.chistes = chistes.consulta_chistes()
