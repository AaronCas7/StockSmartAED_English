from flask import Flask, jsonify, request
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
    
    try:
        items = list(container.read_all_items())
        return jsonify(items)

    except Exception as e:
        print(f"Error al mostrar productos: {str(e)}")
        return jsonify({"error": "Error al mostrar productos"}), 404    

    
# Ruta: http://dominio.com/productos/34                         
@app.route("/productos/<int:id>", methods=["GET"])
def product_get(id):
    
    try:
        query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
        items = list(container.query_items(query, enable_cross_partition_query=True))  # Hay que habilitar las consultas entre particiones cruzadas
            
        return jsonify(items)  # Devolver el primer producto encontrado como JSON
    
    except Exception as e:
        print(f"Error al mostrar producto: {str(e)}")
        return jsonify({"error": "Error al mostrar producto"}), 404
   
        
# Ruta: http://dominio.com/productos     
@app.route("/productos", methods=["POST"])
def products_post():

    try:
        # Obtener los datos del producto desde el cuerpo de la solicitud
        product_data = request.get_json()

        # Insertar el nuevo producto en Cosmos DB
        container.create_item(body=product_data)

        return jsonify({"message": "Producto creado exitosamente"}), 201

    except Exception as e:
        print(f"Error al insertar el producto: {str(e)}")
        return jsonify({"error": "Error al insertar el producto"}), 400


# Ruta: http://dominio.com/productos     
@app.route("/productos/<id>", methods=["PUT"])
def products_put(id):
    
    
    

# Ruta: http://dominio.com/productos/34                    
@app.route("/productos/<id>", methods=["DELETE"])
def products_delete(id):
    
    
        

################################################################
# Ejecutar la aplicación de Flask en el servidor web integrado #
################################################################
if(__name__ == "__main__"):             #Esto no sería necesario, se puede hacer app.run() directamente
    app.run()