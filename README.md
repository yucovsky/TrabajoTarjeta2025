[![codecov](https://codecov.io/gh/mgonzalesips/TrabajoTarjeta2025/graph/badge.svg?token=XI7V37W69W)](https://codecov.io/gh/mgonzalesips/TrabajoTarjeta2025)
# Trabajo Tarjeta 2025

El siguiente trabajo es un enunciado iterativo. Regularmente se ampliara y/o modificara el enunciado.
<br><br>
Aclaraciones: 
- *Todos* los metodos deben estar testeados con un test unitario, aunque no se aclare explicitamente en el enunciado.
- Dentro de las posibilidades utilizar NUnit como framework de testing
- Para la nota final se tomara en cuenta no solo el codigo fuente de la implementacion, sino tambien el uso uso de Git y las herramientas que este provee como commits, ramas y tags.
- Cada clase de la implementacion y de testing debe estar en un archivo aparte.

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

## Iteración 2.
Para esta iteración hay 3 tareas principales. Crear un issue en github copiando la descripción de cada tarea y completar cada uno en una rama diferente. Éstas serán mergeadas al validar, luego de una revisión cruzada (de ambos integrantes del grupo), que todo el código tiene sentido y está correctamente implementado.<br>
No es necesario que todo el código para un issue esté funcionando al 100% antes de mergiearlo, pueden crear pull requests que solucionen algún item particular del problema para avanzar más rápido.
Además de las tareas planteadas, cada grupo tiene tareas pendientes de la iteración antertior que debe finalizar antes de comenzar con la iteración 2. Cuando la iteración 1 esté completada, crear un [tag](https://git-scm.com/book/en/v2/Git-Basics-Tagging) llamado iteracion1: Y subirlo a github.
<br><br>
### Covertura de codigo.
Implementar Git Actions en el repositorio, la [covertura de codigo](https://about.codecov.io/) y tambien el badge.
<br><br>
### Descuento de saldos.
Cada vez que una tarjeta paga un boleto, descuenta el valor del monto gastado.
 - Si la tarjeta se queda sin saldo, la operación $colectivo->pagarCon($tarjeta) devuelve FALSE,
<br><br>   
### Saldo negativo
- Si la tarjeta se queda sin crédito, puede tener un saldo negativo de hasta $1200.
- Cuando se vuelve a cargar la tarjeta, se descuenta el saldo de lo que se haya consumido.
- Escribir un test que valide que la tarjeta no pueda quedar con menos saldo que el permitido.
- Escribir un test que valide que el saldo de la tarjeta descuenta correctamente el/los viaje/s plus otorgado/s.
<br><br>
### Franquicia de Boleto.
Existen dos tipos de franquicia en lo que refiere a tarjetas, las franquicias parciales, como el medio boleto estudiantil o el universitario, y las completas como las de jubilados(Notar que también existe boleto gratuito para estudiantes).
- Implementar cada tipo de tarjeta como una Herencia de la tarjeta original (Medio boleto estudiantil, Boleto gratuito estudiantil, Franquicia completa).
- Para esta iteración considerar simplemente que cuando se paga con una tarjeta del tipo MedioBoleto el costo del pasaje vale la mitad, independientemente de cuántas veces se use y que día de la semana sea.
- Escribir un test que valide que una tarjeta de FranquiciaCompleta siempre puede pagar un boleto.
- Escribir un test que valide que el monto del boleto pagado con medio boleto es siempre la mitad del normal.
