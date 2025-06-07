# AdventureWorks 2022 Veritabanı Projesi

Bu proje, C# konsol uygulaması olarak geliştirilmiş olup, Entity Framework Core kullanarak SQL Server üzerindeki AdventureWorks 2022 veritabanına bağlanmaktadır. Temel amacı, ürün ve sipariş yönetimi ile satış raporlaması gibi işlevleri kullanıcı dostu bir arayüzle sunmaktır.

## Özellikler

- **Ürünleri Kategoriye Göre Listeleme:**
  - Belirli bir kategoriye ait tüm ürünleri görüntüleyebilirsiniz.
- **Sipariş Detaylarını Görüntüleme:**
  - Seçilen siparişin detaylarını ve ilgili müşteri bilgilerini inceleyebilirsiniz.
- **Yeni Sipariş Ekleme:**
  - Sisteme yeni bir sipariş ekleyebilirsiniz.
- **Kategori Bazında Satış Raporu:**
  - Ürün kategorilerine göre satış raporları oluşturabilirsiniz.
- **Çıkış:**
  - Uygulamadan güvenli bir şekilde çıkabilirsiniz.

## Kullanılan Teknolojiler

- **C#** (.NET 6 veya üzeri önerilir)
- **Entity Framework Core**
- **SQL Server** (AdventureWorks2022 veritabanı)

## Veritabanı Modelleri ve İlişkiler

Aşağıdaki tablolar modele dahil edilmiştir:
- `Production.Product`
- `Production.ProductSubcategory`
- `Sales.Customer`
- `Sales.SalesOrderHeader`
- `Sales.SalesOrderDetail`

Tablolar arası ilişkiler ForeignKey ve Column attribute'ları ile tanımlanmıştır.

## Bağlantı Dizesi

Uygulama, aşağıdaki bağlantı dizesi ile veritabanına bağlanır:

```
Data Source=.\SQLEXPRESSEMIR;Initial Catalog=AdventureWorks2022;User ID=sa;Password=sifreniz;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;MultipleActiveResultSets=true;App=EntityFramework;
```

> **Not:** Güvenlik nedeniyle gerçek ortamda kullanıcı adı ve şifre gibi hassas bilgileri açık bırakmamanız önerilir.

## Kurulum ve Çalıştırma

1. **AdventureWorks2022** veritabanını SQL Server'a kurun.
2. Proje dosyalarını bilgisayarınıza indirin.
3. Bağlantı dizesini kendi veritabanı ayarlarınıza göre güncelleyin (gerekirse).
4. Komut satırında proje klasörüne gidin ve aşağıdaki komutu çalıştırın:

   ```bash
   dotnet run
   ```

5. Konsol üzerinden menüyü takip ederek işlemlerinizi gerçekleştirin.

## Katkıda Bulunanlar

- Emirhan (Geliştirici)

## Lisans

Bu proje eğitim amaçlı hazırlanmıştır. Dilediğiniz gibi kullanabilirsiniz.
