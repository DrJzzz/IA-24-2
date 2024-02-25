import requests

def consulta_chistes():
    '''
    Funcion que consulta 10 chistes en la APU
    :return: lista de chistes
    :rtype: array
    '''
    api_url = 'https://api.api-ninjas.com/v1/jokes?limit={}'.format(10)
    response = requests.get(api_url, headers={'X-Api-Key': 'tCKm8LrOO1ieBZ+Qx1iRQg==S3KyircTbnNgFjYz'})
    if response.status_code == requests.codes.ok:

        return limpia(response.json())
    else:
        return "Error no se encontraron chistes por el momento"

def limpia(respuesta):
    '''
    Funcion que limpia las cadenas de la lista de chistes consultados
    :param respuesta: lista de chistes en json
    :return: clista de chistes
    :rtype: array
    '''
    lista = []
    for chiste in respuesta:
        lista.append(chiste['joke'])
    return lista