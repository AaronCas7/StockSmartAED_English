<div align="center">

<<<<<<< HEAD
# :package:STOCKSMART AED 
</div>
=======
# 📦STOCKSMART AED

**¿Quieres ver cómo funciona nuestra solución? 🚀✨ Haz clic aquí para ver el 📽️[VIDEO DEMOSTRATIVO](https://drive.google.com/file/d/1zTWGiYvgJjKeMF3oPAdpKPsfrV4QbMEA/view?usp=drivesdk)📽️ y descubre todo lo que StockSmart AED puede hacer por tu negocio!**

</div>

>>>>>>> cfee71f198fb9bb6e89c0ab5ce639622baf9f40a
<div align="center">

### Nuestro Equipo

|               ![Esperanza Salas González](./StockSmart/wwwroot/images/Esperanza.png)                |                   ![José David Prieto Suárez](./StockSmart/wwwroot/images/JDavid.png)                   |                 ![Aaron Castellano Bao](./StockSmart/wwwroot/images/Aaron.png)                 |
| :-------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------: |
|                                    **Esperanza Salas González**                                     |                                      **José David Prieto Suárez**                                       |                                    **Aaron Castellano Bao**                                    |
| 🔗[LinkedIn](https://www.linkedin.com/in/esperanzasalasgon/) / 🐙[GitHub](https://github.com/ESG96) | 🔗[LinkedIn](https://www.linkedin.com/in/jdavidprietosuarez/) / 🐙[GitHub](https://github.com/IkonikJD) | 🔗[LinkedIn](https://www.linkedin.com/in/aaron-cb/) / 🐙[GitHub](https://github.com/AaronCas7) |

</div>
<<<<<<< HEAD
=======
<br>
>>>>>>> cfee71f198fb9bb6e89c0ab5ce639622baf9f40a

## Descripción de la solución

**StockSmart AED** es una solución integral diseñada para gestionar las existencias de productos de compañías que comercializan a través de Internet.

Nuestra plataforma utiliza tecnología de Azure con una interfaz intuitiva para optimizar la gestión de inventarios, garantizando eficiencia, precisión y rentabilidad para su negocio.

## Diagrama de la Arquitectura

![Diagrama de la Arquitectura](./media/diagrama.png)

## Proyectos

### StockSmart AED

#### **Proyecto:** Aplicación Web MVC

#### **Lenguaje/Framework:** C# .NET Core 8.x

El controlador **Productos** permite:

- Buscar productos por referencia, descripción y rango de precios
- Insertar nuevos productos
- Modificar productos existentes
- Eliminar productos

La operativa del controlador se realiza a través del API desarrollada en el proyecto _StockSmartAPI_. Para conectar con el API requiere un **API Key** que se envia como cabecera y se encuantra almacenado en _Azure Key Vault_. El _App Service_ lee automáticamente la **API Key** del _Key Vault_ mediante la configuración de una variable de entorno.

### StockSmart API

#### **Proyecto:** API Rest

#### **Lenguaje/Framework:** Python 3.xx con Flask

El proyecto es un API que trabaja con una base de datos Azure CosmosDB. Para conectar con el Cosmos DB requiere una **cadena de conexión** almacenada en _Azure Key Vault_. El _App Service_ lee automáticamente la **cadena de conexión** del _Key Vault_ mediante la configuración de una variable de entorno.

El acceso al API se realiza mediante un **API Key** enviada como cabecera. Si el **API Key** no es valido o no esta presente se retorna _401 Unauthorized_.

El API admite los siguientes métodos:

| Request | Status Code Response |
| ------- | -------------------- |
| GET     | 200 OK               |
| POST    | 201 Create           |
<<<<<<< HEAD
| PUT     | 200 OK               |
| DELETE  | 204 OK               |
=======
| PUT     | 204 No Content       |
| DELETE  | 204 No Content       |
>>>>>>> cfee71f198fb9bb6e89c0ab5ce639622baf9f40a

**Definición de producto en JSON**

```javascript
{"ProductID":"","ProductName":"","SupplierID":"","CategoryID":"","QuantityPerUnit":"","UnitPrice":"","UnitsInStock":"","UnitsOnOrder":"","ReorderLevel":"","Discontinued":""}
```

#### GET

- **Endpoint:** https://dominio.com/productos/id/

Retorna el producto coincidente con el **id** del producto o _404 Not Found_ si no existe en la base de datos.

#### POST

- **Endpoint:** https://dominio.com/productos/

Inserta un nuevo producto en la base datos. El cuerpo del mensaje contiene los datos del nuevo producto en JSON. Cualquier error retornara un _400 Bad Request_.

#### PUT

- **Endpoint:** https://dominio.com/productos/id/

Actualiza el producto coincidente con el **id** del producto. El cuerpo del mensaje contiene los datos del producto en JSON. Cualquier error retornara un _400 Bad Request_.

#### DELETE

- **Endpoint:** https://dominio.com/productos/id/

Elimina el producto coincidente con el **id** del producto. Cualquier error retornara un _400 Bad Request_.

### StockSmart Functions

#### **Proyecto:** Azure Function

#### **Lenguaje/Framework:** C# .NET Core 8.x

Registra en una tabla de una _Cuenta de Almacenamiento_ las operaciones de actualización de datos que suceden en Cosmos DB, considerando este registo el LOG de la aplicación API Rest.

Para conectar con la _Cuenta de Almacenamiento_ requiere una **cadena de conexión** almacenada en _Azure Key Vault_. El _Function Service_ lee automáticamente la **cadena de conexión** del _Key Vault_ mediante la configuración de una variable de entorno.
