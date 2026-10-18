### EShopService Application

A modular e-commerce backend built with .NET 7 following Clean Architecture principles. The application separates core business domain rules, application use cases, technical infrastructure, and the web presentation boundary. 

### 🚀 API Endpoints Reference

### Products Management

* **GET /api/product** 

  * **Description**: Returns a list of all active products that are not soft-deleted.
  * **Returns**: 200 OK with an array of Product objects.
* **GET /api/product/{id}** 

  * **Description**: Returns a specific active product by its unique identifier.
  * **Returns**: 200 OK with the Product object if found; 404 Not Found if missing or soft-deleted.
* **POST /api/product** 

  * **Description**: Creates and adds a new product to the catalog. Automatically stamps auditing metadata.
  * **Returns**: 201 Created with the newly assigned Id and the generated object payload.
* **PUT /api/product/{id}** 

  * **Description**: Updates structural fields of an existing product.
  * **Returns**: 204 No Content on success; 400 Bad Request if payload is empty; 404 Not Found if the product doesn't exist.
* **DELETE /api/product/{id}** 

  * **Description**: Flags a product as deleted (Deleted = true) without wiping it completely from historical context.
  * **Returns**: 204 No Content on success; 404 Not Found if missing.

### 📂 Project Architecture & Directory Layout

```text
EShopService.sln
│
├── src/
│   ├── EShopService.Domain/            # Central domain logic layer (No dependencies)
│   │   ├── Entities/                   # Enterprise database data shapes & schemas
│   │   ├── Exceptions/                 # Domain-specific isolated business errors
│   │   └── Enums/                      # Constant structural type system descriptors
│   │
│   ├── EShop.Application/              # Pure application business workflows & rules
│   │   └── CardValidatorService.cs     # ISO/IEC credit card format validation mechanics
│   │
│   ├── EShopService.Infrastructure/    # External communication engines & storage mechanics
│   │   ├── Data/                       # Framework contexts & persistence engines
│   │   └── Repositories/               # Implementation boundaries for abstraction contracts
│   │
│   └── EShopService.API/               # Web layout framework routing configuration
│       ├── Controllers/                # Input entry handlers (HTTP mapping layers)
│       └── Middleware/                 # Global HTTP pipeline pipeline injectors
│
└── test/
    ├── EShopService.UnitTests/         # Product domain interaction tests
    └── EShop.Application.Tests/        # Payment data checking validity tests
```



### 🗄️ Domain Models Breakdown

### 1. BaseModel (Abstract Template Class)

All state-persistent business models inherit common enterprise auditing lifecycle footprints from this wrapper: 

* **Id** (int): Primary sequence database index indicator.
* **Name** (string): General text label applied across the system entity.
* **Deleted** (bool): Operational soft-delete flag toggle. Defaults to false.
* **CreatedAt** (DateTime): Timestamp tracking records system creation calendar event.
* **CreatedBy** (Guid): Tracking identification key designating systemic data author.
* **UpdatedAt** (DateTime?): Nullable timestamp marking modification iterations.
* **UpdatedBy** (Guid?): System actor identity stamp responsible for data alterations.

### 2. Product

Represents a physical or virtual commercial item available inside the EShop: 

* *Inherits all properties from BaseModel* (Id, Name, etc.)
* **Ean** (string): International European Article Number barcode mapping string.
* **Price** (decimal): High precision monetary configuration values representation.
* **Stock** (int): Active warehouse tracking quantity metric count. Defaults to 0.
* **Sku** (string): Stock Keeping Unit internal company organization reference key.
* **CategoryId** (int?): Relational link boundary directing toward parent storage categories.
* **Category** (Category?): Structural context navigational pointer object references block.

### 3. Category

Represents a collection group that organizes products into specific domains: 

* *Inherits all properties from BaseModel* (Id, Name, Deleted, etc.)

### ⚙️ Code Implementation & Function Signatures

### 📦 EShopService.API Project

#### ProductController.cs

Handles mapping incoming API requests onto internal in-memory object arrays. 

* **GetAll()** 

  * *Parameters*: None
  * *Returns*: ActionResult<IEnumerable<Product>> (200 OK listing elements matching Deleted == false).
* **GetById(int id)** 

  * *Parameters*: id (int) - The targeted system entity key parameter.
  * *Returns*: ActionResult<Product> (200 OK model structure, or 404 Not Found).
* **Create([FromBody] Product newProduct)** 

  * *Parameters*: newProduct (Product) - Deserialized input structural data context body.
  * *Returns*: ActionResult<Product> (201 CreatedAtAction containing full updated data properties tracking details).
* **Update(int id, [FromBody] Product updatedProduct)** 

  * *Parameters*: id (int) - Identity routing target key; updatedProduct (Product) - Request modifications context body.
  * *Returns*: IActionResult (204 No Content for matching object updates, 400 Bad Request, or 404 Not Found).
* **Delete(int id)** 

  * *Parameters*: id (int) - TARGET configuration key identity index.
  * *Returns*: IActionResult (204 No Content upon applying structural context toggle updates).

### 📦 EShop.Application Project

#### CardValidatorService.cs

Enforces financial integration formatting validations based on system acceptance patterns. 

* **ValidateCard(string cardNumber)** 

  * *Description*: Strips spacers/dashes, confirms string length falls strictly inside ISO 13–19 standards, and verifies checksum via Luhn algorithm equations.
  * *Parameters*: cardNumber (string) - Formatted plain raw user credit card inputs tracking sequence.
  * *Returns*: bool (true if valid, otherwise false).
* **GetCardType(string cardNumber)** 

  * *Description*: Decodes specific card issuer networks utilizing evaluated regular expression prefix matches.
  * *Parameters*: cardNumber (string) - Text target input segment tracking data.
  * *Returns*: string (The string label matching issuer profiles: "Visa", "MasterCard", "American Express", "Discover", "JCB", "Maestro", or "Unknown").