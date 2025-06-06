-- 10 T-SQL Sorgusu (JOIN, GROUP BY, CTE, Window Function, Data Modification)

-- 1. Sipariş detaylarıyla müşteri ve ürün bilgisi
SELECT c.CustomerID, p.Name AS ProductName, sod.OrderQty, soh.OrderDate
FROM Sales.Customer c
JOIN Sales.SalesOrderHeader soh ON c.CustomerID = soh.CustomerID
JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID
JOIN Production.Product p ON sod.ProductID = p.ProductID;

-- 2. Ürün, alt kategori ve kategori bilgisi
SELECT p.Name AS ProductName, ps.Name AS Subcategory, pc.Name AS Category
FROM Production.Product p
JOIN Production.ProductSubcategory ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
JOIN Production.ProductCategory pc ON ps.ProductCategoryID = pc.ProductCategoryID;

-- 3. Sipariş, müşteri, çalışan ve departman bilgisi
SELECT soh.SalesOrderID, c.CustomerID, e.BusinessEntityID AS EmployeeID, d.Name AS Department
FROM Sales.SalesOrderHeader soh
JOIN Sales.Customer c ON soh.CustomerID = c.CustomerID
JOIN HumanResources.Employee e ON soh.SalesPersonID = e.BusinessEntityID
JOIN HumanResources.EmployeeDepartmentHistory edh ON e.BusinessEntityID = edh.BusinessEntityID
JOIN HumanResources.Department d ON edh.DepartmentID = d.DepartmentID;

-- 4. Kategoriye göre toplam satış adedi ve tutarı
SELECT pc.Name AS Category, SUM(sod.OrderQty) AS TotalSold, SUM(sod.LineTotal) AS TotalAmount
FROM Production.ProductCategory pc
JOIN Production.ProductSubcategory ps ON pc.ProductCategoryID = ps.ProductCategoryID
JOIN Production.Product p ON ps.ProductSubcategoryID = p.ProductSubcategoryID
JOIN Sales.SalesOrderDetail sod ON p.ProductID = sod.ProductID
GROUP BY pc.Name;

-- 5. Müşteriye göre toplam sipariş sayısı ve toplam harcama
SELECT c.CustomerID, COUNT(soh.SalesOrderID) AS OrderCount, SUM(soh.TotalDue) AS TotalSpent
FROM Sales.Customer c
JOIN Sales.SalesOrderHeader soh ON c.CustomerID = soh.CustomerID
GROUP BY c.CustomerID;

-- 6. Son 1 yılda en çok sipariş veren müşteriler (CTE ile)
WITH RecentOrders AS (
    SELECT CustomerID, COUNT(*) AS OrderCount
    FROM Sales.SalesOrderHeader
    WHERE OrderDate >= DATEADD(year, -1, GETDATE())
    GROUP BY CustomerID
)
SELECT TOP 5 CustomerID, OrderCount
FROM RecentOrders
ORDER BY OrderCount DESC;

-- 7. Ortalama satış tutarından fazla harcayan müşteriler (Subquery ile)
SELECT CustomerID, SUM(TotalDue) AS TotalSpent
FROM Sales.SalesOrderHeader
GROUP BY CustomerID
HAVING SUM(TotalDue) > (
    SELECT AVG(TotalDue) FROM Sales.SalesOrderHeader
);

-- 8. Her müşterinin siparişlerine tarih sırasıyla sıra numarası ver (Window Function)
SELECT SalesOrderID, CustomerID, OrderDate,
       ROW_NUMBER() OVER (PARTITION BY CustomerID ORDER BY OrderDate) AS OrderRank
FROM Sales.SalesOrderHeader;

-- 9. Yeni müşteri ekleme (INSERT)
BEGIN TRANSACTION;
INSERT INTO Sales.Customer (PersonID, StoreID, TerritoryID)
VALUES (1, NULL, 1);
COMMIT;

-- 10. Siparişin toplam tutarını güncelle (UPDATE)
BEGIN TRANSACTION;
UPDATE Sales.SalesOrderHeader
SET TotalDue = TotalDue * 1.05
WHERE SalesOrderID = 1;
COMMIT;

-- (Alternatif olarak bir DELETE sorgusu da eklenebilir)
-- BEGIN TRANSACTION;
-- DELETE FROM Sales.SalesOrderHeader WHERE SalesOrderID = 9999;
-- COMMIT;
