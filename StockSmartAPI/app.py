from flask import Flask, render_template, request, Response, jsonify, session
import pymssql


#REVISAR CON EL COMMIT lo que no son funciones de ruta

# Creamos una instancia de Flask
app = Flask(__name__)   #Equiparable a escribir lo que contiene esa variable, que es __main__
                                                        

################################
# Rutas de la aplicación Flask #
################################

# Ruta: http://dominio.com/productos    
@app.route("/productos", methods=["GET"])                  
def products_get():
    
    data = "Mostrando todos los productos"
    return data


# Ruta: http://dominio.com/productos/34                         
@app.route("/productos/<id>", methods=["GET"])   
def product_get(id):

    data = f"Mostrando el producto {id}"  #mirar lo de cambiar el nombre por el id
    return data


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