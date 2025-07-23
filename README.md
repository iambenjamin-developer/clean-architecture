# clean-architecture

Add-Migration Initial -Output Persistence/Migrations
Update-Database

https://localhost:7083/api/Products


```
{
  "sku": "PROD-001",
  "name": "Laptop Dell Inspiron 15",
  "description": "Laptop Dell de 15 pulgadas, procesador Intel i7, 16GB RAM, 512GB SSD.",
  "price": 1250.50,
  "stock": 10,
  "rating": 4.5,
  "imageUrl": "https://example.com/images/laptop-dell-inspiron15.jpg",
  "categoryId": 1
}


curl -X 'POST' \
  'https://localhost:7083/api/Products' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "sku": "PROD-001",
  "name": "Laptop Dell Inspiron 15",
  "description": "Laptop Dell de 15 pulgadas, procesador Intel i7, 16GB RAM, 512GB SSD.",
  "price": 1250.50,
  "stock": 10,
  "rating": 4.5,
  "imageUrl": "https://example.com/images/laptop-dell-inspiron15.jpg",
  "categoryId": 1
}'

```