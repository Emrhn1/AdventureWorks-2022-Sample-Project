# Person ve HumanResources Şemaları Temel Tablolar ve Açıklamalar

## Person Şeması
- **Person.Person**: It holds personal information (name, surname, contact, etc.). It can be a customer, employee or vendor.
- **Person.Address**: Stores address information.
- **Person.EmailAddress**: Stores people's email addresses.
- **Person.BusinessEntity**: The base identity table for all business entities (key for customer, employee, vendor, etc.).
- **Person.PhoneNumberType**: Stores phone number types (work, home, mobile, etc.).

## HumanResources Schema
- **HumanResources.Employee**: Keeps employee information.
- **HumanResources.Department**: Lists company departments.
- **HumanResources.Shift**: Defines work shifts.
- **HumanResources.EmployeeDepartmentHistory**:Departmental history of employees (who worked in which department and on which dates).
- **HumanResources.JobCandidate**: Information about job applicants.
