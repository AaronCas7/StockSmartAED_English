from flask import Flask, jsonify
from azure.cosmos import CosmosClient

import os
from dotenv import load_dotenv

# Creamos una instancia de Flask
app = Flask(__name__)   #Equiparable a escribir lo que contiene esa variable, que es __main__

# Cargar variables de entorno desde .env
load_dotenv()

# Obtener las variables de entorno
endPoint = os.getenv("ENDPOINT")
key = os.getenv("KEY")
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
    return jsonify(items)
   

# Ruta: http://dominio.com/productos/34                         
@app.route("/productos/<int:id>", methods=["GET"])
def product_get(id):
    
    # Obtener el producto por ID desde CosmosDB
    query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
    items = list(container.query_items(query, enable_cross_partition_query=True))  # Hay que habilitar las consultas entre particiones cruzadas
        
    return jsonify(items)  # Devolver el primer producto encontrado como JSON
        
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

    # Intenta eliminar el producto con el ID especificado
    query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
    items = list(container.query_items(query, enable_cross_partition_query=True))
    
    if items:
        container.delete_item(item=items[0]['id'], partition_key=items[0]['ProductID'])
        return f"Eliminado el producto {id} correctamente", 200
    
    else:
        return f"No se encontró el producto con ID {id}", 404


################################################################
# Ejecutar la aplicación de Flask en el servidor web integrado #
################################################################
if(__name__ == "__main__"):             #Esto no sería necesario, se puede hacer app.run() directamente
    app.run()
    
    
