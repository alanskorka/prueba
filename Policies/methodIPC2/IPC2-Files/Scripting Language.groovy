import groovy.json.JsonBuilder

def invoke(msg) {
    // Recupera la respuesta de la base de datos del objeto 'msg'
    def respuestaBD = msg.get('respuestaBD')

    // Verifica si 'responseBD' es una lista
    if (!(respuestaBD instanceof List)) {
        throw new IllegalArgumentException("respuestaBD no es una Lista.")
    }

    // Convierte la lista de mapas a JSON usando JsonBuilder
    def jsonBuilder = new JsonBuilder(respuestaBD)
    def jsonMap = jsonBuilder.toPrettyString()

    // Agrega el JSON al 'msg' como 'mapeoFinal'
    msg.put('mapeoFinal', jsonMap)

    return true;
}