# Microservice E-Ticaret Uygulaması

Bu proje, Udemy'de Fatih Çakıroğlu'ndan alınan .Net Core 7.0 eğitimi kapsamında geliştirilen bir **E-Ticaret Uygulaması**dır. Proje, Microservice Mimarisi'ni kullanarak online kurs satın alma işlemlerini gerçekleştiren bir sistem oluşturur.

## Proje Detayları

Projede **.Net Core 7.0** ve **Microservice Mimarisi** kullanılmıştır. Uygulama, farklı microservisler ile modüler bir yapıdadır. Her microservice, belirli bir işlevi yerine getirir ve birbirleriyle mesajlaşma sistemi ile haberleşir.

### Kullanılan Microservisler

- **Basket MicroService** - Alışveriş sepeti işlemlerini yönetir.  
- **Catalog MicroService** - Ürün katalogları ve kurs bilgilerini yönetir.  
- **Discount MicroService** - İndirim kampanyalarını yönetir.  
- **Order MicroService** - Sipariş işlemlerini ve sipariş yönetimini sağlar.  
- **FakePayment MicroService** - Ödeme işlemlerini simüle eder.  
- **PhotoStock MicroService** - Ürün fotoğraflarını yönetir.  

## Kullanılan Teknolojiler ve Araçlar

Projede çeşitli teknolojiler ve araçlar kullanılmıştır. Bunlar, projenin mikroservis yapısında verimli bir şekilde çalışmasını sağlar:

- **ASP.NET Core Web API 7.0**: Microservislerin API'lerini oluşturmak için kullanıldı.
- **Ocelot Gateway**: API Gateway olarak kullanıldı, servisler arası yönlendirmeyi sağladı.
- **MassTransit ve RabbitMQ**: Mesaj kuyruklama sistemi ile servisler arası haberleşme sağlandı.
- **JWT (Json Web Token)**: API'lerin güvenliğini sağlamak amacıyla kullanıldı.
- **Token Exchange**: Microservisler arasında kimlik doğrulama token'larının paylaşımı.
- **Postman**: API'lerin test edilmesinde kullanıldı.
- **ASP.NET Core MVC 7.0**: Frontend tarafında microservislerin kullanımını sağladı.
- **Identity Model - OpenIDConnect**: Kimlik doğrulama ve yetkilendirme işlemlerinde kullanıldı.
- **Onion Architecture**: Katmanlı mimari ile modüler yapı oluşturuldu.
- **Domain Driven Design (DDD)**: Domain tabanlı yazılım geliştirme yaklaşımı benimsendi.
- **Veritabanı Teknolojileri**:
  - **Microsoft SQL Server**
  - **PostgreSQL**
  - **MongoDB**
  - **Redis**
- **Docker**: Uygulamalar Docker ile konteynerize edildi ve Docker Compose ile yönetildi.
- **Portainer**: Docker konteynerlerinin yönetiminde kullanıldı.
- **Identity Server (.Net Core 3.1)**: Kimlik doğrulama işlemleri için ücretsiz versiyonu kullanıldı.
- **Entity Framework Core (Code First)**: Veritabanı işlemleri için kullanıldı.
- **Dapper Micro-ORM**: Veritabanı sorguları için kullanıldı.
- **AutoMapper**: Nesne dönüştürme işlemlerinde kullanıldı.
- **Bootstrap, CSS, HTML**: Frontend tasarımında kullanıldı.

## Kurulum

Projeyi yerel ortamınızda çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. **Repo'yu Klonlayın**:
   ```bash
   git clone https://github.com/kullanici-adi/proje-adi.git
