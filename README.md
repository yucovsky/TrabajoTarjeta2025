<p align="center">
  <a href="https://codecov.io/gh/yucovsky/TrabajoTarjeta2025">
    <img src="https://codecov.io/gh/yucovsky/TrabajoTarjeta2025/branch/main/graph/badge.svg" alt="Codecov coverage" width="200"/>
  </a>
</p>



# 🚌 Trabajo Tarjeta 2025  

> Proyecto desarrollado como práctica de Programación Orientada a Objetos y Testing Unitario.  
> Los participantes son:  
> 👤 **Julián Vaccari**  
> 👤 **Francisco Yucovsky**

---

## 📘 Descripción General

El objetivo del trabajo es simular el funcionamiento del sistema de transporte urbano de pasajeros de la ciudad de Rosario, utilizando **C# y .NET**.  
El sistema se modela a través de las clases principales:  
- `Tarjeta`  
- `Colectivo`  
- `Boleto`

A lo largo de distintas **iteraciones**, se incorporan nuevas funcionalidades, pruebas unitarias y mejoras de diseño orientadas a objetos.

---

## 🧩 Iteraciones del Proyecto

### 🔹 Iteración 1 – Implementación básica
- Implementación inicial de las clases `Tarjeta`, `Colectivo` y `Boleto`.
- Tarifa base: **$1580**.
- Cargas válidas: `2000`, `3000`, `4000`, `5000`, `8000`, `10000`, `15000`, `20000`, `25000`, `30000`.
- Límite máximo de saldo: **$40.000**.
- Sin medio boleto, transbordos ni saldo negativo.  
- Primeros tests unitarios funcionando con **NUnit**.  
- Se creó el **tag `iteracion1`** al finalizar.

---

### 🔹 Iteración 2 – Funcionalidades ampliadas
- **Cobertura de código** con *GitHub Actions* y badge de *Codecov*.
- **Descuento de saldos** al pagar con la tarjeta.
- **Saldo negativo permitido** hasta **$1200**.
- Implementación de **franquicias**:
  - Medio Boleto Estudiantil 🎓  
  - Franquicia Completa (gratuita) 🧓  
  - Boleto Gratuito Educativo 🏫
- Nuevos tests para validar descuentos, límites y tipos de tarjeta.

---

### 🔹 Iteración 3 – Manipulación de fechas y horarios
- La clase `Boleto` ahora incluye:  
  `Fecha`, `TipoTarjeta`, `Línea`, `TotalAbonado`, `Saldo`, `ID Tarjeta`.
- Control de **viajes por día y por horario**:  
  - Medio boleto: hasta **2 viajes por día**, con **5 minutos** mínimos entre viajes.  
  - Franquicia completa: hasta **2 viajes gratis por día**, luego tarifa normal.
- Nuevo límite de saldo máximo: **$56.000**, con **saldo pendiente de acreditación** al superar el límite.
- Implementación del método `AcreditarCarga`.

---

### 🔹 Iteración 4 – Boleto de uso frecuente y trasbordos
- **Boleto de uso frecuente (tarjeta normal):**
  - 1–29 viajes → tarifa normal  
  - 30–59 viajes → 20% descuento  
  - 60–80 viajes → 25% descuento  
  - 81+ → tarifa normal
- **Franquicias:** válidas solo **lunes a viernes de 6 a 22**.
- **Líneas interurbanas:** tarifa base $3000.
- **Trasbordos:** sin costo dentro de 1 hora, entre líneas distintas (lun-sáb de 7 a 22).

---

## 🧪 Testing y Cobertura

- Framework utilizado: **NUnit** 🧷  
- Comando para ejecutar los tests:
  ```bash
  dotnet test
