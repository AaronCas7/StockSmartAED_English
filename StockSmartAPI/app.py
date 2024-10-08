from flask import Flask, jsonify, request, abort
from azure.cosmos import CosmosClient

import os
from dotenv import load_dotenv

# Creamos una instancia de Flask
app = Flask(__name__)   #Equiparable a escribir lo que contiene esa variable, que es __main__

# Cargar variables de entorno desde .env para pruebas en local
#load_dotenv()

# Obtener las variables de entorno
endPoint = os.getenv("ENDPOINT")
key = os.getenv("KEY")
dbName = "cosmosAED"
containerName = "productos"

# Obtener la API Key desde las variables de entorno
apiKey = os.getenv("APIKEY")

# Establecer conexión con CosmosDB
cosmosClient = CosmosClient(endPoint, key)
db = cosmosClient.get_database_client(dbName)
container = db.get_container_client(containerName)

################################
# Rutas de la aplicación Flask #
################################

#Validación de la autorización
@app.before_request
def require_api_key():
    api_key_header = request.headers.get("Authorization")
    if api_key_header != apiKey:
        abort(401)  # Sin Autorización


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
@app.route("/productos/ficha/<int:id>", methods=["GET"])
def product_get(id):
    
    try:
        query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
        
        # Hay que habilitar las consultas entre particiones cruzadas
        items = list(container.query_items(query, enable_cross_partition_query=True))  
            
        return jsonify(items)  # Devolver el primer producto encontrado como JSON
    
    except Exception as e:
        print(f"Error al mostrar producto: {str(e)}")
        return jsonify({"error": "Error al mostrar producto"}), 404
   
        
# Ruta: http://dominio.com/productos     
@app.route("/productos/nuevo", methods=["POST"])
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
@app.route("/productos/<int:id>", methods=["PUT"])
def products_put(id):
    
    try:
        product_data = request.get_json()  # Obtener los datos del producto
        if not product_data:
            return jsonify({"error": "No se proporcionaron datos para actualizar"}), 400

        query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
        items = list(container.query_items(query, enable_cross_partition_query=True))

        if items:
            # Eliminar el documento existente
            container.delete_item(item=items[0]['id'], partition_key=items[0]['CategoryID'])

            # Crear el nuevo documento con la nueva clave de partición
            updated_product = {**items[0], **product_data}
            container.create_item(body=updated_product)

            return jsonify({"message": f"Producto con ID {id} actualizado correctamente"}), 200
        else:
            return jsonify({"error": f"No se encontró el producto con ID {id}"}), 404
    
    except Exception as e:
        print(f"Error al actualizar el producto: {str(e)}")
        return jsonify({"error": "Error al actualizar el producto"}), 400
    

# Ruta: http://dominio.com/productos/34                    
@app.route("/productos/<int:id>", methods=["DELETE"])
def products_delete(id):
    
    try:
        # Intenta eliminar el producto con el ID especificado
        query = f"SELECT * FROM c WHERE c.ProductID = '{id}'"
        
        # Hay que habilitar las consultas entre particiones cruzadas
        items = list(container.query_items(query, enable_cross_partition_query=True))  

        if items:
            product = items[0]
            # Asegúrate de proporcionar todos los componentes de la clave de partición
            partition_key = product.get('CategoryID')
            
            # Si la clave de partición es compuesta, construye el array con todos los componentes necesarios
            # partition_key = [product.get('CategoryID'), product.get('AnotherPartitionKeyComponent')]

            container.delete_item(item=product['id'], partition_key=partition_key)
            return jsonify({"message": f"Eliminado el producto {id} correctamente"}), 200
        else:
            return jsonify({"error": f"No se encontró el producto con ID {id}"}), 404
        
    except Exception as e:
        print(f"Error al eliminar el producto: {str(e)}")
        return jsonify({"error": "Error al eliminar el producto"}), 400


################################################################
# Ejecutar la aplicación de Flask en el servidor web integrado #
################################################################
if(__name__ == "__main__"):
    app.run("host='0.0.0.0'")