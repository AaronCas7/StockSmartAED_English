from flask import Flask, request, Response, jsonify
from azure.cosmos import CosmosClient

# Creamos una instancia de Flask
app = Flask(__name__)   #Equiparable a escribir lo que contiene esa variable, que es __main__

# Datos específicos
endPoint = "https://democosmosdbaed.documents.azure.com/"
key = "qIRhvZF5K53ACmp5QvpKNOYfSaKBR5N1SHPcVxwFmuUbTHSU4NKxGOJuSqSxy9CVQdTji53gc0kIACDbzxB0dw==;"
dbName = "cosmosAED"
containerName = "products"

# Establecer conexión con CosmosDB
cosmosClient = CosmosClient(endPoint, key)
db = cosmosClient.get_database_client(dbName)
container = db.get_container_client(containerName)

################################
# Rutas de la aplicación Flask #
################################

# Ruta: http://dominio.com/productos    
@app.route("/productos", methods=["GET"])                  
def products_get():
    
    items = list(container.read_all_items())   #Probar y decidir si lo dejamos así o como query = "SELECT * FROM c"
    return items
   

# Ruta: http://dominio.com/productos/34                         
@app.route("/productos/<id>", methods=["GET"])   
def product_get(id):

    query = f"SELECT products WHERE ProductID = '{id}'"  #otra idea-> query = "SELECT * FROM c WHERE c.products = '{id}'"
    items = list(container.query_items(query))

    return items

# Ruta: http://dominio.com/productos     
@app.route("/productos", methods=["POST"])
def products_post():

    data = "Insertando producto"
    return data


# Ruta: http://dominio.com/productos     
@app.route("/productos/<id>", methods=["PUT"])
def products_put(id):

    data = f"Actualizado el producto {id}"  #mirar lo de cambiar el nombre por el id
    return data


# Ruta: http://dominio.com/productos/34                    
@app.route("/productos/<id>", methods=["DELETE"])   
def products_delete(id):

    data = f"Eliminado el producto {id}"  #mirar lo de cambiar el nombre por el id
    return data


################################################################
# Ejecutar la aplicación de Flask en el servidor web integrado #
################################################################
if(__name__ == "__main__"):             #Esto no sería necesario, se puede hacer app.run() directamente
    app.run()