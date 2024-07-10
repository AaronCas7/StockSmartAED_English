# from flask import Flask, request, Response, jsonify
# from azure.cosmos import CosmosClient



# # Creamos una instancia de Flask
# app = Flask(__name__)   #Equiparable a escribir lo que contiene esa variable, que es __main__

# connectionString = ""

# endPoint = ""
# key = ""

# dbName = "cosmosAED"
# containerName = "products"

# cosmosClient = CosmosClient(endPoint, key)
# db = cosmosClient.get_database_client(dbName)
# container = db.get_container_client(containerName)


# ################################
# # Rutas de la aplicación Flask #
# ################################

# # Ruta: http://dominio.com/productos    
# @app.route("/productos", methods=["GET"])                  
# def products_get():
    
#     items = list(container.read_all_items())
#     return items


# # Ruta: http://dominio.com/productos/34                         
# @app.route("/productos/<id>", methods=["GET"])   
# def product_get(id, category_id):

#     item = container.read_item(item=id, partition_key=category_id)
#     return jsonify(item)


# # Ruta: http://dominio.com/productos     
# @app.route("/productos", methods=["POST"])
# def products_post():

#     data = "Insertando producto"
#     return data


# # Ruta: http://dominio.com/productos     
# @app.route("/productos/<id>", methods=["PUT"])
# def products_put(id):

#     data = f"Actualizado el producto {id}"  #mirar lo de cambiar el nombre por el id
#     return data


# # Ruta: http://dominio.com/productos/34                    
# @app.route("/productos/<id>", methods=["DELETE"])   
# def products_delete(id):

#     data = f"Eliminado el producto {id}"  #mirar lo de cambiar el nombre por el id
#     return data


# ################################################################
# # Ejecutar la aplicación de Flask en el servidor web integrado #
# ################################################################
# if(__name__ == "__main__"):             #Esto no sería necesario, se puede hacer app.run() directamente
#     app.run()