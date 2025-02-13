**Veritabanı Scripti**: Proje dizininde `Database` klasörü altında `CargoManagementDB_Script.sql` dosyası bulunmaktadır.



## API Kullanımı
Örnek kullanım sırası aşağıda gösterildiği şekildedir:

### 1. Taşıyıcı Ekleme
- **Örnek İstek**:
- **Endpoint**: `POST /api/Carriers`
  ```json
  {
    "carrierName": "Sample Carrier",
    "carrierIsActive": true,
    "carrierPlusDesiCost": 5,
    "carrierConfigurationId": 1
  }
  ```

### 2. Sipariş Ekleme
- **Endpoint**: `POST /api/Orders`
```json
{
  "carrierId": 1,
  "orderDesi": 25,
  "orderDate": "2025-02-13T10:55:42.783Z",
  "orderCarrierCost": 125.0
}
```

### 3. Taşıyıcı Yapılandırması Ekleme
- **Endpoint**: `POST /api/CarrierConfigurations`
```json
{
  "carrierId": 1,
  "carrierMaxDesi": 100,
  "carrierMinDesi": 10,
  "carrierCost": 50.0
}
```
