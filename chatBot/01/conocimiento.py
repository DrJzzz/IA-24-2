#----------------------------------------------------------------------
# Base de conocimiento
# La base de conocimiento representa una lista de todos los casos o intents.
#
# Cada caso o intent es un diccionario que incluye los siguientes keys (propiedades):
# - intent: Nombre para identificar el intent
# - regex: Lista de posibles expresiones regulares asociadas al intent, donde los parámetros se obtienen del texto parentizado en la expresión regular
# - respuesta: Lista de posibles respuestas al usuario, indicando los parámetros obtenidos con la notación %1, %2, %3, etc para cada parámetro
#----------------------------------------------------------------------
def conocimientoT():
    '''
    Define la base de conocimiento de glados

    :return El conicimiento a mostrar
    :rtype str
    '''
    conocimiento = [
        {
            'intent': 'bienvenida',
            'regex': [
                r'.*hola.*',
                r'.*buen(a|o)s (dias|tardes|noches).*',
                r'.*que (tal|tranza|paso).*',
            ],
            'respuesta': [
                'Hola, mucho gusto %1, te saluda TBA. Espero y disfrutes conversar conmigo',
                'Hola %1, soy un Bot conversacional de Discord. En que te puedo ayudar?.'
            ]
        },
        {
            'intent': 'definicion',
            'regex': [
                r'.*que significa (.*)\??$',
                r'.*que es (.*)$\??$',
                r'.*que\s+significa\s+(.*)+?\?',
                r'.*cual\s+es\s+(el|la)\s+(significado|definicion)\s+de\s+(.*)+?\?',
                r'.*que\s+quiere\s+decir\s(.*)+?\?',
            ],
            'respuesta': [
                'La %1 sirve para %2',
                'El %1 se usa para %2',
                'El significado de %1 es %2',
                'La definicion de %1 es %2'
            ]
        },
        {
            'intent': 'repetir',
            'regex': [
                r'.*(cuentame|dime|saca)* otr(o|a).*',
            ],
            'respuesta': [
                'Bueno...'
            ]
        },
    ]
    return conocimiento

def error_definiciones():
    '''
    Funcion que regresa un arreglo con posibles respuestas a errores para encontrar la definicion
    :return: array str respuestas de errores
    '''
    conocimiento = [
        "No existe la definicion de esa palabra.",
        "No me fue posible encontrar la palabra, intentalo de nuevo.",
        "No pude encontrar la definicion, verifica si esta escrita correctamente.",
        "Solo se significado de palabras en ingles, verifica que este escrita correctamente."

    ]
    return conocimiento