# Trabajo Tarjeta 2025

El siguiente trabajo es un enunciado iterativo. Regularmente se ampliara y/o modificara el enunciado.
<br><br>
Aclaraciones: 
- *Todos* los metodos deben estar testeados con un test unitario, aunque no se aclare explicitamente en el enunciado.
- Dentro de las posibilidades utilizar NUnit como framework de testing
- Para la nota final se tomara en cuenta no solo el codigo fuente de la implementacion, sino tambien el uso uso de Git y las herramientas que este provee como commits, ramas y tags.

## Iteración 1.
Escribir un programa con programación orientada a objetos que permita ilustrar el funcionamiento del transporte urbano de pasajeros de la ciudad de Rosario.
Las clases que interactúan en la simulación son: Colectivo, Tarjeta y Boleto.
Cuando un usuario viaja en colectivo con una tarjeta, obtiene un boleto como resultado de la operación colectivo.pagarCon(tarjeta);
<br><br>
Para esta iteración se consideran los siguientes supuestos:
 - No hay medio boleto de ningún tipo.
 - No hay transbordos.
 - No hay saldo negativo.
 - La tarifa básica de un pasaje es de: $1580
 - Las cargas aceptadas de tarjetas son: (2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000)
 - El límite de saldo de una tarjeta es de $40000
<br><br>
Se pide:
 - Hacer un fork del repositorio.
 - Implementar el código de las clases Tarjeta, Colectivo y Boleto.
 - Hacer que el test Tarjeta.cs funcione correctamente con todos los montos de pago listados.
 - Enviar el enlace del repositorio al mail del profesor con los integrantes del grupo: dos por grupo.
