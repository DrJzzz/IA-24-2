import requests
import re

def consulta_definicion(palabra):
    '''
    Funcion que consulta la definicion de una palabra en ingles en una API
    :return lista de definiciones en una cadena
    :rtype str
    '''
    api_url = 'https://api.api-ninjas.com/v1/dictionary?word={}'.format(palabra)
    response = requests.get(api_url, headers={'X-Api-Key': 'tCKm8LrOO1ieBZ+Qx1iRQg==S3KyircTbnNgFjYz'})
    if response.status_code == requests.codes.ok:
        return response.text
    else:
        return ""

def separa_definicion(respuesta):
    '''
    Funcion que se encarga de limpiar la cadena de respuesta de API y  separarla en un arreglo cada definicion.
    :param respuesta: la respuesta de la API
    :return: EL arreglo de definiciones de la palabra consultada
    :rtype: array str
    '''
    respuesta = re.sub(r'"definition": ', '', respuesta)
    tokens = re.split(r'\d+', respuesta)
    return tokens

def consulta(palabra):
    '''
    Funcion que consulta la palabra en ingles y regresa una lista de definiciones
    :param palabra: la palabra en ingles a consultar en el diccionario
    :return: un arreglo de definiciones
    '''
    res = consulta_definicion(palabra)
    definiciones = separa_definicion(res)
    return definiciones


