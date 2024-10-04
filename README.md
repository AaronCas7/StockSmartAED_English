<div align="center">

# 📦STOCKSMART AED

**Do you want to see how our solution works? 🚀✨ Click here to watch the 📽️[DEMONSTRATION VIDEO](https://drive.google.com/file/d/1EPu8IPckIjYfR2zHuSAj0SwzfvMaEh0s/view?usp=drive_link)📽️ and discover everything StockSmart AED can do for your business!**

</div>

<div align="center">

### Our Team

|               ![Esperanza Salas González](./StockSmart/wwwroot/images/Esperanza.png)                |                   ![José David Prieto Suárez](./StockSmart/wwwroot/images/JDavid.png)                   |                 ![Aaron Castellano Bao](./StockSmart/wwwroot/images/Aaron.png)                 |
| :-------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------: | :--------------------------------------------------------------------------------------------: |
|                                    **Esperanza Salas González**                                     |                                      **José David Prieto Suárez**                                       |                                    **Aaron Castellano Bao**                                    |
| 🔗[LinkedIn](https://www.linkedin.com/in/esperanzasalasgon/) / 🐙[GitHub](https://github.com/ESG96) | 🔗[LinkedIn](https://www.linkedin.com/in/jdavidprietosuarez/) / 🐙[GitHub](https://github.com/IkonikJD) | 🔗[LinkedIn](https://www.linkedin.com/in/aaron-cb/) / 🐙[GitHub](https://github.com/AaronCas7) |

</div>
<br>

## Solution Description

**StockSmart AED** is a comprehensive solution designed to manage product inventories for companies that sell over the Internet.

Our platform uses Azure technology with an intuitive interface to optimize inventory management, guaranteeing efficiency, accuracy and profitability for your business.

## Architecture Diagram

![Architecture Diagram](./media/diagrama.png)

## Projects

### StockSmart AED

#### **Project:** MVC Web Application

#### **Language/Framework:** C# .NET Core 8.x

The **Products** controller allows:

- Searching products by reference, description and price range
- Inserting new products
- Modifying existing products
- Deleting products

The controller's operation is carried out through the API developed in the _StockSmartAPI_ project. To connect to the API, it requires an **API Key** that is sent as a header and is stored in _Azure Key Vault_. The _App Service_ automatically reads the **API Key** from the _Key Vault_ by setting an environment variable.

### StockSmart API

#### **Project:** Rest API

#### **Language/Framework:** Python 3.xx with Flask

The project is an API that works with an Azure CosmosDB database. To connect to the Cosmos DB requires a **connection string** stored in _Azure Key Vault_. The _App Service_ automatically reads the **connection string** from the _Key Vault_ by setting an environment variable.

Access to the API is done through an **API Key** sent as a header. If the **API Key** is not valid or not present, _401 Unauthorized_ is returned.

The API supports the following methods:

| Request | Status Code Response |
| ------- | -------------------- |
| GET     | 200 OK               |
| POST    | 201 Create           |
| PUT     | 204 No Content       |
| DELETE  | 204 No Content       |

**Product definition in JSON**

```javascript
{"ProductID":"","ProductName":"","SupplierID":"","CategoryID":"","QuantityPerUnit":"","UnitPrice":"","UnitsInStock":"","UnitsOnOrder":"","ReorderLevel":"","Discontinued":""}
```

#### GET

- **Endpoint:** https://domain.com/productos/id/

Returns the matching product with the **id** of the product or _404 Not Found_ if it does not exist in the database.

#### POST

- **Endpoint:** https://domain.com/productos/

Inserts a new product into the database. The body of the message contains the new product data in JSON. Any error will return a _400 Bad Request_.

#### PUT

- **Endpoint:** https://domain.com/productos/id/

Updates the matching product with the product **id**. The body of the message contains the product data in JSON. Any error will return a _400 Bad Request_.

#### DELETE

- **Endpoint:** https://domain.com/productos/id/

Deletes the product matching the product **id**. Any error will return a _400 Bad Request_.

### StockSmart Functions

#### **Project:** Azure Function

#### **Language/Framework:** C# .NET Core 8.x

Records in a table of a _Storage Account_ the data update operations that occur in Cosmos DB, considering this record the LOG of the Rest API application.

To connect to the _Storage Account_ requires a **connection string** stored in _Azure Key Vault_. The _Function Service_ automatically reads the **connection string** from the _Key Vault_ by setting an environment variable.
