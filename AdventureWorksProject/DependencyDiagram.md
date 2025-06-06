# AdventureWorks 5 Tablo Dependency Diyagramı

```mermaid
erDiagram
    CUSTOMER ||--o{ SALESORDERHEADER : "gives"
    SALESORDERHEADER ||--o{ SALESORDERDETAIL : "includes"
    SALESORDERDETAIL }o--|| PRODUCT : "product"
    PRODUCT }o--|| PRODUCTCATEGORY : "category"
```

**Description:**
- A customer can place more than one order (SalesOrderHeader).
- Each order can have multiple details (SalesOrderDetail).
- Each detail is associated with a product (Product).
- Each product belongs to a category (ProductCategory).
